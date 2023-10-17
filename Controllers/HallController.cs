using Cinema.Context;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Cinema.Controllers
{
    public class HallController : Controller
    {
        private readonly IHallService hallService;
        public HallController(IHallService hallService)
        {
            this.hallService = hallService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var halls = await hallService.GetHalls();
            if (halls.StatusCode == Enum.StatusCode.OK)
            {
                return View(halls.Data.OrderBy(x => x.hall_number).ToList());
            }

            return RedirectToAction("Index");
        }
    }
}
