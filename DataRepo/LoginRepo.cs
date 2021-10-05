using MovieDB.Models;
using MovieDL;
using MovieDL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepo
{
    public class LoginRepo: ILoginRepo
    {
        private readonly MovieDatabaseContext dbc;

        public LoginRepo(MovieDatabaseContext dbc)
        {
            this.dbc = dbc;
        }

        public User getUser(string userName, string password)
        {
            var user = dbc.Users
                           .Where(a => a.UserName == userName).Where(a => a.Password == password)
                           .ToArray();
            return user[0];
        }
        public bool isAdmin(string userName, string password)
        {
            var users = dbc.Users
                           .Where(a => a.UserName == userName).Where(a=>a.Password == password)
                           .ToArray();
           if(users.Length != 0)
            {
                if(users[0].IsAdmin)
                {
                    return true;
                }
            }

            return false;
        }
        public bool isUser(string userName, string password)
        {
            var users = dbc.Users
                           .Where(a => a.UserName == userName).Where(a => a.Password == password)
                           .ToArray();
            if (users.Length != 0)
            {
                    return true;
            }

            return false;
        }
        
        public void createUser(string userName, string password, bool isAdmin)
        {
            User a = new User()
            {
                UserName = userName,
                Password = password,
                IsAdmin = isAdmin,
                Id = Guid.NewGuid()
            };

            dbc.Users.Add(a);
            dbc.SaveChanges();
        }

    }
}
