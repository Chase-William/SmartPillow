using Android.Content;
using SmartPillow.Droid.Renderers;
using SmartPillow.Pages.Nav;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TransparentNavigationPage), typeof(TransparentNavigationPageRenderer))]
namespace SmartPillow.Droid.Renderers
{
    /// <summary>
    ///     https://xamgirl.com/transparent-navigation-bar-in-xamarin-forms/
    ///     this renderer helps to turn the navigation bar to transparent and keep the drawer icon, title, and right-side icon displayed
    /// </summary>

    public class TransparentNavigationPageRenderer : NavigationPageRenderer
    {
        public TransparentNavigationPageRenderer(Context context) : base(context) { }

        IPageController PageController => Element as IPageController;
        TransparentNavigationPage TransparentNavigationPage => Element as TransparentNavigationPage;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            TransparentNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            TransparentNavigationPage.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                var child = GetChildAt(i);

                if (child is Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}