using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using GalagaWPF.Models;
using System.Windows.Shapes;

namespace GalagaWPF.Controller
{
    public static class ShipManager
    {


        public static ImageBrush CreatePlayerShip()
        {
            ImageBrush playerSkin = new ImageBrush();
            playerSkin.ImageSource = new BitmapImage(new Uri("../../../Resources/player.png", UriKind.Relative));
            return playerSkin;
        }
        public static List<Rectangle> CreateEnemies(int limit)
        {
            List<Rectangle> enemies = new List<Rectangle>();
            int left = 0;

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
                enemies.Add(newEnemy);

                left -= 60;
                int enemyImages = i % 8 + 1; // Simplificado para obtener valores de 1 a 8

                string imageSource = $"../../../Resources/invader{enemyImages}.gif";
                enemySkin.ImageSource = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            }

            return enemies;
        }
        


    }
}

