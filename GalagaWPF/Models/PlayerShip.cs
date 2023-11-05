using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalagaWPF.Models
{
    public class PlayerShip : Ship
    {
        bool Shield { get; set; }

        public PlayerShip(int id, object png) : this(id, png, true)
        { }
        public PlayerShip(int id, object png, bool shield = true)
        {
            Id = id;
            Png = png;         
            Shield = shield;



        }
    }
}
