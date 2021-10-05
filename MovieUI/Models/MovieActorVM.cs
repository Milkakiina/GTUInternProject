using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDL;
using System;
using System.Collections.Generic;

namespace MovieUI.Models
{

    public class MovieActorVM
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }


        public List<SelectListItem> Actors { get; set; }
        public List<SelectListItem> PlayedActors { get; set; }
        public string SelectedActorText { get; set; }


    }
}
