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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GalagaWPF
{
    
    public partial class Menu : Window
    {
        private GamePage gamePage;
        public Menu()
        {
            InitializeComponent();          
            

        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            OpenGamePage();
            
        }

        private void setUserButton_Click(object sender, RoutedEventArgs e)
        {
            OpenLoginPage();
        }
        private void seeHighscoreButton_Click(object sender, RoutedEventArgs e)
        {
            OpenHighscorePage();


        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void OpenGamePage()
        {
            this.Hide();
            if (UserManager.Instance.GetSession() == null)
            {
                OpenLoginPage();
            }
            else if (gamePage == null)
            {
                gamePage = new GamePage(this);
            }
            else { gamePage.Show(); }

        }

        public void OpenHighscorePage()
        {
            this.Hide();
            HighscorePage highscorePage = new(this);
        }
        public void OpenLoginPage()
        {
            this.Hide();
            LoginPage loginPage = new(this);
        }

    }
}
