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
        public Menu()
        {
            InitializeComponent();          
            

        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            OpenGameWindow(null);
            
        }

        private void setUserButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage gamePage = new LoginPage(this);
            gamePage.Show();
            this.Close();
        }
        private void seeHighscoreButton_Click(object sender, RoutedEventArgs e)
        {
            HighscorePage highscorePage= new HighscorePage();
            highscorePage.Show();
            this.Close();


        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void OpenGameWindow(User session)
        {
            GamePage gamePage = new GamePage(session,this);
            this.Hide();
            
        }        

    }
}
