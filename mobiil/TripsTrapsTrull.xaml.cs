using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripsTrapsTrull : ContentPage
    {
        Grid gr;
        Random rnd = new Random();
        Frame fr;
        Label lbl;
        Image krest, krug;
        int[,] board = new int[3,3];
        int[,] bigboard = new int[5,5];
        Button btn_newGame, btn_playwithbot, btnColor;
        public TripsTrapsTrull()
        {
            gr = new Grid
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gr.Children.Add(
                        fr = new Frame
                        {
                            BackgroundColor = Color.White,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand
                        }, i, j
                        );
                    fr.GestureRecognizers.Add(tap);
                }
            }
            btn_newGame = new Button
            {
                Text = "Uus mäng",
                BackgroundColor = Color.Gray,
                TextColor = Color.White,
            };
            btn_newGame.Clicked += Btn_newGame_Clicked;

            gr.Children.Add( btn_newGame, 0, 3 );

            btn_playwithbot = new Button
            {
                Text = "Mäng robotiga",
                BackgroundColor = Color.Gray,
                TextColor = Color.White
            };
            gr.Children.Add(btn_playwithbot, 1, 3);
            btn_playwithbot.Clicked += Btn_PlayWithBot_Clicked;

            btnColor = new Button
            {
                Text = "Muutke mänguvälja suurust",
                BackgroundColor = Color.Gray,
                TextColor = Color.White
            };
            gr.Children.Add(btnColor, 2, 3);
            btnColor.Clicked += BtnColor_Clicked;


            Content = gr;
        }
        bool bigboardornot = false;
        private async void BtnColor_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Milline värv?", "Loobu", "Tavaline suurus", "3x3", "5x5");
            if (action == "3x3")
            {
                Navigation.RemovePage(this);
                await Navigation.PushAsync(new TripsTrapsTrull());
            }
            if (action == "5x5")
            {
                bigboardornot = true;
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += Tap_Tapped;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        gr.Children.Add(
                            fr = new Frame
                            {
                                BackgroundColor = Color.White,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand
                            }, i, j
                            );
                        fr.GestureRecognizers.Add(tap);
                    }
                }
                btn_newGame = new Button
                {
                    Text = "Uus mäng",
                    BackgroundColor = Color.Gray,
                    TextColor = Color.White,
                };
                btn_newGame.Clicked += Btn_newGame_Clicked;

                gr.Children.Add(btn_newGame, 0, 5);

                btn_playwithbot = new Button
                {
                    Text = "Mäng robotiga",
                    BackgroundColor = Color.Gray,
                    TextColor = Color.White
                };
                gr.Children.Add(btn_playwithbot, 1, 5);
                btn_playwithbot.Clicked += Btn_PlayWithBot_Clicked;

                btnColor = new Button
                {
                    Text = "Muutke mänguvälja suurust",
                    BackgroundColor = Color.Gray,
                    TextColor = Color.White
                };
                gr.Children.Add(btnColor, 2, 5);
                btnColor.Clicked += BtnColor_Clicked;
            }
        }

        bool playerTurn = true;
        bool withbot = false;
        private async void Btn_PlayWithBot_Clicked(object sender, EventArgs e)
        {
            bool vali = await DisplayAlert("Mäng robotiga", "Mängid nullidega", "X", "O");
            withbot = true;
            if (vali)
            {
                await DisplayAlert("Mäng robotiga", "Mängid ristidega", "Ok");
            }
            else
            {
                playerTurn = false;
                await DisplayAlert("Mäng robotiga", "Mängid nullidega", "Ok");
                BotMove();
            }
        }

        private async void BotMove()
        {
            bool freeFrame = true;
            if(!bigboardornot)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (board[row, col] > 0)
                        {
                            freeFrame = false;
                        }
                        if (board[row, col] == 0)
                        {
                            freeFrame = true;
                            break;
                        }
                    }
                    if (freeFrame)
                    {
                        break;
                    }
                }
                if (freeFrame && !win)
                {
                    await Task.Delay(300);
                    while (true)
                    {
                        int rida = rnd.Next(0, 3);
                        int column = rnd.Next(0, 3);
                        if (playerTurn)
                        {
                            if (board[rida, column] == 0)
                            {
                                board[rida, column] = 10;

                                Frame fr = GetFrameByRowColumn(rida, column);
                                krest = new Image
                                {
                                    Source = "nolik.png"
                                };
                                fr.Content = krest;
                                checkWin(30);
                                tapcount++;
                                break;
                            }
                        }
                        else
                        {
                            if (board[rida, column] == 0)
                            {
                                board[rida, column] = 1;

                                Frame fr = GetFrameByRowColumn(rida, column);
                                krest = new Image
                                {
                                    Source = "krestik.png"
                                };
                                fr.Content = krest;
                                checkWin(3);
                                tapcount++;
                                break;
                            }
                        }
                    }
            
                }
            }
            if (bigboardornot)
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (bigboard[row, col] > 0)
                        {
                            freeFrame = false;
                        }
                        if (bigboard[row, col] == 0)
                        {
                            freeFrame = true;
                            break;
                        }
                    }
                    if (freeFrame)
                    {
                        break;
                    }
                }
                if (freeFrame && !win)
                {
                    await Task.Delay(300);
                    while (true)
                    {
                        int rida = rnd.Next(0, 5);
                        int column = rnd.Next(0, 5);
                        if (playerTurn)
                        {
                            if (bigboard[rida, column] == 0)
                            {
                                bigboard[rida, column] = 10;

                                Frame fr = GetFrameByRowColumn(rida, column);
                                krest = new Image
                                {
                                    Source = "nolik.png"
                                };
                                fr.Content = krest;
                                checkWin(50);
                                tapcount++;
                                break;
                            }
                        }
                        else
                        {
                            if (bigboard[rida, column] == 0)
                            {
                                bigboard[rida, column] = 1;

                                Frame fr = GetFrameByRowColumn(rida, column);
                                krest = new Image
                                {
                                    Source = "krestik.png"
                                };
                                fr.Content = krest;
                                checkWin(5);
                                tapcount++;
                                break;
                            }
                        }
                    }
                }
            }
            playerTurn = true;
        }

        private Frame GetFrameByRowColumn(int row, int column)
        {
            foreach (var child in gr.Children)
            {
                if (child is Frame fr && Grid.GetRow(fr) == row && Grid.GetColumn(fr) == column)
                {
                    return fr;
                }
            }
            return null;
        }

        private void Btn_newGame_Clicked(object sender, EventArgs e)
        {
            WinAndEnd(0);
        }

        int tapcount = 1;
        private void Tap_Tapped(object sender, EventArgs e)
        {
            Frame fr = (Frame)sender;
            if (fr.Content == null)
            {
                var rida = Grid.GetRow(fr);
                var column = Grid.GetColumn(fr);

                if (tapcount % 2 == 0)
                {
                    krug = new Image
                    {
                        Source = "nolik.png"
                    };
                    fr.Content = krug;
                    if(!bigboardornot)
                    {
                        board[rida, column] = 10;
                        checkWin(30);
                    }

                    if (bigboardornot)
                    {
                        bigboard[rida, column] = 10;
                        checkWin(50);
                    }
                    if (withbot)
                    {
                        playerTurn = false;
                    }
                }
                else
                {
                    krest = new Image
                    {
                        Source = "krestik.png"
                    };
                    fr.Content = krest;
                    if (!bigboardornot)
                    {
                        board[rida, column] = 1;
                        checkWin(3);
                    }


                    if (bigboardornot)
                    {
                        bigboard[rida, column] = 1;
                        checkWin(5);
                    }
                }
                tapcount++;
                if(withbot)
                {
                    BotMove();
                }
            }
        }
        bool draw;
        private void checkWin(int result)
        {
            if (bigboardornot)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (bigboard[i, 0] + bigboard[i, 1] + bigboard[i, 2] + bigboard[i, 3] + bigboard[i, 4] == result)
                    {
                        WinAndEnd(result);
                        return;
                    }

                    if (bigboard[0, i] + bigboard[1, i] + bigboard[2, i] + bigboard[3, i] + bigboard[4, i] == result)
                    {
                        WinAndEnd(result);
                        return;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] + board[i, 1] + board[i, 2] == result)
                {
                    WinAndEnd(result);
                    return;
                }

                if (board[0, i] + board[1, i] + board[2, i] == result)
                {
                    WinAndEnd(result);
                    return;
                }
            }
            if (board[0, 0] + board[1, 1] + board[2, 2] == result || bigboard[0,0] + bigboard[1, 1] + bigboard[2, 2] + bigboard[3, 3] + bigboard[4, 4] == result)
            {
                WinAndEnd(result);
                return;
            }

            if (board[0, 2] + board[1, 1] + board[2, 0] == result || bigboard[0, 4] + bigboard[1, 3] + bigboard[2, 2] + bigboard[3, 1] + bigboard[4, 0] == result)
            {
                WinAndEnd(result);
                return;
            }

            draw = true;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == 0)
                    {
                        draw = false;
                        break;
                    }
                }
                if (!draw)
                {
                    break;
                }
            }
            if (draw)
            {
                WinAndEnd(0);
                return;
            }
        }
        bool win = false;
        private async void WinAndEnd(int result)
        {
            win = true;
            if(result == 3 || result == 5)
            {
                await DisplayAlert("Win", "Ristid võitsid", "Uus mäng");
            }

            if (result == 30 || result == 50)
            {
                await DisplayAlert("Win", "O-d võitsid", "Uus mäng");
            }
            if(draw)
            {
                await DisplayAlert("Win", "Võitjaid pole", "Uus mäng");
            }
            withbot = false;
            Navigation.RemovePage(this);
            await Navigation.PushAsync(new TripsTrapsTrull());
        }
    }
}