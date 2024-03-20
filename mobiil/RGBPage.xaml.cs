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
    public partial class RGBPage : ContentPage
    {
        Label lblRed, lblGreen, lblBlue;
        Slider sldRed, sldGreen, sldBlue;
        BoxView box;
        Stepper stp;
        Button btn;
        Random rnd;
        public RGBPage()
        {
            InitializeComponent();

            box = new BoxView
            {
                WidthRequest = 400,
                HeightRequest = 300,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            sldRed = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Black,
                ThumbColor = Color.Red
            };
            sldRed.ValueChanged += Sld_ValueChanged;

            sldGreen = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Black,
                ThumbColor = Color.Green
            };
            sldGreen.ValueChanged += Sld_ValueChanged; ;

            sldBlue = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Black,
                ThumbColor = Color.Blue
            };
            sldBlue.ValueChanged += Sld_ValueChanged;

            lblRed = new Label
            {
                Text = "Red = ",
                TextColor = Color.Red,
                BackgroundColor = Color.Black
            };

            lblGreen = new Label
            {
                Text = "Green = ",
                TextColor = Color.Green,
                BackgroundColor = Color.Black
            };

            lblBlue = new Label
            {
                Text = "Blue = ",
                TextColor = Color.Blue,
                BackgroundColor = Color.Black
            };

            stp = new Stepper
            {
                Minimum = 0,
                Maximum = 100,
                Increment = 10,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            stp.ValueChanged += Stp_ValueChanged;

            btn = new Button
            {
                Text = "Random color",
                TextColor = Color.White,
                BackgroundColor = Color.Black
            };
            btn.Clicked += Btn_Clicked;

            AbsoluteLayout abs = new AbsoluteLayout
            {
                Children = { sldRed, sldGreen, sldBlue, box, lblRed, lblGreen, lblBlue, stp, btn }
            };
            AbsoluteLayout.SetLayoutBounds(sldRed, new Rectangle(0.3, 0.5, 300, 50));
            AbsoluteLayout.SetLayoutFlags(sldRed, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(sldGreen, new Rectangle(0.3, 0.6, 300, 50));
            AbsoluteLayout.SetLayoutFlags(sldGreen, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(sldBlue, new Rectangle(0.3, 0.7, 300, 50));
            AbsoluteLayout.SetLayoutFlags(sldBlue, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(box, new Rectangle(0.5, 0.05, 400, 300));
            AbsoluteLayout.SetLayoutFlags(box, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(lblRed, new Rectangle(0.3, 0.55, 300, 50));
            AbsoluteLayout.SetLayoutFlags(lblRed, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(lblGreen, new Rectangle(0.3, 0.65, 300, 50));
            AbsoluteLayout.SetLayoutFlags(lblGreen, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(lblBlue, new Rectangle(0.3, 0.75, 300, 50));
            AbsoluteLayout.SetLayoutFlags(lblBlue, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(stp, new Rectangle(0.5, 0.8, 200, 50));
            AbsoluteLayout.SetLayoutFlags(stp, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(btn, new Rectangle(0.5, 0.9, 200, 50));
            AbsoluteLayout.SetLayoutFlags(btn, AbsoluteLayoutFlags.PositionProportional);

            Content = abs;
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            rnd = new Random();
            box.Color = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        private void Stp_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.CornerRadius = (float)e.NewValue;
        }

        private void Sld_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if(sender == sldRed)
            {
                lblRed.Text = String.Format("Red = {0:X2}", (int)e.NewValue);
            }
            else if (sender == sldGreen)
            {
                lblGreen.Text = String.Format("Green = {0:X2}", (int)e.NewValue);
            }
            else if (sender == sldBlue)
            {
                lblBlue.Text = String.Format("Blue = {0:X2}", (int)e.NewValue);
            }
            box.Color = Color.FromRgb((int)sldRed.Value, (int)sldGreen.Value, (int)sldBlue.Value);
        }
    }
}