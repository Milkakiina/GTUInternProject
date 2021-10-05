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
    public class ActorController : Controller
    {
        private readonly IMovieRepo _movieRepo;
        private readonly ILogger<ActorController> _logger;
        private readonly IActorRepo _actorRepo;

        public ActorController(ILogger<ActorController> logger, IMovieRepo movieRepo, IActorRepo actorRepo)
        {
            _movieRepo = movieRepo;
            _logger = logger;
            _actorRepo = actorRepo;
        }

        public IActionResult ActorsList()
        {
            var actors = _actorRepo.getAllActors();

            var actorVM = actors.Select(a => new ActorVM()
            {
              ActorName = a.ActorName,
              ActorGender = a.ActorGender,
            }
            ).ToList();

            return View(actorVM);
        }

        [Authorize(Roles = "User")]
        public IActionResult CreateNewActor(Actors a)
        {

            if (a.ActorGender == null)
            {
                return View();
            }

            Actors ac = new Actors()
            {
                ActorName = a.ActorName,
                ActorGender = a.ActorGender
            };
            _movieRepo.addActor(ac);

            List<SelectListItem> genders = new List<SelectListItem>();
            genders.Add(new SelectListItem() { Text = "female", Value = "female" });
            genders.Add(new SelectListItem() { Text = "female", Value = "female" });

            string[] played = a.moviePlayed.Split(',');

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
        //<tr>
        //                <td>
        //                    Played Movie(s)
        //                </td>
        //                <td>
        //                    @Html.TextBoxFor(m => m.moviePlayed, new {@Value = "Enter Movies", @class = "red" })

        //                </td>

        //            </tr>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
