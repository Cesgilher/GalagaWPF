using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalagaWPF.Models;

namespace GalagaWPF.Controller
{
    public class Board //La matriz con las naves
    {
        private int id;
        private List<Alien> aliens = new List<Alien>();
        private PlayerShip playerShip;

        public int Id { get => id; set => id = value; }
        public List<Alien> Aliens { get => aliens; set => aliens = value; }
        public PlayerShip PlayerShip { get => playerShip; set => playerShip = value; }


        public void CreateShip() { }
        public void Update() { }
        public void DeleteShip() { }
        public void CheckEnemyShip(Ship ship) { }
        public void Shoot(Ship ship) { }
        public void MoveShip(Ship ship) { }
        public void MoveAliens() { }

    }
}
