using Android.Widget;
using Vazoo1123.Droid.NewElementRender;
using Vazoo1123.NewElement;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CrossRadioBtn), typeof(CrossRadioBtnRenden))]
namespace Vazoo1123.Droid.NewElementRender
{
    public class CrossRadioBtnRenden : ViewRenderer<CrossRadioBtn, RadioButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CrossRadioBtn> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged += ElementOnPropertyChanged;
            }

            if (this.Control == null)
            {
                var radButton = new RadioButton(this.Context);
                radButton.CheckedChange += radButton_CheckedChange;

                this.SetNativeControl(radButton);
            }

            Control.Text = e.NewElement.Text;
            Control.Checked = e.NewElement.Checked;

            Element.PropertyChanged += ElementOnPropertyChanged;
        }

        void radButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            this.Element.Checked = e.IsChecked;
        }



        void ElementOnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;

            }
        }
    }
}