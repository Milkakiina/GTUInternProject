using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL
{
    public class Movie
    {
        public Movie()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public  string Title { get; set; }
        public  DateTime ReleaseDate { get; set; }
        public  string Genre { get; set; }
        public  string Director { get; set; }

        //public List<Actors> actorList = new List<Actors>();

    }

}
