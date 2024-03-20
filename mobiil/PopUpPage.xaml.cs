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
    public partial class PopUpPage : ContentPage
    {
        Button alertButton, alertYesNoButton, alertListButton, questButton;
        public PopUpPage()
        {
            alertButton = new Button
            {
                Text = "Teade",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertButton.Clicked += AlertButton_Clicked;

            alertYesNoButton = new Button
            {
                Text = "Jah või ei",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertYesNoButton.Clicked += AlertYesNoButton_Clicked;

            alertListButton = new Button
            {
                Text = "Valik",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertListButton.Clicked += AlertListButton_Clicked;

            questButton = new Button
            {
                Text = "Küsims",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            questButton.Clicked += QuestButton_Clicked;

            Content = new StackLayout { Children = { alertButton, alertYesNoButton, alertListButton, questButton } };
        }

        private async void QuestButton_Clicked(object sender, EventArgs e)
        {
            string result1 = await DisplayPromptAsync("Küsimus", "Kuidas läheb", placeholder: "Tore");
            string result2 = await DisplayPromptAsync("Vasta", "Millega võrdub 5 + 5", initialValue: "10", maxLength: 2, keyboard: Keyboard.Numeric);
        }

        private async void AlertListButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Mida teha?", "Loobu", "Kustutada", "Tantsida", "Laulda", "Joonestada");
        }

        private async void AlertYesNoButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Kinnitus", "Kas oled kindel?", "Olen kindel", "Ei ole kindel");
            await DisplayAlert("Teade", "Teie valik on: " + (result ? "Jah" : "Ei"), "OK");
        }

        private void AlertButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Teade", "Teil on uus teade", "OK");
        }
    }
}