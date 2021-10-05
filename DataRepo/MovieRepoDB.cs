using MovieDB.Models;
using MovieDL;
using MovieDL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepo
{
    public class MovieRepoDB : IMovieRepo
    {
        private readonly MovieDatabaseContext dbc;

        public MovieRepoDB(MovieDatabaseContext dbc)
        {
            this.dbc = dbc;
        }

        object IMovieRepo.dbc => throw new NotImplementedException();

        public void addActor(Actors actor)
        {
            throw new NotImplementedException();
        }
        public void addMovie(Movies m)
        {
            Movie movie = new Movie()
            {
                Title = m.Title,
                Genre = m.Genre,
                Director = m.Director,
                ReleaseDate = m.ReleaseDate,
                Id = m.ID
            };
            dbc.Movies.Add(movie);
            dbc.SaveChanges();

        }
        public void addActorToMovie(Guid actorID, Guid movieID)
        {
            MovieActor ma = new MovieActor()
            {
                ActorId = actorID,
                MovieId = movieID,
                Id = Guid.NewGuid()
            };
            dbc.MovieActors.Add(ma);
            dbc.SaveChanges();

        }

     

        public List<Actors> getActors(Guid movieID)
        {
            throw new NotImplementedException();
        }

        public List<Actors> getAllActors()
        {
            throw new NotImplementedException();
        }

        public List<MovieActors> getAllMovieActors()
        {
            List<MovieDL.MovieActors> malist = new List<MovieDL.MovieActors>();
            foreach (var ma in dbc.MovieActors)
            {
                malist.Add(new MovieDL.MovieActors {
                    actorID = ma.ActorId, movieID = ma.MovieId });
            }
            return malist;
        }

        public List<Movies> getAllMovies()
        {
            List<MovieDL.Movies> movieList = new List<MovieDL.Movies>();
            foreach (var movie in dbc.Movies)
            {
                movieList.Add(new MovieDL.Movies {
                Title = movie.Title, Director = movie.Director, Genre=movie.Genre,ReleaseDate = movie.ReleaseDate, ID=movie.Id});
            }
            return movieList;
        }
    }
}
