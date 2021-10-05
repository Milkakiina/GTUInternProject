using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace MovieUI.Models
{

    public class LoginVM
    {              
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }

    }
}
