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
        internal Menu menu;

        internal bool goLeft, goRight;

        internal List<Rectangle> itemsToRemove = new List<Rectangle>();
        internal List<Rectangle> enemies = new List<Rectangle>();

        internal Rect playerHitBox;


        internal int bulletTimer = 0;
        internal int bulletTimerLimit = 90;
        internal int totalEnemies = 0;
        internal int enemySpeed = 6;
        internal bool gameOver = false;

        internal DispatcherTimer gameTimer = new DispatcherTimer();
        public GamePage(Menu menu)
        {
            InitializeComponent();

            this.menu = menu;
            this.Show();

            InitializeGameAsync();







        }





        private void GameLoop(object? sender, EventArgs e)
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            enemiesLeft.Content = "Enemies left: " + totalEnemies;
            
            UpdatePlayerPosition();

            bulletTimer -= 3;

            UpdatePlayerShot();

            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {   
                if ((string)x.Tag == "bullet")
                {
                   UpdatePlayerBullet(x);
                }
                if ((string)x.Tag == "enemy")
                {
                    UpdateEnemy(x);
                }
                if ((string)x.Tag == "enemyBullet")
                {
                    UpdateEnemyBullet(x);
                }

            }

            RemoveItems();

            if (totalEnemies < 1)
            {
                ShowGameOver("Good job you finished this level");
            }
        }

        private void RemoveItems()
        {
            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }
        }


        private void UpdatePlayerBullet(Rectangle bullet) 
        {
            Canvas.SetTop(bullet, Canvas.GetTop(bullet) - 20);

            if (Canvas.GetTop(bullet) < 10)
            {
                itemsToRemove.Add(bullet);
            }

            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)y.Tag == "enemy")
                {
                    CheckBulletHitEnemy(bullet, y);
                }
            }


        }

        private void CheckBulletHitEnemy(Rectangle bullet, Rectangle enemy)
        {
            Rect bulletHitBox = new Rect(Canvas.GetLeft(bullet), Canvas.GetTop(bullet), bullet.Width, bullet.Height);
            Rect enemyHit = new Rect(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);

            if (bulletHitBox.IntersectsWith(enemyHit))
            {
                itemsToRemove.Add(bullet);
                itemsToRemove.Add(enemy);
                totalEnemies--;

                if (totalEnemies == 0)
                {
                    ShowGameOver("You win!");
                }
            }
        }
        private void UpdateEnemyBullet(Rectangle enemyBullet)
        {
            Canvas.SetTop(enemyBullet, Canvas.GetTop(enemyBullet) + 10);

            if (Canvas.GetTop(enemyBullet) > 480)
            {
                itemsToRemove.Add(enemyBullet);
            }

            Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(enemyBullet), Canvas.GetTop(enemyBullet), enemyBullet.Width, enemyBullet.Height);

            if (playerHitBox.IntersectsWith(enemyBulletHitBox))
            {
                ShowGameOver("You were killed by an alien bullet");
            }
        }
        private void UpdateEnemy(Rectangle enemy)
        {
            Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) + enemySpeed);

            if (Canvas.GetLeft(enemy) > 800)
            {
                Canvas.SetLeft(enemy, -80);
                Canvas.SetTop(enemy, Canvas.GetTop(enemy) + (enemy.Height + 10));
            }

            Rect enemyHitBox = new Rect(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);

            if (playerHitBox.IntersectsWith(enemyHitBox))
            {
                ShowGameOver("You were killed by an alien");
            }
        }
        private void UpdatePlayerShot()
        {
            if (bulletTimer < 0)
            {
                myCanvas.Children.Add(ProjectileManager.EnemyBulletMaker(Canvas.GetLeft(player) + 20, 10));
                bulletTimer = bulletTimerLimit;
            }
        }
        private void UpdatePlayerPosition()
        {
            if (goLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
            }
            if (goRight == true && Canvas.GetLeft(player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
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
                myCanvas.Children.Add(ProjectileManager.PlayerBulletMaker(Canvas.GetTop(player), Canvas.GetLeft(player), player.Width));
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


        private async void InitializeGameAsync()
        {
            myCanvas.Focus();
            player.Fill = ShipManager.CreatePlayerShip();

            // Espera a que se completen la creación de enemigos
            AddEnemiesAsync(36);

            // Inicia el temporizador del juego después de que todos los enemigos se hayan creado
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }


        private void AddEnemiesAsync(int limit)
        { 
            enemies = ShipManager.CreateEnemies(limit);

            foreach (Rectangle enemy in enemies)
            {
                myCanvas.Children.Add(enemy);
            }

            totalEnemies = limit;

           
        }
        private void ShowGameOver(string msg)
        {
            gameOver = true;
            gameTimer.Stop();
            enemiesLeft.Content = " " + msg + " Press Enter to continue playing";

        }

























        //for (int i = 3; i > 0; i--)
        //{
        //    countdownMessage.Text = i.ToString();
        //    await Task.Delay(1000);
        //}

        //countdownMessage.Visibility = Visibility.Collapsed;





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
