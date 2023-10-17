using Cinema.Interfaces;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Cinema.Enum;
using Microsoft.AspNetCore.Authorization;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema.Models;
using System.Diagnostics.Metrics;
using Cinema.Context;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService filmService;
        private readonly IGenreService genreService;
        private readonly IOriginCountryService originCountryService;
        private readonly IDirectorService directorService;
        private readonly IRestrictionService restrictionService;
        private readonly IFilmGenreService filmGenreService;
        private readonly IFilmCountryService filmCountryService;
        public FilmController(IFilmService filmService, IGenreService genreService, IOriginCountryService originCountryService, IDirectorService directorService, 
            IRestrictionService restrictionService, IFilmGenreService filmGenreService, IFilmCountryService filmCountryService)
        {
            this.filmService = filmService;
            this.originCountryService = originCountryService;
            this.genreService = genreService;
            this.directorService = directorService;
            this.restrictionService = restrictionService;
            this.filmCountryService = filmCountryService;
            this.filmGenreService = filmGenreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            var films = await filmService.GetFilms();
            if(films.StatusCode == Enum.StatusCode.OK)
            {
                return View(films.Data.ToList());
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetFilm(int id)
        {
            var films = await filmService.GetFilm(id);
            if (films.StatusCode == Enum.StatusCode.OK)
            {
                return View(films.Data);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateFilm()
        
        {
            var genres = await genreService.GetGenres();
            List<GenreCheckBoxViewModel> genreCheckBox = new List<GenreCheckBoxViewModel>();
            foreach (var elem in genres.Data)
            {
                GenreCheckBoxViewModel genreCheckBoxElem = new GenreCheckBoxViewModel();
                genreCheckBoxElem.Id = elem.genre_id;
                genreCheckBoxElem.Name = elem.genre_name;
                genreCheckBoxElem.IsChecked = false;
                genreCheckBox.Add(genreCheckBoxElem);
            }

            var countries = await originCountryService.GetOriginCountries();
            List<CountryCheckBoxViewModel> countryCheckBox = new List<CountryCheckBoxViewModel>();
            foreach (var elem in countries.Data)
            {
                CountryCheckBoxViewModel countryCheckBoxElem = new CountryCheckBoxViewModel();
                countryCheckBoxElem.Id = elem.country_id;
                countryCheckBoxElem.Name = elem.country_name;
                countryCheckBoxElem.IsChecked = false;
                countryCheckBox.Add(countryCheckBoxElem);
            }

            var directors = await directorService.GetDirectors();
            var Directors = directors.Data.Select(x => new
            {
                director_id = x.director_id,
                req_info = x.director_firstname + " " + x.director_lastname
            });
            ViewBag.Directors = new SelectList(Directors, "director_id", "req_info");

            var restrictions = await restrictionService.GetRestrictions();
            ViewBag.Restrictions = new SelectList(restrictions.Data, "restriction_id", "restriction");

            FilmGenreCountryDirectorRestrictionCheckBoxViewModel model = new FilmGenreCountryDirectorRestrictionCheckBoxViewModel();
            model.genreCheckBoxViewModel = genreCheckBox;
            model.countryCheckBoxViewModel = countryCheckBox;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilm(Film film, List<GenreCheckBoxViewModel> genreCheckBoxViewModel, List<CountryCheckBoxViewModel> countryCheckBoxViewModel)
        {
            if (ModelState.IsValid)
            {
                if (film.film_id == 0)
                {
                    await filmService.CreateFilm(film);

                    var films = await filmService.GetFilms();
                    var film_id = films.Data.OrderBy(x => x.film_id).ToList().Last().film_id;

                    foreach (var elem in genreCheckBoxViewModel)
                    {
                        if(elem.IsChecked == true)
                        {
                            FilmGenre fg = new FilmGenre();
                            fg.fk_genre = elem.Id;
                            fg.fk_film = film_id;
                            await filmGenreService.CreateFilmGenre(fg);
                        }
                    }
                    foreach (var elem in countryCheckBoxViewModel)
                    {
                        if (elem.IsChecked == true)
                        {
                            FilmCountry fc = new FilmCountry();
                            fc.fk_country = elem.Id;
                            fc.fk_film = film_id;
                            await filmCountryService.CreateFilmCountry(fc);
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Database");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFilm()
        {
            var temp_films = await filmService.GetFilms();
            if(temp_films.StatusCode == Enum.StatusCode.OK)
            {
                var films = temp_films.Data.ToList();
                if (films.Count != 0)
                {
                    var Films = films.Select(x => new
                    {
                        film_id = x.film_id,
                        req_info = x.film_name + " | " + x.release_date.ToShortDateString()
                    });
                    ViewBag.Films = new SelectList(Films, "film_id", "req_info");

                    return View();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFilm(Film film)
        {
            var films = await filmService.DeleteFilm(film);
            if (films.StatusCode == Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "Database");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
