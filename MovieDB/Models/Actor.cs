using System;
using System.Collections.Generic;

#nullable disable

namespace MovieDB.Models
{
    public partial class Actor
    {
        public Actor()
        {
            MovieActors = new HashSet<MovieActor>();
        }

        public Guid Id { get; set; }
        public string ActorName { get; set; }
        public string ActorGender { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }
}
