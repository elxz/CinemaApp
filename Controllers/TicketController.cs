using Cinema.Context;
using Cinema.Enum;
using Cinema.Models;
using Cinema.Models.Queries;
using Cinema.Models.ViewModels;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Serialization;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;

namespace Cinema.Controllers
{
    public class TicketController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IHallService hallService;
        private readonly ISessionRowPlaceService sessionRowPlaceService;
        private readonly IPricingService priceService;
        private readonly IUserService userService;  
        public TicketController(ISessionService sessionService, IHallService hallService, ISessionRowPlaceService sessionRowPlaceService,
            IPricingService priceService, IUserService userService)
        {
            this.sessionService = sessionService;
            this.hallService = hallService;
            this.sessionRowPlaceService = sessionRowPlaceService;
            this.priceService = priceService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] TicketQuery ticketQuery)
        {
            TicketViewModel ticketViewModel = new TicketViewModel();

            var session = await sessionService.GetSession(ticketQuery.id);
            ticketViewModel.session = session.Data;

            var hall = await hallService.GetHall(session.Data.fk_hall);
            ticketViewModel.hall = hall.Data;

            var temp_sessionRowPlaces = await sessionRowPlaceService.GetSessionRowPlaces();
            var sessionRowPlaces = temp_sessionRowPlaces.Data.Where(x => x.fk_session == session.Data.session_id).OrderBy(x => x.row_).ThenBy(x => x.place_).ToList();
            ticketViewModel.sessionRowPlaces = sessionRowPlaces;

            var temp_pricing = await priceService.GetPricings();
            var pricing = temp_pricing.Data.FirstOrDefault(x => x.fk_hall == hall.Data.hall_id);
            ticketViewModel.pricing = pricing.price;

            return View(ticketViewModel);
        }

        [Authorize]
        public async Task<bool> changeStatusToWaiting(int id)
        {
            var sessionRowPlace = await sessionRowPlaceService.GetSessionRowPlace(Convert.ToInt16(id));
            var user = await userService.GetUserByLogin(User.Identity.Name);

            if(sessionRowPlace.Data.fk_user != 0)
            {
                return false;
            }

            sessionRowPlace.Data.status = "Waiting";
            sessionRowPlace.Data.fk_user = user.Data.user_id;
            await sessionRowPlaceService.Edit(sessionRowPlace.Data);

            return true;
        }

        [Authorize]
        public async Task<bool> changeStatusToAvailable(int id)
        {
            var sessionRowPlace = await sessionRowPlaceService.GetSessionRowPlace(Convert.ToInt16(id));
            var user = await userService.GetUserByLogin(User.Identity.Name);

            if (sessionRowPlace.Data.fk_user != user.Data.user_id)
            {
                return false;
            }

            sessionRowPlace.Data.fk_user = 0;
            sessionRowPlace.Data.status = "Available";
            await sessionRowPlaceService.Edit(sessionRowPlace.Data);

            return true;
        }
    }
}
