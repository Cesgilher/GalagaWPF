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
    }
}
