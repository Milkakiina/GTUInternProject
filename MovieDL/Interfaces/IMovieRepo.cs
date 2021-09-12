using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL.Interfaces
{
    public interface IMovieRepo
    {
        List<Movie> getAllMovies();
        void addMovie(Movie m);
        List<Actors> getActors(Guid movieID);
        public List<Actors> getAllActors();
        void addActor(Actors actor);
        void addActorToMovie(Guid actorID, Guid movieID);
    }
}
