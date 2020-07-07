using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CloudBackground : ContentView
    {
        #region Cloud Bitmap Keys
        private const string SINGLE_CLOUD_KEY = "scb";

        private const string CEILING_TOP_CLOUD_KEY = "ct";
        private const string CEILING_MIDDLE_CLOUD_KEY = "cm";
        private const string CEILING_BACK_CLOUD_KEY = "cb";
        private const string FLOOR_TOP_CLOUD_KEY = "ft";
        private const string FLOOR_MIDDLE_CLOUD_KEY = "fm";
        private const string FLOOR_BACK_CLOUD_KEY = "fb";
        #endregion

        /// <summary>
        ///     Used to control animation framerate.
        /// </summary>
        Stopwatch Stopwatch { get; set; } = new Stopwatch();
        /// <summary>
        ///     State that determines whether the animation is running.
        /// </summary>
        public bool IsActive { get; private set; }

        public Dictionary<string, CloudData> Clouds { get; private set; }
        /// <summary>
        ///     Stores the shader needed for rendering our gradient background.
        /// </summary>
        public SKPaint GradientPaint { get; private set; } = new SKPaint();

        public CloudBackground()
        {
            InitializeComponent();

            // Attaching a handler for the linear gradient
            canvasView.PaintSurface += (sender, e) =>
            {
                e.Surface.Canvas.Clear();

                if (GradientPaint.Shader == null)
                    GradientPaint.Shader = SKShader.CreateLinearGradient(
                                        new SKPoint(0, 0),
                                        new SKPoint(e.Info.Width, e.Info.Height),
                                        new SKColor[] { ((Color)App.Current.Resources[App.ResourceKeys.GRADIENT_BLUE]).ToSKColor(), ((Color)App.Current.Resources[App.ResourceKeys.GRADIENT_PURP]).ToSKColor() },
                                        null,
                                        SKShaderTileMode.Repeat);

                // Draw the gradient on the rectangle
                e.Surface.Canvas.DrawRect(0, 0, e.Info.Width, e.Info.Height, GradientPaint);
            };           
            
            // Get the current opimization state
            AnimationOptimization optimization = (AnimationOptimization)Preferences.Get(App.AnimationKeys.COSTLY_ANIMATION_FRAMERATE, (int)AnimationOptimization.Fancy);

            // Depending on the current optimization state attach a different event handler.
            switch (optimization)
            {
                case AnimationOptimization.Basic:
                    
                    break;
                case AnimationOptimization.Intermediate:
                    // Getting the embedded bitmap
                    Clouds = new Dictionary<string, CloudData>
                    {
                        { SINGLE_CLOUD_KEY , new CloudData(GetSKBitmap("SmartPillow.Media.Cloud1.png")) }
                    };
                    canvasView.PaintSurface += SKCanvas_Intermediate_PaintCanvas;
                    break;
                case AnimationOptimization.Fancy:

                    int height = (int)DeviceDisplay.MainDisplayInfo.Height;

                    // Clouds are arranged from top to bottem for layering
                    // Ceiling clouds are loaded first and then floor clouds
                    Clouds = new Dictionary<string, CloudData>
                    {
                        { CEILING_TOP_CLOUD_KEY, new CloudData(GetSKBitmap("SmartPillow.Media.Ceiling_BackLayerCloud.png"), 70, 0.3f) },
                        { CEILING_MIDDLE_CLOUD_KEY, new CloudData(GetSKBitmap("SmartPillow.Media.Ceiling_MiddleLayerCloud.png"), 45, 0.7f) },
                        { CEILING_BACK_CLOUD_KEY, new CloudData(GetSKBitmap("SmartPillow.Media.Ceiling_TopLayerCloud.png"), 30, 0.9f) },
                        { FLOOR_TOP_CLOUD_KEY, new CloudData(GetSKBitmap("SmartPillow.Media.Floor_BackLayerCloud.png"), height / 2 + 200, 0.2f) },
                        { FLOOR_MIDDLE_CLOUD_KEY, new CloudData(GetSKBitmap("SmartPillow.Media.Floor_MiddleLayerCloud.png"), height / 2 + 150, 0.6f) },
                        { FLOOR_BACK_CLOUD_KEY, new CloudData(GetSKBitmap("SmartPillow.Media.Floor_TopLayerCloud.png"), height / 2 + 130, 0.8f) }
                    };
                    canvasView.PaintSurface += SKCanvas_Fancy_PaintCanvas;
                    break;
                default:
                    
                    break;
            }
        }

        private void CloudBackground_MeasureInvalidated(object sender, EventArgs e)
        {
            Console.WriteLine();
        }

        /// <summary>
        ///     Fancy level of optimiation for drawing our animation.<br/>
        ///     Draws 6 Bitmaps in two groups that create an infinite loop of clouds floating by.<br/>
        ///     Each group contains two 3 bitmaps with the higher Z value bitmaps moving faster than the lower.<br/>
        ///     Ceiling clouds are drawn before floor clouds.<br/>
        ///     Cloud layers are drawn back to front.
        /// </summary>
        private void SKCanvas_Fancy_PaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            Clouds.ForEach(cloud =>
            {
                canvas.DrawBitmap(cloud.Value.CloudBitmap, cloud.Value.FirstStageCloudPoint);
                canvas.Save();

                if (cloud.Value.FirstStageCloudPoint.X != 0)
                {
                    canvas.Scale(-1, 1, 0, 0);
                    canvas.DrawBitmap(cloud.Value.CloudBitmap, cloud.Value.SecondStageCloudPoint);
                    // Restoring to the previous save.
                    canvas.Restore();
                }

                cloud.Value.CheckForReset();
            });
        }

        /// <summary>
        ///     Intermediate level of optimiation for drawing our animation.<br/>
        ///     Draws 2 Bitmaps that are recycled to create a infinite loop of clouds floating by.
        /// </summary>
        private void SKCanvas_Intermediate_PaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            var cloud = Clouds[SINGLE_CLOUD_KEY];
            canvas.DrawBitmap(cloud.CloudBitmap, cloud.FirstStageCloudPoint);
            // Creating a save point to restore to.
            canvas.Save();

            if (cloud.FirstStageCloudPoint.X != 0)
            {
                canvas.Scale(-1, 1, 0, 0);                
                canvas.DrawBitmap(cloud.CloudBitmap, cloud.SecondStageCloudPoint);
                // Restoring to the previous save.
                canvas.Restore();
            }

            cloud.CheckForReset();
        }

        /// <summary>
        ///     Starts the animation.<br/>
        ///     Call in OnAppearing handler.
        /// </summary>
        public async Task StartAnimation()
        {
            IsActive = true;
            Stopwatch.Start();

            while (IsActive)
            {
                Clouds.Values.ForEach(c => c.MoveCloud());
                canvasView.InvalidateSurface();                
                await Task.Delay(TimeSpan.FromSeconds(1.0 / 30));
            }

            Stopwatch.Stop();
        }

        /// <summary>
        ///     Stops the animation.<br/>
        ///     Call in OnDisappearing handler.        
        /// </summary>
        public void StopAnimation()
        {
            IsActive = false;
        }

        /// <summary>
        ///     Gets a Stream containing the bitmap data.
        /// </summary>
        private SKBitmap GetSKBitmap(string resourceId)
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceId))
            {
                return SKBitmap.Decode(stream);
            }
        }

        /// <summary>
        ///     Contains 1 cloud bitmap and two cloud points.<br/>
        ///     Contains 2 points because there needs to be two of the same SKBitmap being drawn to the screen.<br/>
        ///     The original bitmaps but one is flipped horizontally in the drawcall, this does not change the CloudBitmap property.
        /// </summary>
        public class CloudData
        {
            public SKBitmap CloudBitmap { get; set; }

            public float IncrementBy { get; private set; }

            private SKPoint firstStageCloudPoint;
            /// <summary>
            ///     The first stage cloud is drawn with itself taking up the display.<br/>
            ///     Slowly this cloud will move off the display and will be immediately followed by the second stage cloud.
            /// </summary>
            public SKPoint FirstStageCloudPoint => firstStageCloudPoint;

            private SKPoint secondStageCloudPoint;
            /// <summary>
            ///     The second stage cloud is hidden off the display initially.<br/>
            ///     As the first stage cloud moves off the display this cloud follows to give the appearance of a never ending cloud.<br/>
            ///     The X value of this point has its sign flipped to flex it on the X axis.
            /// </summary>
            public SKPoint SecondStageCloudPoint => secondStageCloudPoint;

            /// <summary>
            ///     Moves the cloud's coordinates to the right.
            /// </summary>
            public void MoveCloud()
            {
                firstStageCloudPoint.X += IncrementBy;
                secondStageCloudPoint.X += - IncrementBy;
            }

            /// <summary>
            ///     Resets the cloud's position.<br/>
            ///     If the animation is running this will make it snap to the start so beware.
            /// </summary>
            public void FullCloudReset()
            {
                firstStageCloudPoint.X = 0;
                secondStageCloudPoint.X = CloudBitmap.Width * (-1);
            }

            /// <summary>
            ///     Checks to see if the first or second stage is ready to be moved
            /// </summary>
            public void CheckForReset()
            {               
                if (FirstStageCloudPoint.X >= CloudBitmap.Width)
                    firstStageCloudPoint.X = -CloudBitmap.Width;
                if (SecondStageCloudPoint.X <= -(CloudBitmap.Width * 2)) // * 2 because since this secondstage is under a translate on the X axis by SkiaSharp we need to account for the width of the cloud itself too not only its X pos.
                    secondStageCloudPoint.X = 0;
            }

            public CloudData(SKBitmap _cloudBitmap, float _incrementBy = 1)
            {
                CloudBitmap = _cloudBitmap;
                IncrementBy = _incrementBy;
            }

            public CloudData(SKBitmap _cloudBitmap, int _y, float _incrementBy = 1)
            {
                CloudBitmap = _cloudBitmap;
                firstStageCloudPoint = new SKPoint(0, _y);
                secondStageCloudPoint = new SKPoint(0, _y);
                IncrementBy = _incrementBy;
            }
        }

        // Get the height from the parent view or something
    }
}