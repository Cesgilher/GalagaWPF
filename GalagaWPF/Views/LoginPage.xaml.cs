using GalagaWPF.Controller;
using GalagaWPF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using static System.Collections.Specialized.BitVector32;


namespace GalagaWPF
{
    public partial class LoginPage : Window
    {
        private Menu menu;

        public LoginPage(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;            
            this.Show();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User user = new User("", "", email, password);
            UserManager.Instance.LogIn(user);

            if (UserManager.Instance.GetSession() != null)
            {
                MessageBox.Show("Inicio de sesión exitoso. Bienvenido, " + UserManager.Instance.GetSession().Name + " " + UserManager.Instance.GetSession().LastName);
                // Puedes cerrar la ventana de inicio de sesión o hacer lo que necesites aquí
                menu.OpenGamePage();
                this.Close();

            }
            else
            {
                MessageBox.Show("Inicio de sesión fallido. Verifica tus credenciales.");
            }

        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string registerName = txtRegisterName.Text;
            string registerLastName = txtRegisterLastName.Text;
            string registerEmail = txtRegisterEmail.Text.ToLower();
            string registerPassword = txtRegisterPassword.Password;

            registerName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(registerName.ToLower());
            registerLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(registerLastName.ToLower());

            User newUser = new User(registerName, registerLastName, registerEmail, registerPassword);

            UserManager.Instance.Register(newUser);

            if (UserManager.Instance.GetSession() != null)
            {
                MessageBox.Show("Registro exitoso. Bienvenido, " + UserManager.Instance.GetSession().Name + " " + UserManager.Instance.GetSession().LastName);

                // Puedes cerrar la ventana de inicio de sesión o hacer lo que necesites aquí
                menu.OpenGamePage();
                this.Close();


            }
            else
            {
                MessageBox.Show("El registro ha fallado. Verifica tus datos e intenta nuevamente.");
            }

        }
        private void btnGoToRegister_Click(object sender, RoutedEventArgs e)
        {
            loginPanel.Visibility = Visibility.Hidden;
            registerPanel.Visibility = Visibility.Visible;
        }
        private void btnGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            loginPanel.Visibility = Visibility.Visible;
            registerPanel.Visibility = Visibility.Hidden;
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
        }//falta por testear

        private void GoToMenu_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para ir al menú desde la tabla
            menu.Show();
            this.Close();
        }

        private void GoToGame_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para ir al juego desde la tabla
            menu.OpenGamePage();
            this.Close();
        }

        private void GoToHighscore_Click(object sender, RoutedEventArgs e)
        {
            menu.OpenHighscorePage();
            this.Close();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }   



    }
}
