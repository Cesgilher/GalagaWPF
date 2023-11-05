using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalagaWPF.Models;

namespace GalagaWPF.Controller
{
    public class Game
    {
        private int level;
        private int score;
        private int lives;
        private Background background;
        private Board board;

        public int Level { get => level; set => level = value; }
        public int Score { get => score; set => score = value; }
        public int Lives { get => lives; set => lives = value; }
        public Background Background { get => background; set => background = value; }
        public Board Board { get => board; set => board = value; }

        public void Stop() { }
        public void Resume() { }
        public void Exit() { }
        public void SaveScore() { }
        public void CheckLives() { }
        public void SetLives() { }
        public void SetBackground() { }


    }
}
