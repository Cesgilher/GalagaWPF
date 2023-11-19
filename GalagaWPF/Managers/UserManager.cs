using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalagaWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace GalagaWPF.Controller
{
    public class UserManager // Patron Singleton
    {
        private static UserManager instance;
        private List<User> users;
        private User session;
        private DBContext dB = new DBContext();

        private UserManager()
        {
            this.users = dB.Users.ToList();
        }

        public static UserManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserManager();
                }
                return instance;
            }
        }

        public User GetSession()
        {
            return session;
        }


        public List<User> GetUsers()
        {
            return users;
        }
        
        public void Register(User user)
        {
            if (users != null)
            {
                bool userExists = users.Any(u => u.Email == user.Email);

                if (userExists)
                {
                    Console.WriteLine("Ese usuario ya existe");
                }
                else
                {
                    dB.Users.Add(user);
                    dB.SaveChanges();
                    users = dB.Users.ToList();
                    session = dB.Users.SingleOrDefault(u => u.Email == user.Email);
                    Console.WriteLine("Usuario registrado con éxito.");
                }
            }
        }
        public void LogIn(User user)
        {
            if (users != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (user.Email == users[i].Email && user.Password == users[i].Password)
                    {
                        session = users[i];
                        Console.WriteLine("Se ha iniciado sesion correctamente");
                        break;
                    }
                    else if (user.Email == users[i].Email)
                    {
                        Console.WriteLine("Contraseña incorrecta");

                    }


                }
            }

        }
        public void LogOut()
        {
            session = null;
        }

        public bool EditField<T>(User session, T newValue, Func<User, T> getField, Action<User, T> setField, string errorMessage)
        {
            bool fieldExists = users.Any(u => getField(u).Equals(newValue));

            if (!fieldExists)
            {
                int index = users.FindIndex(u => u.Email == session.Email);

                if (index != -1)
                {
                    setField(users[index], newValue);
                }

                setField(session, newValue);
                return true;  // Operation succeeded
            }
            else
            {
                Console.WriteLine(errorMessage);
                return false;  // Operation failed
            }
        }

        public bool EditName(string newName)
        {
            return EditField(session, newName, u => u.Name, (u, value) => u.Name = value, "Ese nombre de usuario no está disponible");
        }
        public bool EditLastName(string newLastName)
        {
            return EditField(session, newLastName, u => u.LastName, (u, value) => u.LastName = value, "Ese nombre de usuario no está disponible");
        }

        public bool EditPassword(string newPassword)
        {
            return EditField(session, newPassword, u => u.Password, (u, value) => u.Password = value, "Esa contraseña es idéntica a la actual");
        }


        public void DeleteUser(User session)
        {

            if (users != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (session.Email == users[i].Email)
                    {
                        users.RemoveAt(i);
                        session = null;
                        break;
                    }


                }
            }

        }
    }
}


