using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MovieDL;
using MovieUI.Models;
using MovieDL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace MovieProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepo _movieRepo;
        private readonly ILogger<MovieController> _logger;
        private readonly IActorRepo _actorRepo;

        public MovieController(ILogger<MovieController> logger, IMovieRepo movieRepo, IActorRepo actorRepo)
        {
            _movieRepo = movieRepo;
            _logger = logger;
            _actorRepo = actorRepo;
        }

        public IActionResult MoviesList()
        {

            var movies = _movieRepo.getAllMovies();

            var moviesVM = movies.Select(a => new MovieVM()
            {
                Director = a.Director,
                Genre = a.Genre,
                ReleaseDate = a.ReleaseDate.ToString("dd,MM,yyyy"),
                Title = a.Title,
                Link = "https://localhost:5001/Movie/ShowMovieInfo?movieID=" + a.ID.ToString()
            }
            ).ToList();

            return View(moviesVM);
        }

        [Authorize(Roles = "User")]
        public IActionResult CreateNewMovie(MovieVM a)
        {
            if (a.Title == null)
            {
                return View();
            }

            DateTime dt = Convert.ToDateTime(a.ReleaseDate);
            Movies m = new Movies()
            {
                Director = a.Director,
                Genre = a.Genre,
                Title = a.Title,
                ReleaseDate = dt,
            };
            _movieRepo.addMovie(m);

            return View();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult ShowMovieInfo(Guid movieID)
        {
            var movieActorVM = new MovieActorVM();
            var movies = _movieRepo.getAllMovies();
            foreach(var item in movies)
            {
                if(item.ID == movieID)
                {
                    movieActorVM.ID = item.ID;
                    movieActorVM.Director = item.Director;
                    movieActorVM.Genre = item.Genre;
                    movieActorVM.Title = item.Title;
                    movieActorVM.ReleaseDate = item.ReleaseDate.ToString();
                }
            }

            List<SelectListItem> playedActors = new List<SelectListItem>();
            foreach (var actor in _actorRepo.getActors(movieID))
            {
                playedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = actor.ActorName });
            }

            List<SelectListItem> unplayedActors = new List<SelectListItem>();

            foreach (var actor in _actorRepo.getAllActors())
            {
                foreach (var actorp in _actorRepo.getActors(movieID))
                {
                        if (actor.ID != actorp.ID && IsHave(playedActors,actor) && IsHave(unplayedActors, actor))
                        {
                            unplayedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = actor.ActorName});
                        }
                    
                }
         }

            if (playedActors.Count == 0)
            {
                foreach (var actor2 in _actorRepo.getAllActors())
                {
                    unplayedActors.Add(new SelectListItem() { Text = actor2.ActorName, Value = actor2.ActorName });
                }
            }

            movieActorVM.Actors = unplayedActors;
            movieActorVM.PlayedActors = playedActors;
            return View(movieActorVM);
        }

        [HttpPost]
        public ActionResult ShowMovieInfo(MovieActorVM tempMovieActorVM)
        {
           
            foreach(var actor in _movieRepo.getAllActors())
            {
                if (actor.ActorName.Equals(tempMovieActorVM.SelectedActorText))
                {
                    _movieRepo.addActorToMovie(actor.ID, tempMovieActorVM.ID);
                }
            }

            return (ActionResult)ShowMovieInfo(tempMovieActorVM.ID);
        }

        public bool IsHave(List<SelectListItem> playedActors, Actors actor)
        {
            foreach (var movieActor in playedActors)
            {
                if (movieActor.Text.Equals(actor.ActorName))
                {
                    return false;
                }
            }
            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
