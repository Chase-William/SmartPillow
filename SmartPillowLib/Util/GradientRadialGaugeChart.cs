using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPillowLib.Util
{
    public class GradientRadialGaugeChart : Chart
    {
        #region Properties

        /// <summary>
        /// Gets or sets the size of each gauge. If negative, then its will be calculated from the available space.
        /// </summary>
        /// <value>The size of the line.</value>
        public float LineSize { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gauge background area alpha.
        /// </summary>
        /// <value>The line area alpha.</value>
        public byte LineAreaAlpha { get; set; } = 52;

        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        /// <value>The start angle.</value>
        public float StartAngle { get; set; } = -90;

        private float AbsoluteMinimum => this.Entries.Select(x => x.Value).Concat(new[] { this.MaxValue, this.MinValue, this.InternalMinValue ?? 0 }).Min(x => Math.Abs(x));

        private float AbsoluteMaximum => this.Entries.Select(x => x.Value).Concat(new[] { this.MaxValue, this.MinValue, this.InternalMinValue ?? 0 }).Max(x => Math.Abs(x));

        private float ValueRange => this.AbsoluteMaximum - this.AbsoluteMinimum;

        #endregion

        #region Methods

        public void DrawGaugeArea(SKCanvas canvas, Entry entry, float radius, int cx, int cy, float strokeWidth)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                Color = entry.Color.WithAlpha(this.LineAreaAlpha),
                IsAntialias = false,
            })
            {
                canvas.DrawCircle(cx, cy, radius, paint);
            }
        }

        public void DrawGauge(SKCanvas canvas, Entry entry, float radius, int cx, int cy, float strokeWidth)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                StrokeCap = SKStrokeCap.Round,

                // from top left to bottom right
                Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(cx / 2, cy / 2),
                                    new SKPoint(cx * 1.45f, cy * 1.45f),
                                    new SKColor[] { SKColor.Parse("#D765FF"), SKColor.Parse("#7AC0DF") },
                                    null,
                                    SKShaderTileMode.Repeat),

                // from top to bottom
                //Shader = SKShader.CreateLinearGradient(
                //                    new SKPoint(cx * 2f, 0),
                //                    new SKPoint(cx * 2f, cy * 1.65f),
                //                    new SKColor[] { SKColor.Parse("#D765FF"), SKColor.Parse("#7AC0DF") },
                //                    null,
                //                    SKShaderTileMode.Repeat),

                IsAntialias = true,
            })
            {
                using (SKPath path = new SKPath())
                {
                    var sweepAngle = 360 * (Math.Abs(entry.Value) - this.AbsoluteMinimum) / this.ValueRange;
                    path.AddArc(SKRect.Create(cx - radius, cy - radius, 2 * radius, 2 * radius), this.StartAngle, sweepAngle);
                    canvas.DrawPath(path, paint);
                }
            }
        }

        public override void DrawContent(SKCanvas canvas, int width, int height)
        {
            this.DrawCaption(canvas, width, height);

            var sumValue = this.Entries.Sum(x => Math.Abs(x.Value));
            var radius = (Math.Min(width, height) - (2 * Margin)) / 2;
            var cx = width / 2;
            var cy = height / 2;
            var lineWidth = (this.LineSize < 0) ? (radius / ((this.Entries.Count() + 1) * 2)) : this.LineSize;
            var radiusSpace = lineWidth * 2;

            for (int i = 0; i < this.Entries.Count(); i++)
            {
                var entry = this.Entries.ElementAt(i);
                var entryRadius = (i + 1) * radiusSpace;
                //if entry.count is at 0, skip drawing a circle to disappear a small dot in the middle of a radial gauge chart
                if(i == 1)
                {
                    this.DrawGaugeArea(canvas, entry, entryRadius, cx, cy, lineWidth);
                    this.DrawGauge(canvas, entry, entryRadius, cx, cy, lineWidth);
                }
            }
        }

        private void DrawCaption(SKCanvas canvas, int width, int height)
        {
            var range = this.ValueRange;
            var rightValues = new List<Entry>();
            var leftValues = new List<Entry>();

            foreach (var entry in this.Entries)
            {
                if (Math.Abs(entry.Value) < range / 2)
                {
                    rightValues.Add(entry);
                }
                else
                {
                    leftValues.Add(entry);
                }
            }

            leftValues.Reverse();

            this.DrawCaptionElements(canvas, width, height, rightValues, false);
            this.DrawCaptionElements(canvas, width, height, leftValues, true);
        }

        #endregion
    }
}
