using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using SmartPillow.Controls;
using SmartPillow.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomRadioBtn), typeof(RadioBtnRenderer))]
namespace SmartPillow.Droid.Renderers
{
    /// <summary>
    ///     Custom renderer for android radio btns.
    /// </summary>
    public class RadioBtnRenderer : Xamarin.Forms.Platform.Android.RadioButtonRenderer
    {
        public RadioBtnRenderer(Context ctx) : base(ctx) 
        {
            SetHighlightColor(Android.Graphics.Color.Yellow);
            //SetBackgroundColor(Android.Graphics.Color.Green);
            SetTextColor(Android.Graphics.Color.Red);
            SetOutlineSpotShadowColor(Android.Graphics.Color.Red);
            SetOutlineAmbientShadowColor(Android.Graphics.Color.Red);


            //SetButtonDrawable(Resource.Drawable.CloudButtonUnChecked);

            //Click += delegate
            //{
            //    if (!Checked)
            //        SetButtonDrawable(Resource.Drawable.CloudButtonUnChecked);
            //    else
            //        SetButtonDrawable(Resource.Drawable.CloudButtonChecked);
            //};            

            //this.ButtonTintList = new Android.Content.Res.ColorStateList(null, new int[] { 999999 });
        }
    }
}