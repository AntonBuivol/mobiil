using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePage : ContentPage
    {
        public TimePage()
        {
            InitializeComponent();
            BackgroundColor = Color.Black;
        }

        bool must_teema = true;
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (must_teema)
            {
                must_teema = false;
                lbl.BackgroundColor = Color.White;
                lbl.TextColor = Color.Black;
                Time_run.BackgroundColor = Color.White;
                Time_run.TextColor = Color.Black;
                BackgroundColor = Color.White;
                lbl_time.TextColor = Color.Black;
                lbl_time.BackgroundColor = Color.White;
                Tagasi_btn.BackgroundColor = Color.White;
                Tagasi_btn.TextColor = Color.Black;
            }
            else
            {
                must_teema = true;

                lbl.BackgroundColor = Color.Black;
                lbl.TextColor = Color.White;
                Time_run.BackgroundColor = Color.Black;
                Time_run.TextColor = Color.White;
                BackgroundColor = Color.Black;
                lbl_time.TextColor = Color.White;
                lbl_time.BackgroundColor = Color.Black;
                Tagasi_btn.BackgroundColor = Color.Black;
                Tagasi_btn.TextColor = Color.White;
            }
            
        }

        bool flag = false;
        public async void NaitaAeg()
        {
            while (flag) 
            {
                lbl_time.Text = DateTime.Now.ToString("f");
                Time_run.Text = DateTime.Now.ToString("T");
                await Task.Delay(1000);
            }
        }

        private void Time_run_Clicked(object sender, EventArgs e)
        {
            if (flag)
            {
                flag= false;
            }
            else
            {
                flag = true;
                NaitaAeg();
            }
        }

        private async void Tagasi_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}