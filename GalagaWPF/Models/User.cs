using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalagaWPF.Models
{
    public class User
    {
        private int id;
        private string name;
        private string lastName;
        private string email;
        private string password;


        //el por defecto
        public User(int id, string name, string lastName, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }

        //polimorfismo para el inicio de sesion y el registro
        public User(string name, string lastName, string email, string password)
        {
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }

        public User()
        {
        }   


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
    }
}
