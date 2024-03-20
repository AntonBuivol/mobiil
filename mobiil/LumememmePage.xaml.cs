using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.GTKSpecific;
using Xamarin.Forms.Xaml;
using BoxView = Xamarin.Forms.BoxView;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LumememmePage : ContentPage
    {
        StackLayout st;
        Label lbl, lblstp;
        Button btn, btnColor, btnHide;
        Stepper stp;
        Slider sld;
        BoxView box, box2, box3_vedro;
        AbsoluteLayout abs;
        Random rnd;
        public LumememmePage()
        {
            InitializeComponent();
            BackgroundColor = Color.Black;
            box = new BoxView
            {
                CornerRadius = 200,
                Color = Color.White,
                WidthRequest = 130,
                HeightRequest = 130,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            box2 = new BoxView
            {
                CornerRadius = 300,
                Color = Color.White,
                WidthRequest = 180,
                HeightRequest = 180,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            box3_vedro = new BoxView
            {
                Color = Color.Gray,
                WidthRequest = 120,
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            lbl = new Label
            {
                Text = "Läbipaistvus: 100%"
            };

            sld = new Slider
            {
                Minimum = 0,
                Maximum = 1,
                Value = 1,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Gray,
                ThumbColor = Color.White
            };
            sld.ValueChanged += Sld_ValueChanged;

            btn = new Button
            {
                Text = "Peida",
                BackgroundColor = Color.Black,
                TextColor = Color.White,

            };
            btn.Clicked += Btn_Clicked;

            btnColor = new Button
            {
                Text = "Juhuslik lumememme värv",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };
            btnColor.Clicked += BtnColor_Clicked;

            btnHide = new Button
            {
                Text = "Sulata",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };
            btnHide.Clicked += BtnHide_Clicked;

            lblstp = new Label
            {
                Text = "Liiguta",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            stp = new Stepper
            {
                Minimum = -50,
                Maximum = 50,
                Increment = 5,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            stp.ValueChanged += Stp_ValueChanged;

            st = new StackLayout
            {
                Children = {lbl, sld, btn, btnColor, btnHide, lblstp, stp }
            };
            //
            abs = new AbsoluteLayout
            {
                Children = { st, box2, box, box3_vedro }
            };
            AbsoluteLayout.SetLayoutBounds(box3_vedro, new Rectangle(0.5, 0.03, 130, 100));
            AbsoluteLayout.SetLayoutFlags(box3_vedro, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(box, new Rectangle(0.5, 0.1, 200, 200));
            AbsoluteLayout.SetLayoutFlags(box, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(box2, new Rectangle(0.5, 0.37, 200, 200));
            AbsoluteLayout.SetLayoutFlags(box2, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(st, new Rectangle(0.5, 0.95, 300, 280));
            AbsoluteLayout.SetLayoutFlags(st, AbsoluteLayoutFlags.PositionProportional);

            Content = abs;
        }

        private void Stp_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.TranslationX = e.NewValue;
            box3_vedro.TranslationX = e.NewValue;
            box2.TranslationX = e.NewValue;
        }

        private async void BtnHide_Clicked(object sender, EventArgs e)
        {
            double opacityValue = box.Opacity;

            while (opacityValue > 0)
            {
                await Task.Run(() => Thread.Sleep(500));
                opacityValue -= 0.1;
                box.Opacity = opacityValue;
                box2.Opacity = opacityValue;
                box3_vedro.Opacity = opacityValue;
            }
        }

        private void BtnColor_Clicked(object sender, EventArgs e)
        {
            rnd = new Random();
            box3_vedro.Color = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)); 
            box.Color = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            box2.Color = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            if (box.IsVisible && box2.IsVisible && box3_vedro.IsVisible)
            {
                box.IsVisible = false;
                box2.IsVisible = false;
                box3_vedro.IsVisible = false;
                sld.Value = 0;
            }
        }

        private void Sld_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.IsVisible = true;
            box2.IsVisible = true;
            box3_vedro.IsVisible = true;
            lbl.Text = String.Format("Läbipaistmatus: {0:F1}%", e.NewValue * 100);
            double opacityValue = e.NewValue;
            box3_vedro.Opacity = opacityValue;
            box.Opacity = opacityValue;
            box2.Opacity = opacityValue;
        }
    }
}