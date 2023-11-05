using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalagaWPF.Models
{
    public abstract class Ship
    {
        private int id;
        private object png;
        

        public int Id { get => id; set => id = value; }
        public object Png { get => png; set => png = value; }
    }
}
