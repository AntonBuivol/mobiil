using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Euroopa_riigid : ContentPage
    {
        public ObservableCollection<Euroopa> riigid {  get; set; }
        Label lbl_list;
        ListView list;
        Button lisa, kustuta;
        public Euroopa_riigid()
        {
            riigid = new ObservableCollection<Euroopa>
            {
                new Euroopa { Riik = "Eesti", Pelinn = "Tallinn", Rahvaarv = 1331000, Pilt = "eesti" },
                new Euroopa { Riik = "Läti", Pelinn = "Riia", Rahvaarv = 1884000, Pilt = "latvia" },
                new Euroopa { Riik = "Leedu", Pelinn = "Vilnus", Rahvaarv = 2801000, Pilt = "litva" }

            };

            list = new ListView
            {
                SeparatorColor = Color.Black,
                Header = "Euroopa riigid",
                Footer = DateTime.Now.ToString("t"),

                HasUnevenRows = true,
                ItemsSource = riigid,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.White, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Riik");
                    Binding companyBinding = new Binding { Path = "Pelinn", StringFormat = "Pealinn on {0}"};
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return imageCell;
                })
            };
            list.ItemTapped += List_ItemTapped;

            lisa = new Button { Text = "Lisa riik" };
            kustuta = new Button { Text = "Kustuta riik" };
            lisa.Clicked += Lisa_Clicked;
            kustuta.Clicked += Kustuta_Clicked;



            lbl_list = new Label
            {
                Text = "Riigid",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            
            this.Content = new StackLayout { Children = {lbl_list, list, lisa, kustuta} };
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Euroopa phone = list.SelectedItem as Euroopa;
            if(phone != null)
            {
                riigid.Remove(phone);
                list.SelectedItem = null;
            }
            lbl_list.Text = "Riigid";
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {
            string nimetus = await DisplayPromptAsync("Riik", "Kirjutage riik");
            if(nimetus == null)
            {
                return;
            }
            string tootja = await DisplayPromptAsync("Pealinn", "Kirjuta pealinn");
            if(tootja == null)
            {
                return;
            }
            string hind = await DisplayPromptAsync("Hind", "Kirjuta rahvaarv", keyboard: Keyboard.Numeric);
            if(hind == null)
            {
                return;
            }
            var pilt = await CrossMedia.Current.PickPhotoAsync();
            ImageSource source = ImageSource.FromStream(() => pilt.GetStream());
            if (nimetus != string.Empty && tootja != string.Empty && hind != string.Empty)
            {
                Euroopa telefon = new Euroopa { Riik = nimetus, Pelinn = tootja, Rahvaarv = Convert.ToInt32(hind), Pilt = source };
                if(riigid.Any(x => x.Riik == telefon.Riik))
                {
                    return;
                }

                riigid.Add(telefon);
            }

        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Euroopa selectedPhone = e.Item as Euroopa;
            if(selectedPhone != null)
            {
                await DisplayAlert("Riik", $"{selectedPhone.Riik} \nPealinn: {selectedPhone.Pelinn} \nRahvaarv: {selectedPhone.Rahvaarv}", "OK");
                //lbl_list.Text = $"{selectedPhone.Riik} \nPealinn: {selectedPhone.Pelinn} \nRahvaarv: {selectedPhone.Rahvaarv} eurot";
            }
        }
    }
}