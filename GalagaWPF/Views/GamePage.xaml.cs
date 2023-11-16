using GalagaWPF.Controller;
using GalagaWPF.Models;
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
    /// <summary>
    /// Lógica de interacción para GamePage.xaml
    /// </summary>
    public partial class GamePage : Window
    {
        Menu menu;
        public GamePage(Menu menu)
        {
            InitializeComponent();
            
            this.menu = menu;
            this.Show();
            
           
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (escapePanel.Visibility == Visibility.Collapsed)
                {
                    escapePanel.Visibility = Visibility.Visible;
                }
                else
                {
                    escapePanel.Visibility = Visibility.Collapsed;
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
    }
}
