using Cinema.Context;
using Cinema.Models;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace Cinema.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorService directorService;
        public DirectorController(IDirectorService directorService)
        {
            this.directorService = directorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectors()
        {
            var directors = await directorService.GetDirectors();
            if (directors.StatusCode == Enum.StatusCode.OK)
            {
                return View(directors.Data.ToList());
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetDirector(int id)
        {
            var directors = await directorService.GetDirector(id);
            if (directors.StatusCode == Enum.StatusCode.OK)
            {
                return View(directors.Data);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateDirector()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirector(Director director)
        {
            if (ModelState.IsValid)
            {
                if (director.director_id == 0)
                {
                    await directorService.CreateDirector(director);
                }
            }

            return RedirectToAction("Index", "Database");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDirector()
        {
            var temp_directors = await directorService.GetDirectors();
            if (temp_directors.StatusCode == Enum.StatusCode.OK)
            {
                var directors = temp_directors.Data.ToList();
                if (directors.Count != 0)
                {
                    var Directors = directors.Select(x => new
                    {
                        director_id = x.director_id,
                        req_info = x.director_firstname + " " + x.director_lastname
                    });
                    ViewBag.Directors = new SelectList(Directors, "director_id", "req_info");

                    return View();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDirector(Director director)
        {
            var directors = await directorService.DeleteDirector(director);
            if (directors.StatusCode == Enum.StatusCode.OK)
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
