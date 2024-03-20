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
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            Button Entry_btn = new Button
            {
                Text = "Entry leht",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            Button StartPage2 = new Button
            {
                Text = "Start Page 2.0",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            Button DateTime_btn = new Button
            {
                Text = "Date Time",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Gray,
                Children = { Entry_btn, StartPage2, DateTime_btn }
            };
            Content = st;
            Entry_btn.Clicked += Entry_btn_Clicked;
            StartPage2.Clicked += StartPage2_Clicked;
            DateTime_btn.Clicked += DateTime_btn_Clicked;
        }

        private async void DateTime_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DateTimePage());
        }

        private async void StartPage2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage2());
        }

        private async void Entry_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryPage());
        }

    }
}