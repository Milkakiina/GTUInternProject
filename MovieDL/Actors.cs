using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL
{
    public class Actors
    {
        public string ActorName { get; set; }
        public string ActorGender { get; set; }
        public Guid ID { get; set; }
        public string moviePlayed { get; set; }
        public Actors(string actorName, string actorGender)
        {
            this.ID = Guid.NewGuid();
            ActorName = actorName;
            ActorGender = actorGender;
        }
        public Actors()
        {
            this.ID = Guid.NewGuid();
        }

    }
}
