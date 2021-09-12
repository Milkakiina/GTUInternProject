using MovieDL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL
{
    public class MovieRepo : IMovieRepo
    {
        public void addActor(Actors actor)
        {
            Entities.actorList.Add(actor);

            //Entities.movieActorList.Add(new MovieActor()
            //{
            //    actorID = actor.ID
            //});

        }
        public List<Actors> getAllActors()
        {
            return Entities.actorList.ToList();
        }
        public void addActorToMovie(Guid actorID, Guid movieID)
        {
            Entities.movieActorList.Add(new MovieActor()
            {
                actorID = actorID,
                movieID = movieID
            });   
        }

        public void addMovie(Movie m)
        {
            Entities.movieList.Add(m);
        }

        public List<Actors> getActors(Guid movieID)
        {
            List<Actors> actorList = new List<Actors>();
            foreach(var item in Entities.movieActorList)
            {
                if (item.movieID == movieID)
                {
                    Guid actorID = item.actorID;
                    foreach (var actor in Entities.actorList)
                    {
                        if (actor.ID == actorID)
                        {
                            actorList.Add(actor);
                        }
                    }
                }
            }
            
            return actorList;
        }

        public List<Movie> getAllMovies()
        {
            return Entities.movieList.ToList();
        }

  
    }
}
