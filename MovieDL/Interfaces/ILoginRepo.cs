using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL.Interfaces
{
    public interface ILoginRepo
    {
        public bool isAdmin(string username, string password);
        public bool isUser(string username, string password);
        public void createUser(string userName, string password, bool isAdmin);
    }
}
