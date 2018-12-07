using Xamarin.Forms;

namespace Vazoo1123.NewElement
{
    public class CrossEdit : Editor
    {
        public static BindableProperty PlaceholderProperty
            = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CrossEdit));

        public static BindableProperty PlaceholderColorProperty
            = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CrossEdit), Color.Gray);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public CrossEdit()
        {
            this.TextColor = Color.FromHex("#2aa0ea");
            this.PlaceholderColor = Color.FromHex("#8094ff");
        }
    }
}