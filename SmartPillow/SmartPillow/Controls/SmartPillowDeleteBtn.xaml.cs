using SkiaSharp;
using SmartPillow.Pages.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPillow.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmartPillowDeleteBtn : ContentView
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
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(SmartPillowDeleteBtn), null, BindingMode.TwoWay, null, null);
        public static readonly BindableProperty AboveListViewProperty = BindableProperty.Create(nameof(AboveListView), typeof(ListView), typeof(SmartPillowDeleteBtn), null, BindingMode.OneWay, null, null);
        public static readonly BindableProperty ParentPageProperty = BindableProperty.Create(nameof(ParentPage), typeof(Page), typeof(SmartPillowDeleteBtn), null, BindingMode.OneWay, null, null);
        #endregion

        #region UI Binding Properties
        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ListView AboveListView
        {
            get => (ListView)GetValue(AboveListViewProperty);
            set => SetValue(AboveListViewProperty, value);
        }

        public Page ParentPage
        {
            get => (Page)GetValue(ParentPageProperty);
            set => SetValue(ParentPageProperty, value);
        }

        #endregion

        public SmartPillowDeleteBtn()
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
                    this.TranslationY = ParentPage.Height;
                }
            }
        }

        private async void SmartPillowDeleteBtn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {
                if (IsVisible)
                {                    
                    var listViewRect = new Rectangle(AboveListView.X, AboveListView.Y, AboveListView.Width, AboveListView.Height - (this.HeightRequest * 1.2 + this.Margin.Bottom + this.Margin.Top));
                    await Task.WhenAll(this.TranslateTo(0, AboveListView.Height, 200, Easing.BounceIn),
                                       AboveListView.LayoutTo(listViewRect, 200));                
                }    
                else
                {                    
                    var listViewRect = new Rectangle(AboveListView.X, AboveListView.Y, AboveListView.Width, AboveListView.Height + (this.HeightRequest * 1.2 + this.Margin.Bottom + this.Margin.Top));
                    await Task.WhenAll(this.TranslateTo(0, ParentPage.Height, 200, Easing.BounceOut),
                                       AboveListView.LayoutTo(listViewRect, 200));
                }
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