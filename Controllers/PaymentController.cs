using Cinema.Models;
using Cinema.Models.Queries;
using Cinema.Models.ViewModels;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace Cinema.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ISessionRowPlaceService sessionRowPlaceService;
        private readonly ISessionService sessionService;
        private readonly IHallService hallService;
        private readonly IFilmService filmService;
        private readonly ITicketService ticketService;
        private readonly IPricingService pricingService;
        private readonly IUserService userService;
        private readonly IPricingService priceService;
        public PaymentController(ISessionRowPlaceService sessionRowPlaceService, ISessionService sessionService, IHallService hallService, IFilmService filmService,
            ITicketService ticketService, IPricingService pricingService, IUserService userService, IPricingService priceService)
        {
            this.sessionRowPlaceService = sessionRowPlaceService;
            this.sessionService = sessionService;
            this.hallService = hallService;
            this.filmService = filmService;
            this.ticketService = ticketService;
            this.pricingService = pricingService;
            this.userService = userService;
            this.priceService = priceService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] PaymentQuery paymentQuery)
        {
            var user = await userService.GetUserByLogin(User.Identity.Name);

            var temp_places = await sessionRowPlaceService.GetSessionRowPlaces();
            var places = temp_places.Data.Where(x => x.fk_session == paymentQuery.id && x.status == "Waiting" && x.fk_user == user.Data.user_id).ToList();

            PaymentViewModel paymentViewModel = new PaymentViewModel();

            paymentViewModel.sessionRowPlaces = places; paymentViewModel.ticketsCount = places.Count;

            var session = await sessionService.GetSession(paymentQuery.id);
            paymentViewModel.session = session.Data;

            var hall = await hallService.GetHall(session.Data.fk_hall);
            paymentViewModel.hall = hall.Data;
                  
            var film = await filmService.GetFilm(session.Data.fk_film);
            paymentViewModel.film = film.Data;

            var temp_pricing = await priceService.GetPricings();
            var pricing = temp_pricing.Data.FirstOrDefault(x => x.fk_hall == hall.Data.hall_id);
            var finale_price = pricing.price * places.Count;
            paymentViewModel.finale_price = finale_price;

            return View(paymentViewModel);
        }

        [Authorize]
        public async Task<bool> changeStatusToSold(List<int> id)
        {
            foreach (var srp_id_check in id)
            {
                var sessionRowPlace = await sessionRowPlaceService.GetSessionRowPlace(srp_id_check);

                if(sessionRowPlace.Data.status == "Sold")
                {
                    return false;
                }
            }

            foreach(var srp_id in id) {
                var sessionRowPlace = await sessionRowPlaceService.GetSessionRowPlace(srp_id);

                sessionRowPlace.Data.status = "Sold";

                await sessionRowPlaceService.Edit(sessionRowPlace.Data);

                var session = await sessionService.GetSession(sessionRowPlace.Data.fk_session);
                var hall = await hallService.GetHall(session.Data.fk_hall);

                var user = await userService.GetUserByLogin(User.Identity.Name);
                var price = await pricingService.GetPricingByH(hall.Data.hall_number);

                Ticket ticket = new Ticket();
                ticket.sale_date = DateTime.Now;
                ticket.fk_srp = srp_id;
                ticket.fk_price = price.Data.pricing_id;
                ticket.fk_user = user.Data.user_id;
                await ticketService.CreateTicket(ticket);
            }

            return true;
        }
    }
}
