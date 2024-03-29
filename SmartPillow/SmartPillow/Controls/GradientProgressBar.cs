﻿using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartPillow.Controls
{
    /// <summary>
    ///     this control contains bindable properties created to be able to draw a progress bar with gradient
    /// </summary>
    public class GradientProgressBar : SKCanvasView
    {
        public static BindableProperty PercentageProperty = BindableProperty.Create(nameof(Percentage), typeof(float),
            typeof(GradientProgressBar), 0f, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public float Percentage
        {
            get => (float)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }

        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(Percentage), typeof(float),
            typeof(GradientProgressBar), 5f, BindingMode.OneWay,
            validateValue: (_, value) => value != null && (float)value >= 0,
            propertyChanged: OnPropertyChangedInvalidate);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static BindableProperty BarBackgroundColorProperty = BindableProperty.Create(nameof(BarBackgroundColor), typeof(Color),
            typeof(GradientProgressBar), Color.White, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color BarBackgroundColor
        {
            get => (Color)GetValue(BarBackgroundColorProperty);
            set => SetValue(BarBackgroundColorProperty, value);
        }

        public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float),
            typeof(GradientProgressBar), 12f, BindingMode.OneWay,
            validateValue: (_, value) => value != null && (float)value >= 0,
            propertyChanged: OnPropertyChangedInvalidate);

        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        ///     Still working on the animation to be a bindable property for XAML
        /// </summary>

        //public static BindableProperty AnimateToProperty =
        //    BindableProperty.CreateAttached("Animation",
        //    typeof(double),
        //    typeof(GradientProgressBar),
        //    0.0d,
        //    BindingMode.OneWay,
        //    validateValue: (_, value) => value != null && (float)value >= 0,
        //    propertyChanged: (b, o, n) =>
        //    Try((GradientProgressBar)b, (double)n));

        //public static void Try(GradientProgressBar progressBar, double progress)
        //{
        //    progressBar.AnimateTo((double)progress, 800, Easing.Linear);
        //}

        public Task<bool> AnimateTo(double value, uint length, Easing easing)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            this.Animate("Progress", (Action<double>)(d => this.Percentage = (float)d), Convert.ToDouble(Percentage),
                value, 16U, length, easing, (Action<double, bool>)((d, finished) => tcs.SetResult(finished)), (Func<bool>)null);
            return tcs.Task;
        }

        public static BindableProperty GradientStartColorProperty = BindableProperty.Create(nameof(GradientStartColor), typeof(Color),
            typeof(GradientProgressBar), Color.Purple, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color GradientStartColor
        {
            get => (Color)GetValue(GradientStartColorProperty);
            set => SetValue(GradientStartColorProperty, value);
        }

        public static BindableProperty GradientEndColorProperty = BindableProperty.Create(nameof(GradientEndColor), typeof(Color),
            typeof(GradientProgressBar), Color.Blue, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color GradientEndColor
        {
            get => (Color)GetValue(GradientEndColorProperty);
            set => SetValue(GradientEndColorProperty, value);
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color),
            typeof(GradientProgressBar), Color.Blue, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static BindableProperty AlternativeTextColorProperty = BindableProperty.Create(nameof(AlternativeTextColor), typeof(Color),
            typeof(GradientProgressBar), Color.Blue, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color AlternativeTextColor
        {
            get => (Color)GetValue(AlternativeTextColorProperty);
            set => SetValue(AlternativeTextColorProperty, value);
        }

        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (GradientProgressBar)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            float width = (float)Width;
            var scale = CanvasSize.Width / width;

            var percentage = Percentage;

            var cornerRadius = CornerRadius * scale;

            var textSize = FontSize * scale;

            var height = e.Info.Height;

            var str = percentage.ToString("0%");

            var percentageWidth = (int)Math.Floor(info.Width * percentage);

            canvas.Clear();

            var backgroundBar = new SKRoundRect(new SKRect(0, 0, info.Width, height), cornerRadius, cornerRadius);
            var progressBar = new SKRoundRect(new SKRect(0, 0, percentageWidth, height), cornerRadius, cornerRadius);

            var background = new SKPaint { Color = BarBackgroundColor.ToSKColor(), IsAntialias = true };

            canvas.DrawRoundRect(backgroundBar, background);

            using (var paint = new SKPaint() { IsAntialias = true })
            {
                float x = percentageWidth;
                float y = info.Height;
                var rect = new SKRect(0, 0, x, y);

                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(rect.Left, rect.Top),
                    new SKPoint(rect.Right, rect.Top),
                    new[]
                    {
                        GradientStartColor.ToSKColor(),
                        GradientEndColor.ToSKColor()
                    },
                    new float[] { 0, 1 },
                    SKShaderTileMode.Clamp);

                canvas.DrawRoundRect(progressBar, paint);
            }

            var textPaint = new SKPaint { Color = TextColor.ToSKColor(), TextSize = textSize };

            var textBounds = new SKRect();

            textPaint.MeasureText(str, ref textBounds);

            var xText = percentageWidth / 2 - textBounds.MidX;
            if (xText < 0)
            {
                xText = info.Width / 2 - textBounds.MidX;
                textPaint.Color = AlternativeTextColor.ToSKColor();
            }

            var yText = info.Height / 2 - textBounds.MidY;

            canvas.DrawText(str, xText, yText, textPaint);

            var leftCircle = new SKRoundRect(new SKRect(0, 0, height, height), cornerRadius, cornerRadius);
            var leftPaint = new SKPaint { Color = Color.White.ToSKColor(), IsAntialias = true };

            //canvas.DrawRoundRect(leftCircle, leftPaint);

            //var rightCircle = new SKRoundRect(new SKRect(height * 2, 0, height, height), cornerRadius, cornerRadius);

            //var moveToRight = new SKMatrix(0,0,0,0,0,(float)info.Width,0,0,0);

            //rightCircle.Transform(moveToRight);
            //var rightPaint = new SKPaint { Color = Color.White.ToSKColor(), IsAntialias = true };

            //canvas.DrawCircle(info.Width, info.Height / 2, cornerRadius, leftPaint);
            //canvas.DrawCircle(0, info.Height / 2, cornerRadius, leftPaint);
        }
    }
}
