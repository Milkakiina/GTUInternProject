using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL
{
    public class Entities
    {
       public static List<Movies> movieList = new List<Movies>();

       public static List<Actors> actorList = new List<Actors>()
        {
            new Actors("Keanu Reeves", "male"),
            new Actors("Carrie-Anne Moss", "female"),
            new Actors("Hugo Weaving", "male"),
            new Actors("Ben Affleck", "male"),
            new Actors("Leonardo DiCaprio", "male"),
            new Actors("Robert Downey Jr.", "male"),
            new Actors("Johnny Depp", "male"),
            new Actors("Nicole Kidman", "female"),
            new Actors("Cate Blanchett", "female"),
            new Actors("Julia Roberts", "female"),
            new Actors("Liv Tyler", "female"),
        };

        public static List<MovieActors> movieActorList = new List<MovieActors>();
      
    }
}
