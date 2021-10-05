using System;
using System.Collections.Generic;

#nullable disable

namespace MovieDB.Models
{
    public partial class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public Guid Id { get; set; }
    }
}
