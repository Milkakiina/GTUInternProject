using System;
using System.Collections.Generic;

#nullable disable

namespace MovieDB.Models
{
    public partial class MovieActor
    {
        public Guid Id { get; set; }
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
