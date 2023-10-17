using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Cinema.Models;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using System.IO;
using static System.Net.WebRequestMethods;

namespace Cinema.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IFilmService filmService;
        private readonly IHallService hallService;
        private readonly ISessionRowPlaceService sessionRowPlaceService;
        public SessionController(ISessionService sessionService, IFilmService filmService, IHallService hallService, ISessionRowPlaceService sessionRowPlaceService)
        {
            this.sessionService = sessionService;
            this.hallService = hallService;
            this.filmService = filmService;
            this.sessionRowPlaceService = sessionRowPlaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSessions()
        {
            var sessions = await sessionService.GetSessions();
            if (sessions.StatusCode == Enum.StatusCode.OK)
            {
                return View(sessions.Data.ToList());
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetSession(int id)
        {
            var sessions = await sessionService.GetSession(id);
            if (sessions.StatusCode == Enum.StatusCode.OK)
            {
                return View(sessions.Data);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateSession()

        {
            var halls = await hallService.GetHalls();
            if (halls.StatusCode == Enum.StatusCode.HallNotFound)
            {
                return View("Index");
            }
            ViewBag.Halls = new SelectList(halls.Data, "hall_id", "screen_type");

            var films = await filmService.GetFilms();
            if (films.StatusCode == Enum.StatusCode.FilmNotFound)
            {
                return View("Index");
            }
            var Films = films.Data.Select(x => new
            {
                film_id = x.film_id,
                req_info = x.film_name + " | " + x.release_date.ToShortDateString()
            });
            ViewBag.Films = new SelectList(Films, "film_id", "req_info");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(Session session)
        {
            if (ModelState.IsValid)
            {
                if (session.session_id == 0)
                {
                    await sessionService.CreateSession(session);

                    var sessions = await sessionService.GetSessions();

                    var hall = await hallService.GetHall(sessions.Data.ToList().Last().fk_hall);

                    SessionRowPlace srp = new SessionRowPlace();
                    for (int indexRow = 0; indexRow < hall.Data.count_of_rows; indexRow++)
                    {
                        for(int indexPlace = 0; indexPlace < hall.Data.places_per_row; indexPlace++)
                        {
                            srp.fk_session = sessions.Data.ToList().Last().session_id;
                            srp.row_ = indexRow + 1;
                            srp.place_ = indexPlace + 1;
                            srp.status = "Available";

                            await sessionRowPlaceService.CreateSessionRowPlace(srp);
                        }
                    }
                }
                else
                {
                    await sessionService.Edit(session);
                }
            }

            return RedirectToAction("Index", "Database");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSession()
        {
            var temp_sessions = await sessionService.GetSessions();
            if(temp_sessions.StatusCode == Enum.StatusCode.OK)
            {
                var sessions = temp_sessions.Data.ToList();
                if (sessions.Count != 0)
                {
                    var Sessions = sessions.Select(x => new
                    {
                        session_id = x.session_id,
                        req_info = x.session_date + " | " + x.start_time + " - " + x.end_time + " | " + x.fk_hall
                    });
                    ViewBag.Sessions = new SelectList(Sessions, "session_id", "req_info");

                    return View();
                }
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSession(Session session)
        {
            var sessions = await sessionService.DeleteSession(session);
            if (sessions.StatusCode == Enum.StatusCode.OK)
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
