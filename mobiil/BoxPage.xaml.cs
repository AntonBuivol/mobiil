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
    public partial class BoxPage : ContentPage
    {
        int cliks;
        int r=0, g=0, b=0;
        BoxView box;
        Label lbl;
        public BoxPage()
        {
            InitializeComponent();
            box = new BoxView
            {
                Color = Color.FromRgb(r, g, b),
                CornerRadius = 10,
                WidthRequest = 200,
                HeightRequest = 400,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            
            lbl = new Label
            {
                Text = "Clicks " + Convert.ToString(cliks),
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            box.GestureRecognizers.Add(tap);
            StackLayout st = new StackLayout { Children = {Tagasi_btn, box, lbl } };
            Content = st;

        }

        private async void Tagasi_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        Random rnd;
        private void Tap_Tapped(object sender, EventArgs e)
        {
            rnd = new Random();
            box.Color= Color.FromRgb(rnd.Next(0, 255), rnd.Next(0,255), rnd.Next(0,255));
            lbl.Text = "Clicks " + Convert.ToString(cliks);
            cliks++;
        }
    }
}