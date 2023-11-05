using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalagaWPF.Models
{
    public class Projectile
    {
        private int id;
        private object png;

        public Projectile(int id, object png)
        {
            this.id = id;
            this.png = png;
        }
        

        public int Id { get => id; set => id = value; }
        public object Png { get => png; set => png = value; }
    }
}
