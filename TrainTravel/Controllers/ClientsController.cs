using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainTravel.ApplicationLogic.Services;
using TrainTravel.ViewModels;
using TrainTravel.ViewModels.Clients;

namespace TrainTravel.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientsController : Controller
    {

        private readonly ClientServices clientServices;
        private readonly AdminServices adminServices;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientsController(ClientServices clientServices, AdminServices adminServices,
                                 UserManager<ApplicationUser> userManager)
        {
            this.clientServices = clientServices;
            this.adminServices = adminServices;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [HttpGet]
        public IActionResult SearchRoute()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchRoute([FromForm]ClientSearchRouteViewModel modelType)
        {

            return RedirectToAction("SearchResult", new
            {
                DepartureCity = modelType.DepartureCity,
                ArrivalCity = modelType.ArrivalCity,
                DepartureDate = modelType.DepartureDate,
            });
        }

        [HttpGet]
        public IActionResult SearchResult(string departureCity, string arrivalCity, DateTime departureDate)
        {
            try
            {

                var future_routes = adminServices.GetRouteByAll(departureCity, arrivalCity, departureDate);

                return View(new ClientSearchResultViewModel { TrainRoutes = future_routes });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public IActionResult BuyTicket (Guid Id)
        {
            var trainRoute = adminServices.GetTrainRouteById(Id);
            ClientBuyTicketViewModel clientBuyTicketViewModel = new ClientBuyTicketViewModel
            {
                TrainRouteId = trainRoute.TrainRouteID,
                TrainRouteNo = trainRoute.TrainRouteNo,
                Class1Price = trainRoute.Class1Price,
                Class2Price = trainRoute.Class2Price,
                DepartureCity = trainRoute.TrainSchedule.DepartureCity,
                DepartureDate = trainRoute.TrainSchedule.DepartureDate,
                DepartureHour = trainRoute.TrainSchedule.DepartureHour,
                ArrivalCity = trainRoute.TrainSchedule.ArrivalCity,
                ArrivalDate = trainRoute.TrainSchedule.ArrivalDate,
                ArrivalHour = trainRoute.TrainSchedule.ArrivalHour,
            };
            return View(clientBuyTicketViewModel);

        }
        public IActionResult BuyTicket ([FromForm]ClientBuyTicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = _userManager.GetUserId(User);
            var return1 = clientServices.BuyTicket(userId, model.FirstName, model.LastName, model.DiscountCode,model.TicketCategory, model.TrainRouteId, model.TicketType);
            if (return1)
            {
                return Redirect(Url.Action("Index", "Clients"));
            }else
            {
                return BadRequest("No More Tickets of this category");
            }

        }

        [HttpGet]
        public IActionResult Reservations()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var reservations = clientServices.GetReservations(userId);

                return View(new ClientReservationViewModel { Reservations = reservations });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditAccount()
        {
            var userId = _userManager.GetUserId(User);

            var user_identity = await _userManager.FindByIdAsync(userId);
            var user_email = await _userManager.GetEmailAsync(user_identity);
            var user_username = await _userManager.GetUserNameAsync(user_identity);

            var client = clientServices.GetClientById(userId);

            ClientEditAccountViewModel clientEditAccountViewModel = new ClientEditAccountViewModel
            {
                UserId = userId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNo = client.PhoneNo,
                Email = user_email,
                Username = user_username
            };
            return View(clientEditAccountViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount([FromForm]ClientEditAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user_identity = await _userManager.FindByIdAsync(model.UserId);
            user_identity.Email = model.Email;
            user_identity.UserName = model.Username;

            var result = await _userManager.UpdateAsync(user_identity);

            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(model.UserId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }


            clientServices.EditAccount(clientIdGuid, model.FirstName, model.LastName,model.Email,
                                         model.PhoneNo);
            return Redirect(Url.Action("Index", "Clients"));
        }



    }
}