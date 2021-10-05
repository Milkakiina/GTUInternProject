using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL
{
    public class Movies
    {
       
        public  string Title { get; set; }
        public  DateTime ReleaseDate { get; set; }
        public  string Genre { get; set; }
        public  string Director { get; set; }

        public Movies()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
    }

}
