﻿using GalagaWPF.Controller;
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
    /// Lógica de interacción para HighscorePage.xaml
    /// </summary>
    public partial class HighscorePage : Window
    {
        public HighscorePage()
        {
            Scoreboard scoreboard = new Scoreboard();

            DataContext = scoreboard.GetScores();

            InitializeComponent();
        }
    }
}