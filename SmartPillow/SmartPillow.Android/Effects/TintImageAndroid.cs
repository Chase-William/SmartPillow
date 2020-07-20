using System.Linq;
using Android.Graphics;
using Android.Widget;
using SmartPillow.Controls.TintColor;
using SmartPillow.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(TintImageAndroid), nameof(TintImage))]
namespace SmartPillow.Droid.Effects
{
    public class TintImageAndroid : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (TintImage)Element.Effects.FirstOrDefault(e => e is TintImage);

            if (effect == null || !(Control is ImageView image))
                return;

            var filter = new PorterDuffColorFilter(effect.TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            image.SetColorFilter(filter);
        }

        protected override void OnDetached() { }
    }
}