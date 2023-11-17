using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GalagaWPF.Models;

namespace GalagaWPF.Controller
{
    public static class ProjectileManager
    {





        public static Rectangle PlayerBulletMaker(double playerTop, double playerLeft, double playerWidth)
        {
            Rectangle bullet = new Rectangle
            {
                Tag = "bullet",
                Height = 20,
                Width = 5,
                Fill = Brushes.White,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
            };

            Canvas.SetTop(bullet, playerTop - bullet.Height);
            Canvas.SetLeft(bullet, playerLeft + playerWidth / 2);

            return bullet;
        }

        public static Rectangle EnemyBulletMaker(double x, double y)
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

            return enemyBullet;
        }
    }
}

