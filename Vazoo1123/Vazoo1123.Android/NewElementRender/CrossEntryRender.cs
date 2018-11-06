using Android.Content;
using Android.Content.Res;
using Android.OS;
using Vazoo1123.Droid.NewElementRender;
using Vazoo1123.NewElement;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CrossEntry), typeof(CrossEntryRender))]
namespace Vazoo1123.Droid.NewElementRender
{
    class CrossEntryRender : EntryRenderer
    {
        public CrossEntryRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            
                

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Rgb(42, 189, 234));
            }
            else
            {
                Control.Background.SetColorFilter(Android.Graphics.Color.Rgb(42, 189, 234), Android.Graphics.PorterDuff.Mode.SrcAtop);
            }
        }
    }
}
