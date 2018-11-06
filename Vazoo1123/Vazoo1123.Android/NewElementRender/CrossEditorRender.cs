using Android.Content;
using Android.Content.Res;
using Android.OS;
using Vazoo1123.Droid.NewElementRender;
using Vazoo1123.NewElement;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CrossEdit), typeof(CrossEditorRender))]
namespace Vazoo1123.Droid.NewElementRender
{
    class CrossEditorRender : EditorRenderer
    {
        public CrossEditorRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Element == null)
                return;

            var element = (CrossEdit)Element;
            Control.Hint = element.Placeholder;
            Control.SetHintTextColor(element.PlaceholderColor.ToAndroid());

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