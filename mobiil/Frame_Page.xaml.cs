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
    public partial class Frame_Page : ContentPage
    {
        Grid gr;
        Random rnd = new Random();
        Frame fr;
        Label lbl;
        Image image;
        Switch sw;
        public Frame_Page()
        {
            gr = new Grid
            {
                BackgroundColor = Color.Green,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            tap.NumberOfTapsRequired = 2;
            
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    gr.Children.Add(
                        fr = new Frame { BackgroundColor = Color.FromRgb(rnd.Next(0,255), rnd.Next(0,255), rnd.Next(0, 255)),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand
                        }, i, j
                        );
                    fr.GestureRecognizers.Add(tap);
                }
            }
            lbl = new Label
            {
                Text = "Tekst",
                FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label))
            };
            gr.Children.Add( lbl, 0, 6 );
            Grid.SetColumnSpan(lbl, 10);

            image = new Image
            {
                Source = "CImage.png",
                IsVisible = false
            };
            sw = new Switch
            {
                IsToggled = false,
            };
            sw.Toggled += Sw_Toggled;

            gr.Children.Add(sw, 0, 7);
            gr.Children.Add(image, 1, 7);

            Content = gr;
            
        }

        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                image.IsVisible = true;
            }
            else
            {
                image.IsVisible = false;
            }
            
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            Frame fr = (Frame)sender;
            var rida = Grid.GetRow(fr);
            var column = Grid.GetColumn(fr);
            fr.IsVisible = false;
            lbl.Text = "Rida: " + rida + "\n Veerg: " + column;
        }
    }
}