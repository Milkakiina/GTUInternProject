﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL.Interfaces
{
    public interface IMovieRepo
    {
        object dbc { get; }

        List<Movies> getAllMovies();
        void addMovie(Movies m);
        List<Actors> getActors(Guid movieID);
        public List<Actors> getAllActors();
        public List<MovieActors> getAllMovieActors();

        void addActor(Actors actor);
        void addActorToMovie(Guid actorID, Guid movieID);

    }
}
