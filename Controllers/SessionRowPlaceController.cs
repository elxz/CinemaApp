using Cinema.Models.ViewModels;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    public class SessionRowPlaceController : Controller
    {
        private readonly ISessionRowPlaceService sessionRowPlaceService;
        public SessionRowPlaceController(ISessionRowPlaceService sessionRowPlaceService)
        {
            this.sessionRowPlaceService = sessionRowPlaceService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
