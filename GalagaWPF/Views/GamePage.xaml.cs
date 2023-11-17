using GalagaWPF.Controller;
using GalagaWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GalagaWPF
{
    /// <summary>
    /// Lógica de interacción para GamePage.xaml
    /// </summary>
    public partial class GamePage : Window
    {
        Menu menu;

        bool goLeft, goRight;

        List<Rectangle> itemsToRemove = new List<Rectangle>();

        int enemyImages = 0;
        int bulletTimer = 0;
        int bulletTimerLimit = 90;
        int totalEnemies = 0;
        int enemySpeed = 6;
        bool gameOver = false;

        DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush playerSkin = new ImageBrush();
        public GamePage(Menu menu)
        {
            InitializeComponent();
            
            this.menu = menu;
            this.Show();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            playerSkin.ImageSource = new BitmapImage(new Uri("../../../Resources/player.png", UriKind.Relative));
            player.Fill = playerSkin;

            myCanvas.Focus();
            MakeEnemies(36);


        }

        

        

        private void GameLoop(object? sender, EventArgs e)
        {
            Rect playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            enemiesLeft.Content = "Enemies left: " + totalEnemies;

            if (goLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
            }
            if (goRight == true && Canvas.GetLeft(player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
            }

            bulletTimer -= 3;

            if (bulletTimer < 0)
            {
                EnemyBulletMaker(Canvas.GetLeft(player) + 20, 10);
                bulletTimer = bulletTimerLimit;
            }


            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {    if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemsToRemove.Add(x);
                                itemsToRemove.Add(y);
                                totalEnemies--;
                                 if (totalEnemies == 0)
                                {
                                    ShowGameOver("You win!");
                                }
                            }
                        }
                    }   
                                       
                }
                
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);

                    if (Canvas.GetLeft(x) > 800)
                    {
                        Canvas.SetLeft(x, -80);
                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height +10));
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        ShowGameOver("You were killed by an alien");
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > 480)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyBulletHitBox))
                    {
                        ShowGameOver("You were killed by an alien bullet");
                    }
                }   

            }

            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }

            if (totalEnemies < 0)
            {
                ShowGameOver("Good job you finished this level");
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if (e.Key == Key.Space)
            {
                PlayerBulletMaker();
            }
            
        }



        

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if (e.Key == Key.Enter && gameOver == true)
            {
                GamePage newGame = new GamePage(menu);
                newGame.Show();
                this.Close();
            }
            
        }

        private void PlayerBulletMaker()
        {
            Rectangle newBullet = new Rectangle
            {
                Tag = "bullet",
                Height = 20,
                Width = 5,
                Fill = Brushes.White,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
            };

            Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);
            Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);

            myCanvas.Children.Add(newBullet);
        }   

        private void EnemyBulletMaker(double x, double y)
        {
            Rectangle enemyBullet = new Rectangle
            {
                Tag = "enemyBullet",
                Height = 40,
                Width = 15,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5,

            };

            Canvas.SetTop(enemyBullet, y);
            Canvas.SetLeft(enemyBullet, x);

            myCanvas.Children.Add(enemyBullet);


        }

        private void MakeEnemies(int limit)
        {
            int left = 0;

            totalEnemies = limit;

            for (int i = 0; i < limit; i++)
            {
                ImageBrush enemySkin = new ImageBrush();

                Rectangle newEnemy = new Rectangle
                {
                    Tag = "enemy",
                    Height = 45,
                    Width = 45,
                    Fill = enemySkin,
                };

                Canvas.SetTop(newEnemy, 30); 
                Canvas.SetLeft(newEnemy, left);
                myCanvas.Children.Add(newEnemy);
                left -= 60;
                enemyImages++;
                if (enemyImages > 8)
                {
                    enemyImages = 1;
                }
                switch (enemyImages)
                {
                    case 1:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader1.gif", UriKind.Relative));
                        break;
                    case 2:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader2.gif", UriKind.Relative));
                        break;
                    case 3:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader3.gif", UriKind.Relative));
                        break;
                    case 4:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader4.gif", UriKind.Relative));
                        break;
                    case 5:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader5.gif", UriKind.Relative));
                        break;
                    case 6:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader6.gif", UriKind.Relative));
                        break;
                    case 7:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader7.gif", UriKind.Relative));
                        break;
                    case 8:
                        enemySkin.ImageSource = new BitmapImage(new Uri("../../../Resources/invader8.gif", UriKind.Relative));
                        break;

                }
            }
        }

        private void ShowGameOver(string msg)
        {
            gameOver = true;
            gameTimer.Stop();
            enemiesLeft.Content += " " + msg + " Press Enter to continue playing";

        }


        
        
        
        
        //<-Escape panel->//
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (escapePanel.Visibility == Visibility.Collapsed)
                {
                    escapePanel.Visibility = Visibility.Visible;
                    gameTimer.Stop();
                    myCanvas.Visibility = Visibility.Hidden;
                    escapePanel.Focus();
                }
                else
                {
                    escapePanel.Visibility = Visibility.Collapsed;
                    myCanvas.Visibility = Visibility.Visible;
                    gameTimer.Start();
                    myCanvas.Focus();
                }
            }
        }
        private void GoToMenu_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para ir al menú desde la tabla
            menu.Show();
            this.Hide();
        }
        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para ir al juego desde la tabla
            menu.OpenLoginPage();
            this.Hide();
        }
        private void GoToHighscore_Click(object sender, RoutedEventArgs e)
        {
            menu.OpenHighscorePage();
            this.Hide();
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //<-------------->//
    }
}
