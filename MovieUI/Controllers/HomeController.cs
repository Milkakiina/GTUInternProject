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

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepo _movieRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
                Link = "https://localhost:5001/Home/ShowMovieInfo?movieID=" + a.ID.ToString()
            }
            ).ToList();

            return View(moviesVM);
        }

        public IActionResult CreateNewMovie(MovieVM a)
        {
            if (a.Title == null)
            {
                return View();
            }

            DateTime dt = Convert.ToDateTime(a.ReleaseDate);
            Movie m = new Movie()
            {
                Director = a.Director,
                Genre = a.Genre,
                Title = a.Title,
                ReleaseDate = dt,
              
            };
            _movieRepo.addMovie(m);

            return View();
        }
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
            var i = 1;
            List<SelectListItem> playedActors = new List<SelectListItem>();
            foreach (var actor in Entities.actorList)
            {
                Guid tempActorID = new Guid();
                foreach (var movieActor in Entities.movieActorList)
                {
                    if (movieActor.movieID == movieActorVM.ID)
                    {
                        tempActorID = movieActor.actorID;
                        if (actor.ID == tempActorID)
                        {
                            playedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = actor.ActorName });
                            i++;
                        }
                    }
                }
            }

            i = 1;
            List<SelectListItem> unplayedActors = new List<SelectListItem>();
            foreach (var actor in Entities.actorList)
            {
                Guid tempActorID = new Guid();
                foreach (var movieActor in Entities.movieActorList)
                {
                    if (movieActor.movieID == movieActorVM.ID)
                    {
                        tempActorID = movieActor.actorID;
                        if (actor.ID != tempActorID && IsHave(playedActors,actor) && IsHave(unplayedActors, actor))
                        {
                            unplayedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = actor.ActorName});
                            i++;
                        }
                    }
                }
            }
            movieActorVM.Actors = unplayedActors;
            movieActorVM.PlayedActors = playedActors;
            return View(movieActorVM);
        }

        [HttpPost]
        public ActionResult ShowMovieInfo(MovieActorVM tempMovieActorVM)
        {
            var movieActorVM = new MovieActorVM();
            var movies = _movieRepo.getAllMovies();
           
            foreach(var actor in Entities.actorList)
            {
                if (actor.ActorName.Equals(tempMovieActorVM.SelectedActorText))
                {
                    _movieRepo.addActorToMovie(actor.ID, tempMovieActorVM.ID);
                }
            }
            foreach (var item in movies)
            {
                if (item.ID == tempMovieActorVM.ID)
                {
                    movieActorVM.ID = item.ID;
                    movieActorVM.Director = item.Director;
                    movieActorVM.Genre = item.Genre;
                    movieActorVM.Title = item.Title;
                    movieActorVM.ReleaseDate = item.ReleaseDate.ToString();
                }
            }
            var i = 1;
            List<SelectListItem> playedActors = new List<SelectListItem>();
            foreach (var actor in Entities.actorList)
            {
                Guid tempActorID = new Guid();
                foreach (var movieActor in Entities.movieActorList)
                {
                    if (movieActor.movieID == movieActorVM.ID)
                    {
                        tempActorID = movieActor.actorID;
                        if (actor.ID == tempActorID)
                        {
                            playedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = i.ToString() });
                            i++;
                        }
                    }
                }
            }

            i = 1;
            List<SelectListItem> unplayedActors = new List<SelectListItem>();
            foreach (var actor in Entities.actorList)
            {
                Guid tempActorID = new Guid();
                foreach (var movieActor in Entities.movieActorList)
                {
                    if (movieActor.movieID == movieActorVM.ID)
                    {
                        tempActorID = movieActor.actorID;
                        if (actor.ID != tempActorID && IsHave(playedActors, actor) && IsHave(unplayedActors, actor))
                        {
                            unplayedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = i.ToString() });
                            i++;
                        }
                    }
                }
            }

            movieActorVM.Actors = unplayedActors;
            movieActorVM.PlayedActors = playedActors;

            return View(movieActorVM);
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

        public IActionResult ActorsList()
        {
            var actors = _movieRepo.getAllActors();

            var actorVM = actors.Select(a => new ActorVM()
            {
              ActorName = a.ActorName,
              ActorGender = a.ActorGender,
            }
            ).ToList();

            return View(actorVM);
        }

        public IActionResult CreateNewActor(Actors a)
        {

            if (a.ActorGender == null)
            {
                return View();
            }
            string[] played = a.moviePlayed.Split(',');

            Actors ac = new Actors()
            {
                ActorName = a.ActorName,
                ActorGender = a.ActorGender
            };
            _movieRepo.addActor(ac);

            int i = 0;
            while(i != played.Length)
            {
                foreach(var movie in Entities.movieList)
                {
                    if (movie.Title.Equals(played[i], StringComparison.OrdinalIgnoreCase))
                    {
                        _movieRepo.addActorToMovie(ac.ID, movie.ID);
                        var movieActors = Entities.movieActorList;
                    }
                }
                i++;
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
