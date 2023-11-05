using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalagaWPF.Models
{
    public class Alien : Ship
    {
        public int Health { get; set; }

        public Alien(int id, object png) : this(id, png, 1)
        { }
        public Alien(int id, object png, int health = 1)
        {
            Id = id;
            Png = png;     
            Health = health;



        }
    }

}
