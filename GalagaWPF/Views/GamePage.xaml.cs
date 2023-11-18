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
        internal Scoreboard scoreboard = new Scoreboard();
        internal bool goLeft, goRight;
        internal List<Rectangle> itemsToRemove;
        internal List<Rectangle> enemies;
        internal Rect playerHitBox;
        internal int bulletTimer;
        internal int bulletTimerLimit = 90;
        internal int totalEnemies;
        internal int enemySpeed;
        internal int enemyLimit;
        internal bool levelOver;
        internal int level;
        internal int punctuation;
        internal DispatcherTimer gameTimer;
        public GamePage(Menu menu)
        {
            InitializeComponent();

            this.menu = menu;
            this.Show();
            myCanvas.Focus();

            InitGame();







        }
        //Game logic//
        private void ClearScreen()
        {
            foreach (var item in myCanvas.Children.OfType<Rectangle>().ToList())
            {
                myCanvas.Children.Remove(item);
            }
        }
        private void ResetPlayerShip()
        {
            myCanvas.Children.Add(player);
            Canvas.SetLeft(player, 370);
            Canvas.SetTop(player, 409);
        }
        private void AddEnemies(int limit)
        {
            enemies = ShipManager.CreateEnemies(limit);

            foreach (Rectangle enemy in enemies)
            {
                myCanvas.Children.Add(enemy);
            }

            totalEnemies = limit;


        }
        private async Task ShowBeginMsg()
        {
            countdownMessage.Visibility = Visibility.Visible;

            for (int i = 3; i > 0; i--)
            {
                countdownMessage.Text = i.ToString();
                await Task.Delay(1000);
            }
            countdownMessage.Visibility = Visibility.Collapsed;
        }
        private void RemoveItems()
        {
            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }
        }
        private void ShowLevelOver(string msg)
        {
            levelOver = true;
            if (totalEnemies == 0)
            {
                punctuation += 50;
                gameTimer.Stop();
                enemiesLabel.Content = " " + msg + " Press Enter to continue playing";
            }
            else
            {
                SaveScore();
                gameTimer.Stop();
                enemiesLabel.Content = " " + msg + " Press Enter to restart";
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
        private void UpdatePlayerShot()
        {
            if (bulletTimer < 0)
            {
                myCanvas.Children.Add(ProjectileManager.EnemyBulletMaker(Canvas.GetLeft(player) + 20, 10));
                bulletTimer = bulletTimerLimit;
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
                ShowLevelOver("You were killed by an alien bullet");
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
                ShowLevelOver("You were killed by an alien");
            }
        }
        private void CheckBulletHitEnemy(Rectangle bullet, Rectangle enemy)
        {
            if ((string)enemy.Tag == "enemy") // Verifica si el enemigo no ha sido eliminado
            {
                Rect bulletHitBox = new Rect(Canvas.GetLeft(bullet), Canvas.GetTop(bullet), bullet.Width, bullet.Height);
                Rect enemyHit = new Rect(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);

                if (bulletHitBox.IntersectsWith(enemyHit))
                {
                    itemsToRemove.Add(bullet);
                    itemsToRemove.Add(enemy);
                    totalEnemies--;
                    punctuation++;
                    enemy.Tag = "dead";
                }
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
        private void GameLoop(object? sender, EventArgs e)
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            enemiesLabel.Content = "Enemies left: " + totalEnemies;
            scoreLabel.Content = "Score: " + punctuation;

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
                ShowLevelOver("Good job you finished this level");
            }
        }
        private async void InitLevel()
        {
            ClearScreen();
            ResetPlayerShip();

            level++;
            enemyLimit += 2;
            enemySpeed += level % 2 == 1 ? 1 : 0;

            levelLabel.Content = "Level: " + level;
            AddEnemies(enemyLimit);
            await ShowBeginMsg();
            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }
        private void InitGame()
        {
            punctuation = 0;
            level = 0;
            enemySpeed = 6;
            enemyLimit = 6;
            levelOver = false;

            player.Fill = ShipManager.CreatePlayerShip();
            itemsToRemove = new List<Rectangle>();
            enemies = new List<Rectangle>();
            InitLevel();
        }
        private void SaveScore()
        {
            Score score = new Score(UserManager.Instance.GetSession().Id, punctuation, level);

            scoreboard.SaveScore(score);

        }
        //End game logic//  
        
        
        //Key events//
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
            if (e.Key == Key.Enter && levelOver == true)
            {
                if (totalEnemies == 0)
                {
                    levelOver = false;
                    InitLevel();
                }
                else 
                {
                    InitGame();
                }
            }

        }
        //End Key events//

       
        
        //Escape panel//
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
        //End Escape panel//
    }
}
