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
    public partial class StartPage2 : ContentPage
    {
        List<ContentPage> pages = new List<ContentPage>()
            {
                new EntryPage(), new TimePage(), new BoxPage(), new DateTimePage(), new StepperSliderPage(), new RGBPage(), new LumememmePage(), new Frame_Page(), new TripsTrapsTrull(), new PopUpPage(), new PickerPage(), new Table_Page(), new List_Page(), new Euroopa_riigid()
            };
        List<string> texts = new List<string>()
            {
                "Ava entry","Ava timer leht", "Ava Box leht", "Ava DateTime leht","Ava Stepper Slider Page","Ava RGB leht", "Ava Lumememme leht", "Ava Frame left", "Ava trips traps trull", "Ava PopUp leht", "Ava Picker leht", "Table Page", "List Page", "Euroopa riigid"
            };
            StackLayout st;
        public StartPage2()
        {
            st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Black
            };
            for (int i = 0; i < pages.Count; i++)
            {
                Button button = new Button
                {
                    Text = texts[i],
                    TextColor = Color.White,
                    BackgroundColor = Color.Black,
                    TabIndex = i
                };
                st.Children.Add(button);
                button.Clicked += Button_Clicked;
            }
            ScrollView sv = new ScrollView { Content = st };
            Content = sv;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            await Navigation.PushAsync(pages[btn.TabIndex]);
        }
    }
}