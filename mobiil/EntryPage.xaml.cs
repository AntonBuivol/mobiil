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
    public partial class EntryPage : ContentPage
    {
        Label lbl;
        Editor editor;
        public EntryPage()
        {

            Button Tagasi_btn = new Button
            {
                Text = "Tagasi Start lehele",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            lbl = new Label
            {
                Text = "Mingi tekst",
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
            };

            editor = new Editor
            {
                Placeholder = "Sisesta siia tekst... ",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            Button Time_btn = new Button
            {
                Text = "Time Leht",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            Button Box_btn = new Button
            {
                Text = "Box Leht",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };

            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Gray,
                Children = { lbl,Tagasi_btn,editor, Time_btn, Box_btn },
                VerticalOptions = LayoutOptions.FillAndExpand  
            };

            Content = st;
            Tagasi_btn.Clicked += Tagasi_btn_Clicked;
            editor.TextChanged += Editor_TextChanged;
            Time_btn.Clicked += Time_btn_Clicked;
            Box_btn.Clicked += Box_btn_Clicked;
        }

        private async void Box_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BoxPage());
        }

        private async void Time_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimePage());
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl.Text = editor.Text;
        }

        private async void Tagasi_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}