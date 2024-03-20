using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateTimePage : ContentPage
    {
        DatePicker dp;
        TimePicker tp;
        Label lbl;
        public DateTimePage()
        {
            dp = new DatePicker
            {
                Format = "D",
                MinimumDate = DateTime.Now.AddDays(5),
                MaximumDate = DateTime.Now.AddDays(5),
                TextColor = Color.White,
            };
            dp.DateSelected += Dp_DateSelected; ;

            tp = new TimePicker
            {
                Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
            };
            tp.PropertyChanged += Tp_PropertyChanged;

            lbl = new Label
            {
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            AbsoluteLayout abs = new AbsoluteLayout
            {
                Children = { lbl, dp, tp }
            };
            AbsoluteLayout.SetLayoutBounds(dp, new Rectangle(0.2, 0.3, 300, 100));
            AbsoluteLayout.SetLayoutFlags(dp, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(tp, new Rectangle(0.2, 0.6, 300, 100));
            AbsoluteLayout.SetLayoutFlags(tp, AbsoluteLayoutFlags.PositionProportional);

            Content = abs;
        }

        private void Dp_DateSelected(object sender, DateChangedEventArgs e)
        {
            lbl.Text = e.NewDate.ToString();
        }

        private void Tp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            lbl.Text = "Aeg: " + tp.Time.ToString();
        }
    }
}