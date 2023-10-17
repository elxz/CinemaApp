using Cinema.Context;
using Cinema.Models.Queries;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IHallService hallService;
        public ScheduleController(ISessionService sessionService, IHallService hallService)
        {
            this.sessionService = sessionService;
            this.hallService = hallService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] ScheduleQuery schedule)
        {
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel();

            var sessions = await sessionService.GetSessions();
            if(sessions.StatusCode == Enum.StatusCode.SessionNotFound)
            {
                return RedirectToAction("Index", "Home");
            }
            scheduleViewModel.sessions = sessions.Data.ToList();

            var halls = await hallService.GetHalls();
            if (halls.StatusCode == Enum.StatusCode.HallNotFound)
            {
                return RedirectToAction("Index", "Home");
            }
            scheduleViewModel.halls = halls.Data.ToList();

            scheduleViewModel.schedule = schedule;

            return View(scheduleViewModel);
        }
    }
}
