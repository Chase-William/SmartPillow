using SkiaSharp;
using SmartPillow.Pages.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Controls.TimedAlarms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SPDeleteBtn : ContentView
    {
        /// <summary>
        ///     Our Skia paint object for styling the outer circle
        /// </summary>
        SKPaint outerCicleStyle = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 7,
            IsAntialias = true
        };

        #region BindableProperties
        /// <summary>
        ///     Bindable property support for Commands.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(SPDeleteBtn), null, BindingMode.TwoWay, null, null);
        public static readonly BindableProperty SiblingFrameProperty = BindableProperty.Create(nameof(SiblingFrame), typeof(Frame), typeof(SPDeleteBtn), null, BindingMode.OneWay, null, null);
        #endregion

        #region UI Binding Properties
        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Frame SiblingFrame
        {
            get => (Frame)GetValue(SiblingFrameProperty);
            set => SetValue(SiblingFrameProperty, value);
        }


        #endregion

        public SPDeleteBtn()
        {
            InitializeComponent();

            PropertyChanged += SmartPillowDeleteBtn_PropertyChanged;
            PropertyChanging += SmartPillowDeleteBtn_PropertyChanging;
            Image.Clicked += Image_Clicked;
            Image.Clicked += delegate { Command?.Execute(null); };                       
        }

        private void SmartPillowDeleteBtn_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {
                if (IsVisible)
                {
                    
                }
            }
        }

        private async void SmartPillowDeleteBtn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {
                
            }
        }

        private async void Image_Clicked(object sender, EventArgs e)
        {
            await Task.WhenAll(OuterCircle.ScaleTo(0.8, 100),
                               OuterCircle.RotateXTo(90, 70));

            await Task.WhenAll(OuterCircle.ScaleTo(1.0, 100),
                               OuterCircle.RotateXTo(0, 70));
        }        

        private void OuterCircle_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            var info = e.Info;
            canvas.Clear();

            canvas.DrawCircle(info.Width / 2, info.Height / 2, (info.Width > info.Height ? info.Height : info.Width) / 2 - outerCicleStyle.StrokeWidth / 2, outerCicleStyle);           
        }
    }
}