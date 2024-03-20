using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Table_Page : ContentPage
    {
        TableView tableView;
        SwitchCell sc;
        ImageCell ic;
        TableSection fotosection;
        Button call_btn, sms_btn, mail_btn;
        EntryCell tel_nr, email, sonum, teema, nimi;
        public Table_Page()
        {
            sc = new SwitchCell { Text = "Näita veel" };
            sc.OnChanged += Sc_OnChanged;

            ic = new ImageCell
            {
                ImageSource = ImageSource.FromFile("contact.png"),
            };

            fotosection = new TableSection();

            nimi = new EntryCell
            {
                Label = "Nimi: ",
                Keyboard = Keyboard.Default
            };

            tel_nr = new EntryCell
            {
                Label = "Telefon: ",
                Keyboard=Keyboard.Telephone
            };

            email = new EntryCell
            {
                Label = "Email: ",
                Keyboard=Keyboard.Email
            };

            sonum = new EntryCell
            {
                Label = "Sõnum",
                Placeholder = "Kirjutage sõnum",
                Keyboard = Keyboard.Default
            };

            teema = new EntryCell
            {
                Label = "Teema",
                Placeholder = "Kirjutage sõnumi teema",
                Keyboard = Keyboard.Default
            };



            call_btn = new Button
            {
                Text = "Helista",
                HeightRequest = 50,
                WidthRequest = 50
            };
            call_btn.Clicked += Call_btn_Clicked;

            sms_btn = new Button
            {
                Text = "Saata SMS",
                HeightRequest = 50,
                WidthRequest = 50
            };
            sms_btn.Clicked += Sms_btn_Clicked;

            mail_btn = new Button
            {
                Text = "Saada email",
                HeightRequest = 50,
                WidthRequest = 50
            };
            mail_btn.Clicked += Mail_btn_Clicked;

            tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Andemene sisestamine")
                {
                    new TableSection("Põhiandmed: ")
                    {
                        nimi,
                        sc
                    },
                    fotosection,
                    new TableSection("Kontaktandmed: ")
                    {
                        tel_nr,
                        email
                    },
                    new TableSection("Sõnumite saatmine: ")
                    {
                        teema,
                        sonum
                    },
                    new TableSection()
                    {
                        new ViewCell
                        {
                            View = call_btn
                        },
                        new ViewCell
                        {
                            View = sms_btn
                        },
                        new ViewCell
                        {
                            View = mail_btn
                        }
                    }
                }
            };

            Content = tableView;
        }

        private async void Mail_btn_Clicked(object sender, EventArgs e)
        {
            var mail = CrossMessaging.Current.EmailMessenger;
            if(mail.CanSendEmail && !string.IsNullOrEmpty(email.Text))
            {
                mail.SendEmail(email.Text, teema.Text, sonum.Text);
            }
            else
            {
                await DisplayAlert("Email ei sisestatud!", "Peate sisestama email.", "OK");
            }
        }

        private void Sms_btn_Clicked(object sender, EventArgs e)
        {
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms(tel_nr.Text, sonum.Text);
            }    
        }

        private async void Call_btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var call = tel_nr.Text;
                if (!string.IsNullOrWhiteSpace(call))
                {
                    await Launcher.OpenAsync(new Uri("tel:" + call));
                }
                else
                {
                    await DisplayAlert("Telefoninumbrit ei sisestatud!", "Peate sisestama telefoninumbri.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", ex.Message, "OK");
            }
        }

        private void Sc_OnChanged(object sender, ToggledEventArgs e)
        {
            if(e.Value)
            {
                fotosection.Title = "Kontaktinfo";
                ic.Text = "Nimi: " + nimi.Text;
                ic.Detail = $"Telefon: {tel_nr.Text}";
                fotosection.Add(ic);
                sc.Text = "Peida";
            }
            else
            {
                fotosection.Title = "";
                fotosection.Remove(ic);
                sc.Text = "Näita veel";
            }
        }
    }
}