using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GalagaWPF.Models
{
    public class Score
    {
        private int id;
        private int idUser;
        private int points;
        private int level;




        public Score(int id, int idUser, int points, int level)
        {
            this.id = id;
            this.idUser = idUser;
            this.points = points;
            this.level = level;
        }

        public int Id { get => id; set => id = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public int Points { get => points; set => points = value; }
        public int Level { get => level; set => level = value; }
    }
}
