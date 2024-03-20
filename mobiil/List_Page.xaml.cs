using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List_Page : ContentPage
    {
        public ObservableCollection<Telefon> telefons {  get; set; }
        Label lbl_list;
        ListView list;
        Button lisa, kustuta;
        public List_Page()
        {
            telefons = new ObservableCollection<Telefon>
            {
                new Telefon { Nimetus = "Samsung Galaxy S20 Ultra", Tootja = "Samsung", Hind = 1349, Pilt = "SamsungGalaxyS20Ultra" },
                new Telefon { Nimetus = "Xiaomi Mi 11 Lite 5G NE", Tootja = "Xiaomi", Hind = 399, Pilt = "XiaomiMi11Lite5GNE" },
                new Telefon { Nimetus = "Xiaomi Mi 11 Lite 5G", Tootja = "Xiaomi", Hind = 339, Pilt = "XiaomiMi11Lite5G" },
                new Telefon { Nimetus = "iPhone 13", Tootja = "Apple", Hind = 1179, Pilt = "iPhone13"}

            };

            list = new ListView
            {
                SeparatorColor = Color.Orange,
                Header = "Minu oma kolektion",
                Footer = DateTime.Now.ToString("t"),

                HasUnevenRows = true,
                ItemsSource = telefons,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Tootja", StringFormat = "Tore telefon firmalt {0}"};
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return imageCell;
                })
            };
            list.ItemTapped += List_ItemTapped;

            lisa = new Button { Text = "Lisa telefon" };
            kustuta = new Button { Text = "Kustuta telefon" };
            lisa.Clicked += Lisa_Clicked;
            kustuta.Clicked += Kustuta_Clicked;



            lbl_list = new Label
            {
                Text = "Telefonide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            
            this.Content = new StackLayout { Children = {lbl_list, list, lisa, kustuta} };
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Telefon phone = list.SelectedItem as Telefon;
            if(phone != null)
            {
                telefons.Remove(phone);
                list.SelectedItem = null;
            }
            lbl_list.Text = "Telefonide loetelu";
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {
            string nimetus = await DisplayPromptAsync("Nimetus", "Kirjuta nimetus");
            if(nimetus == null)
            {
                return;
            }
            string tootja = await DisplayPromptAsync("Tootja", "Kirjuta tootja");
            if(tootja == null)
            {
                return;
            }
            string hind = await DisplayPromptAsync("Hind", "Kirjuta hind", keyboard: Keyboard.Numeric);
            if(hind == null)
            {
                return;
            }
            var pilt = await CrossMedia.Current.PickPhotoAsync();
            ImageSource source = ImageSource.FromStream(() => pilt.GetStream());
            if (nimetus != string.Empty && tootja != string.Empty && hind != string.Empty)
            {
                Telefon telefon = new Telefon { Nimetus = nimetus, Tootja = tootja, Hind = Convert.ToInt32(hind), Pilt = source };
                if(telefons.Any(x => x.Nimetus == telefon.Nimetus))
                {
                    return;
                }

                telefons.Add(telefon);
            }

        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Telefon selectedPhone = e.Item as Telefon;
            if(selectedPhone != null)
            {
                await DisplayAlert("Vali model", $"{selectedPhone.Tootja} - {selectedPhone.Nimetus} \n{selectedPhone.Hind} eurot", "OK");
                //lbl_list.Text = $"{selectedPhone.Tootja} - {selectedPhone.Nimetus} \n{selectedPhone.Hind} eurot";
            }
        }
    }
}