using Android.Content;
using SmartPillow.Controls;
using SmartPillow.Droid.Renderers;
using Xamarin.Forms;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRenderer))]
namespace SmartPillow.Droid.Renderers
{
    public class CustomTimePickerRenderer : Xamarin.Forms.Platform.Android.TimePickerRenderer
    {
        public CustomTimePickerRenderer(Context ctx) : base(ctx) 
        {
            
        }
    }
}