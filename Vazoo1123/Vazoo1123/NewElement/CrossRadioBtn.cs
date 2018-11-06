using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Vazoo1123.NewElement
{
    public class CrossRadioBtn : View
    {
        public static readonly BindableProperty CheckedProperty =
                   BindableProperty.Create<CrossRadioBtn, bool>(
                       p => p.Checked, false);
        
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<CrossRadioBtn, string>(
                p => p.Text, string.Empty);
        
        public EventHandler<EventArg<bool>> CheckedChanged;
        
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<CrossRadioBtn, Color>(
                p => p.TextColor, Color.Black);

        public static readonly BindableProperty IdProperty =
            BindableProperty.Create(nameof(Id), typeof(int), typeof(CrossRadioBtn), default);


        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set
            {
                this.SetValue(CheckedProperty, value);
                var eventHandler = CheckedChanged;
                if (eventHandler != null)
                {
                    EventArg<bool> eventArgs = new EventArg<bool>(value);
                    eventHandler.Invoke(this, eventArgs);
                }
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        public int Id
        {
            get
            {
                return (int)GetValue(IdProperty);
            }
            set => SetValue(IdProperty, value);
        }
    }
}