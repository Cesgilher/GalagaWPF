﻿using GalagaWPF.Controller;
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
            if (UserManager.Instance.GetSession() == null)
            {
                // Si session es null, crea y muestra la ventana LoginPage
                LoginPage loginPage = new LoginPage(menu);
           
                
            }
            else
            {
                this.Show();
            }

           
        }
    }
}
