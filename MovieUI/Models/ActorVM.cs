using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace MovieUI.Models
{

    public class ActorVM
    {              
        public string ActorGender { get; set; }
        public string ActorName { get; set; }
        public string moviePlayed { get; set; }
        public List<SelectListItem> genders { get; set; }


    }
}
