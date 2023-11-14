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


namespace GalagaWPF
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private UserManager userManager;

        public LoginPage()
        {
            InitializeComponent();
            userManager = new UserManager();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User user = new User(0, "", "", email, password);
            User session = userManager.LogIn(user);

            if (session != null)
            {
                MessageBox.Show("Inicio de sesión exitoso. Bienvenido, " + session.Name + " " + session.LastName);
                // Puedes cerrar la ventana de inicio de sesión o hacer lo que necesites aquí
                this.Close();
            }
            else
            {
                MessageBox.Show("Inicio de sesión fallido. Verifica tus credenciales.");
            }

        }

        private void btnGoToRegister_Click(object sender, RoutedEventArgs e)
        {
            loginPanel.Visibility = Visibility.Hidden;
            registerPanel.Visibility = Visibility.Visible;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string registerName = txtRegisterName.Text;
            string registerLastName = txtRegisterLastName.Text;
            string registerEmail = txtRegisterEmail.Text.ToLower();
            string registerPassword = txtRegisterPassword.Password;

            registerName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(registerName.ToLower());
            registerLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(registerLastName.ToLower());

            User newUser = new User(0, registerName, registerLastName, registerEmail, registerPassword);

            User registeredUser = userManager.Register(newUser);

            if (registeredUser != null)
            {
                MessageBox.Show("Registro exitoso. Bienvenido, " + registeredUser.Name + " " + registeredUser.LastName);

                // Puedes cerrar la ventana de inicio de sesión o hacer lo que necesites aquí
                this.Close();
            }
            else
            {
                MessageBox.Show("El registro ha fallado. Verifica tus datos e intenta nuevamente.");
            }

        }

        private void btnGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            loginPanel.Visibility = Visibility.Visible;
            registerPanel.Visibility = Visibility.Hidden;
        }
    }
}
