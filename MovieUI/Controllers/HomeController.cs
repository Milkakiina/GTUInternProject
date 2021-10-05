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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MovieDB.Models;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepo _movieRepo;
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginRepo _loginRepo;

        public HomeController(ILogger<HomeController> logger, IMovieRepo movieRepo, ILoginRepo loginRepo)
        {
            _movieRepo = movieRepo;
            _logger = logger;
            _loginRepo = loginRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserIndex()
        {
            return View();
        }
        public IActionResult AdminIndex()
        {
            return View();
        }
        public IActionResult CreateNewUser(User user)
        {

            if (user.Password == null)
            {
                return View();
            }

            _loginRepo.createUser(user.UserName, user.Password, user.IsAdmin);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(LoginVM login)
        {
                var check = _loginRepo.isAdmin(login.UserName, login.Password);
                var usercheck = _loginRepo.isUser(login.UserName, login.Password);
          
                var user = login;
                var userRights = new List<Claim>();
                userRights.Add(new Claim(ClaimTypes.Name, user.UserName));

                if (check)
                {
                    userRights.Add(new Claim(ClaimTypes.Role, "Admin"));
                    userRights.Add(new Claim(ClaimTypes.Role, "User"));
                }
                else if (usercheck)
                {
                    userRights.Add(new Claim(ClaimTypes.Role, "User"));
                }
                var userIdentity = new ClaimsIdentity(userRights, CookieAuthenticationDefaults.AuthenticationScheme);

                var settings = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                var claimsPrincipal = new ClaimsPrincipal(userIdentity);
                var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

                await HttpContext.SignInAsync(scheme, claimsPrincipal, settings);
            if (check)
            {
                return RedirectToAction("AdminIndex", "Home");
            }
            return RedirectToAction("Index", "Home");
            
           
        }
 
        //public IActionResult MoviesList()
        //{

        //    var movies = _movieRepo.getAllMovies();

        //    var moviesVM = movies.Select(a => new MovieVM()
        //    {
        //        Director = a.Director,
        //        Genre = a.Genre,
        //        ReleaseDate = a.ReleaseDate.ToString("dd,MM,yyyy"),
        //        Title = a.Title,
        //        Link = "https://localhost:5001/Home/ShowMovieInfo?movieID=" + a.ID.ToString()
        //    }
        //    ).ToList();

        //    return View(moviesVM);
        //}

        //public IActionResult CreateNewMovie(MovieVM a)
        //{
        //    if (a.Title == null)
        //    {
        //        return View();
        //    }

        //    DateTime dt = Convert.ToDateTime(a.ReleaseDate);
        //    Movies m = new Movies()
        //    {
        //        Director = a.Director,
        //        Genre = a.Genre,
        //        Title = a.Title,
        //        ReleaseDate = dt,
        //    };
        //    _movieRepo.addMovie(m);

        //    return View();
        //}

        //[HttpGet]
        //public IActionResult ShowMovieInfo(Guid movieID)
        //{
        //    var movieActorVM = new MovieActorVM();
        //    var movies = _movieRepo.getAllMovies();
        //    foreach(var item in movies)
        //    {
        //        if(item.ID == movieID)
        //        {
        //            movieActorVM.ID = item.ID;
        //            movieActorVM.Director = item.Director;
        //            movieActorVM.Genre = item.Genre;
        //            movieActorVM.Title = item.Title;
        //            movieActorVM.ReleaseDate = item.ReleaseDate.ToString();
        //        }
        //    }
        //    var i = 1;
        //        //getActorOfMvie
        //    List<SelectListItem> playedActors = new List<SelectListItem>();
        //    foreach (var actor in _movieRepo.getAllActors())
        //    {
        //        Guid tempActorID = new Guid();
        //        foreach (var movieActor in _movieRepo.getAllMovieActors())
        //        {
        //            if (movieActor.movieID == movieActorVM.ID)
        //            {
        //                tempActorID = movieActor.actorID;
        //                if (actor.ID == tempActorID)
        //                {
        //                    playedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = actor.ActorName });
        //                    i++;
        //                }
        //            }
        //        }
        //    }
        //    i = 1;
        //    List<SelectListItem> unplayedActors = new List<SelectListItem>();


        //    foreach (var actor in _movieRepo.getAllActors())
        //    {
        //        Guid tempActorID = new Guid();
        //        foreach (var movieActor in _movieRepo.getAllMovieActors())
        //        {
        //            if (movieActor.movieID == movieActorVM.ID)
        //            {
        //                tempActorID = movieActor.actorID;
        //                if (actor.ID != tempActorID && IsHave(playedActors,actor) && IsHave(unplayedActors, actor))
        //                {
        //                    unplayedActors.Add(new SelectListItem() { Text = actor.ActorName, Value = actor.ActorName});
        //                    i++;
        //                }
        //            }
        //        }


        //    }

        //    if (playedActors.Count == 0)
        //    {
        //        foreach (var actor2 in _movieRepo.getAllActors())
        //        {
        //            unplayedActors.Add(new SelectListItem() { Text = actor2.ActorName, Value = actor2.ActorName });
        //        }
        //    }

        //    movieActorVM.Actors = unplayedActors;
        //    movieActorVM.PlayedActors = playedActors;
        //    return View(movieActorVM);
        //}

        //[HttpPost]
        //public ActionResult ShowMovieInfo(MovieActorVM tempMovieActorVM)
        //{

        //    foreach(var actor in _movieRepo.getAllActors())
        //    {
        //        if (actor.ActorName.Equals(tempMovieActorVM.SelectedActorText))
        //        {
        //            _movieRepo.addActorToMovie(actor.ID, tempMovieActorVM.ID);
        //        }
        //    }

        //    return (ActionResult)ShowMovieInfo(tempMovieActorVM.ID);
        //}

        //public bool IsHave(List<SelectListItem> playedActors, Actors actor)
        //{
        //    foreach (var movieActor in playedActors)
        //    {
        //        if (movieActor.Text.Equals(actor.ActorName))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public IActionResult ActorsList()
        //{
        //    var actors = _movieRepo.getAllActors();

        //    var actorVM = actors.Select(a => new ActorVM()
        //    {
        //      ActorName = a.ActorName,
        //      ActorGender = a.ActorGender,
        //    }
        //    ).ToList();

        //    return View(actorVM);
        //}

        //public IActionResult CreateNewActor(Actors a)
        //{

        //    if (a.ActorGender == null)
        //    {
        //        return View();
        //    }

        //    Actors ac = new Actors()
        //    {
        //        ActorName = a.ActorName,
        //        ActorGender = a.ActorGender
        //    };
        //    _movieRepo.addActor(ac);

        //    List<SelectListItem> genders = new List<SelectListItem>();
        //    genders.Add(new SelectListItem() { Text = "female", Value = "female" });
        //    genders.Add(new SelectListItem() { Text = "female", Value = "female" });

        //    string[] played = a.moviePlayed.Split(',');

        //    int i = 0;
        //    while(i != played.Length)
        //    {
        //        foreach(var movie in Entities.movieList)
        //        {
        //            if (movie.Title.Equals(played[i], StringComparison.OrdinalIgnoreCase))
        //            {
        //                _movieRepo.addActorToMovie(ac.ID, movie.ID);
        //                var movieActors = Entities.movieActorList;
        //            }
        //        }
        //        i++;
        //    }
        //    return View();
        //}
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
