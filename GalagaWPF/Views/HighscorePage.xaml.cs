using GalagaWPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GalagaWPF
{
    public partial class HighscorePage : Window
    {
        private Menu menu;
        public HighscorePage(Menu menu)
        {
            Scoreboard scoreboard = new Scoreboard();

            DataContext = scoreboard.GetHighscores();

            this.menu = menu;

            InitializeComponent();
            
            this.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (escapePanel.Visibility == Visibility.Collapsed)
                {
                    escapePanel.Visibility = Visibility.Visible;
                    highscorePanel.Visibility = Visibility.Hidden;
                }
                else
                {
                    escapePanel.Visibility = Visibility.Collapsed;
                    highscorePanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void GoToMenu_Click(object sender, RoutedEventArgs e)
        {
            menu.Show();
            this.Close();
        }

        

       

        private void GoToGame_Click(object sender, RoutedEventArgs e)
        {
            menu.OpenGamePage();
            this.Close();
        }

        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            menu.OpenLoginPage();
            this.Close();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
