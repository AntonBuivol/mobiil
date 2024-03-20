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
    public partial class StepperSliderPage : ContentPage
    {
        Label lbl;
        Slider sld;
        Stepper stp;
        int r = 0, g = 0, b = 0;
        Random rnd;

        public StepperSliderPage()
        {
            InitializeComponent();
            lbl = new Label
            {
                Text = ".......",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.FromRgb(r, g, b)
            };

            sld = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 30,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Black,
                ThumbColor = Color.Red
            };
            sld.ValueChanged += Sld_ValueChanged;

            stp = new Stepper
            {
                Minimum = 0,
                Maximum = 100,
                Increment = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            stp.ValueChanged += Stp_ValueChanged; ;

            AbsoluteLayout abs = new AbsoluteLayout
            {
                Children = { sld, lbl, stp }
            };
            AbsoluteLayout.SetLayoutBounds(sld, new Rectangle(0.2, 0.1, 300, 50));
            AbsoluteLayout.SetLayoutFlags(sld, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(stp, new Rectangle(0.2, 0.8, 300, 100));
            AbsoluteLayout.SetLayoutFlags(stp, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(lbl, new Rectangle(0.2, 0.4, 300, 300));
            AbsoluteLayout.SetLayoutFlags(lbl, AbsoluteLayoutFlags.PositionProportional);

            Content = abs;
        }

        private void Stp_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            rnd = new Random();
            lbl.Text = String.Format("Valitud: {0:F1}", e.NewValue);
            lbl.FontSize = e.NewValue;
            lbl.Rotation = e.NewValue;
            lbl.TextColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        private void Sld_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            rnd = new Random();
            lbl.Text = String.Format("Valitud: {0:F1}", e.NewValue);
            lbl.FontSize = e.NewValue;
            lbl.Rotation = e.NewValue;
            lbl.TextColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }
    }
}