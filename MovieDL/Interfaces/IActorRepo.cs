using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL.Interfaces
{
    public interface IActorRepo
    {
        List<Actors> getActors(Guid movieID);
        public List<Actors> getAllActors();
        void addActor(Actors actor);

    }
}
