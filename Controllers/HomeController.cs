using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFilmService filmService;
        private readonly IRestrictionService restrictionService;
        private readonly IGenreService genreService;
        private readonly IFilmGenreService filmGenreService;
        private readonly IFilmCountryService filmCountryService;
        private readonly IDirectorService directorService;
        private readonly IOriginCountryService originCountryService;

        public HomeController(ILogger<HomeController> logger, IFilmService filmService, IRestrictionService restrictionService, IGenreService genreService, 
            IFilmGenreService filmGenreService, IDirectorService directorService, IOriginCountryService originCountryService, IFilmCountryService filmCountryService)
        {
            _logger = logger;
            this.filmService = filmService;
            this.restrictionService = restrictionService;
            this.genreService = genreService;
            this.filmGenreService = filmGenreService;
            this.directorService = directorService;
            this.originCountryService = originCountryService;
            this.filmCountryService = filmCountryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            FilmGenreCountryDirectorRestrictionListViewModel model = new FilmGenreCountryDirectorRestrictionListViewModel();

            var films = await filmService.GetFilms();
            if(films.Data != null)
            {
                var restrictions = await restrictionService.GetRestrictions();
                var genres = await genreService.GetGenres();
                var filmGenres = await filmGenreService.GetFilmGenres();
                var filmCountries = await filmCountryService.GetFilmCountries();
                var directors = await directorService.GetDirectors();
                var countries = await originCountryService.GetOriginCountries();

                model.films = films.Data.ToList();
                model.restrictions = restrictions.Data.ToList();
                model.genres = genres.Data.ToList();
                model.filmGenres = filmGenres.Data.ToList();
                model.filmCountries = filmCountries.Data.ToList();
                model.directors = directors.Data.ToList();
                model.countries = countries.Data.ToList();
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}