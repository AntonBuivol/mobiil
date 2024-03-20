using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        ImageButton btn_back, btn_home, btn_history, btn_redo, btn_favorite;
        Entry ent;
        Picker picker;
        WebView webView;
        List<string> lemmiklehed = new List<string> { "https://google.com" };
        List<string> nimetused = new List<string> { "Google" };
        List<string> history = new List<string> {  };
        string homepage = "https://google.com";
        Grid grid;
        public PickerPage()
        {
            btn_back = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                Source = "undo.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White
            };

            btn_back.Clicked += Btn_back_Clicked;

            btn_redo = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                Source = "redo.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White
            };
            btn_redo.Clicked += Btn_redo_Clicked;

            btn_home = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                Source = "browser.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White
            };
            btn_home.Clicked += Btn_home_Clicked;

            picker = new Picker
            {
                WidthRequest = 200,
                HeightRequest = 50,
                Title = "Lemmiklehed",
            };
            ent = new Entry();
            ent.Completed += Ent_Completed;
            btn_history = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                Source = "history.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White
            };
            btn_history.Clicked += Btn_history_Clicked;
            foreach (string leht in nimetused)
            {
                picker.Items.Add(leht);
            }
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = "https://google.com" },
                HeightRequest = 800,
                WidthRequest = 200,
            };
            btn_favorite = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                Source = "favorite.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White
            };
            btn_favorite.Clicked += Btn_favorite_Clicked;


            grid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { btn_home, btn_back, btn_redo, btn_history }
            };
            grid.Children.Add( btn_home, 0, 0 );
            grid.Children.Add( btn_back, 1, 0 );
            grid.Children.Add(btn_redo, 2, 0 );
            grid.Children.Add(btn_favorite, 3, 0 );
            grid.Children.Add( btn_history, 4, 0 );
            Content = new StackLayout { Children = { grid, picker, ent, webView } };

            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            webView.Navigating += WebView_Navigating;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.NavigationEvent == WebNavigationEvent.Back)
            {
                history.Add(e.Url);
            }
            if (e.NavigationEvent == WebNavigationEvent.Forward)
            {
                history.Add(e.Url);
            }
            ent.Text = e.Url;
        }

        private async void Btn_favorite_Clicked(object sender, EventArgs e)
        {
            if (webView.Source is UrlWebViewSource url)
            {
                string currentUrl = url.Url;

                string nimi = await DisplayPromptAsync("Vali uus nimi", "Uus nimi");

                if (nimi != null && nimi != string.Empty)
                {
                    lemmiklehed.Add(currentUrl);
                    nimetused.Add(nimi);
                    picker.Items.Add(nimi);
                }
            }
        }

        private void Btn_redo_Clicked(object sender, EventArgs e)
        {
            webView.GoForward();
        }

        private async void Btn_history_Clicked(object sender, EventArgs e)
        {
            string[] hist = history.ToArray();
            var url = await DisplayActionSheet("Ajalugu?", "Tagasi", null, hist);
            webView.Source = new UrlWebViewSource { Url = url };
            ent.Text = url;
        }

        private void Ent_Completed(object sender, EventArgs e)
        {
            if (ent.Text == string.Empty)
            {
                return;
            }
            string url = "https://" + ent.Text;
            DisplayAlert("Navigeerimine", $"Ava {url}", "Ok");
            webView.Source = new UrlWebViewSource { Url = url };
            history.Add(url);
        }

        private void Btn_home_Clicked(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = homepage };
            history.Add(homepage);
        }

        private void Btn_back_Clicked(object sender, EventArgs e)
        {
            webView.GoBack();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = lemmiklehed[picker.SelectedIndex] };
            history.Add(lemmiklehed[picker.SelectedIndex]);
        }
    }
}