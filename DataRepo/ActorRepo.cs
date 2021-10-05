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
    public class ActorRepo : IActorRepo
    {
        private readonly MovieDatabaseContext dbc;

        public ActorRepo(MovieDatabaseContext dbc)
        {
            this.dbc = dbc;
        }

        public void addActor(Actors actor)
        {
            Actor a = new Actor()
            {
                ActorName = actor.ActorName,
                ActorGender = actor.ActorGender,
                Id = actor.ID
            };

            dbc.Actors.Add(a);
            dbc.SaveChanges();
        }

        public List<Actors> getActors(Guid movieID)
        {
            var actorIDs = dbc.MovieActors.Where(a => a.MovieId == movieID).Select(a => a.ActorId).ToList();
            List<Actors> actors = new List<Actors>();
            foreach(var actor in dbc.Actors)
            {
                foreach (var actorID in actorIDs)
                {
                    if(actor.Id == actorID)
                    {
                        actors.Add(new MovieDL.Actors { ActorGender = actor.ActorGender, ActorName = actor.ActorName, ID = actor.Id });
                    }
                }
            }
            return actors;
        }

        public List<Actors> getAllActors()
        {
            List<MovieDL.Actors> actorList = new List<MovieDL.Actors>();
            foreach(var actor in dbc.Actors)
            {
                actorList.Add(new MovieDL.Actors { ActorGender = actor.ActorGender, ActorName = actor.ActorName, ID = actor.Id});
            }
            return actorList;
        }
      
    }
}
