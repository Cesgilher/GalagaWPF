using GalagaConC_.Controller;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();

            UserManager um = new UserManager();
            var users =um.GetUsers();
            foreach (User u in users)
            {
                Console.WriteLine(u.Name);
            }
           
            

        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            GamePage gamePage = new GamePage();
            gamePage.Show();
            this.Close();
        }

        private void setUserButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage gamePage = new LoginPage();
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

       


        //    interface Menu
        //    {

        //        public void Play() { }
        //        public void SeeScoreboard() { }

        //        public void SetUser() { }

        //    }
        //}

    }
}
