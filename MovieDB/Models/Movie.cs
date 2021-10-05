using System;
using System.Collections.Generic;

#nullable disable

namespace MovieDB.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieActors = new HashSet<MovieActor>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }
}
