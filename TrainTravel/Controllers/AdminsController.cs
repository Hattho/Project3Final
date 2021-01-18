using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainTravel.ApplicationLogic.DataModel;
using TrainTravel.DataAcess;
using TrainTravel.ApplicationLogic.Services;
using TrainTravel.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TrainTravel.ViewModels;

namespace TrainTravel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminsController : Controller
    {
        private readonly AdminServices adminServices;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminsController(AdminServices adminServices, UserManager<ApplicationUser> userManager)
        {
            this.adminServices = adminServices;
            _userManager = userManager;
        }

        public ActionResult Index()
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

        [HttpGet]
        public IActionResult SearchRouteAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchRouteAdmin([FromForm]ClientSearchRouteViewModel modelType)
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
                

                return View(new AdminSearchResultViewModel { FutureRoutes = future_routes });
                
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }


        [HttpGet]
        public IActionResult AddTrainType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTrainType([FromForm]AdminAddTrainTypeViewModel modelType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            adminServices.AddTrainType(modelType.CapacityClass1, modelType.CapacityClass2, modelType.Model);
            return Redirect(Url.Action("TrainTypes", "Admins"));
        }

        [HttpGet]
        public IActionResult AddTrainRoute()
        {

            var trainTypes = adminServices.GetTrainTypes();
            AdminAddTrainRouteViewModel adminAddTrainRouteViewModel = new AdminAddTrainRouteViewModel
            {
                TrainTypes = new SelectList(trainTypes, "TrainTypeID", "Model")
            };
            return View(adminAddTrainRouteViewModel);
            //return View();

        }

        public IActionResult AddTrainRoute([FromForm]AdminAddTrainRouteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = _userManager.GetUserId(User);
            adminServices.AddTrainRoute(userId, model.TrainRouteNo, model.Class2Price, model.Class1Price,
                                        model.DepartureDate, model.DepartureCity, model.DepartureHour,
                                        model.ArrivalDate, model.ArrivalCity, model.ArrivalHour, model.TrainType);
            return Redirect(Url.Action("TrainRoutes", "Admins"));

        }

        public IActionResult DeleteTrainRoute(Guid Id)
        {
            var trainRoute = adminServices.GetTrainRouteById(Id);
            if (trainRoute == null)
            {
                return BadRequest("Train route not found");
            }
            else
            {
                var route = adminServices.DeleteTrainRoute(trainRoute);
                return Redirect(Url.Action("TrainRoutes", "Admins"));
            }
        }


        [HttpGet]
        public IActionResult EditTrainRoute(Guid Id)
        {
            var trainRoute = adminServices.GetTrainRouteById(Id);
            AdminEditTrainRouteViewModel adminEditTrainRouteViewModel = new AdminEditTrainRouteViewModel
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
                TicketsLeftC1 = trainRoute.TicketsLeftC1,
                TicketsLeftC2 = trainRoute.TicketsLeftC2

            };

            return View(adminEditTrainRouteViewModel);
        }

        [HttpPost]
        public IActionResult EditTrainRoute([FromForm]AdminEditTrainRouteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            adminServices.EditTrainRoute(model.TrainRouteId, model.TrainRouteNo, model.Class1Price, model.Class2Price,
                                         model.DepartureDate, model.DepartureCity, model.DepartureHour,
                                         model.ArrivalDate, model.ArrivalCity, model.ArrivalHour,
                                         model.TicketsLeftC1, model.TicketsLeftC2);

            return Redirect(Url.Action("TrainRoutes", "Admins"));

        }

        [HttpGet]
        public IActionResult DetailsTrainRoute(Guid Id)
        {
            var trainRoute = adminServices.GetTrainRouteById(Id);
            AdminDetailsTrainRouteViewModel adminDetailsTrainRouteViewModel = new AdminDetailsTrainRouteViewModel
            {
                TrainRoute = trainRoute
            };
            return View(adminDetailsTrainRouteViewModel);

        }

        [HttpGet]
        public IActionResult TrainRoutes()
        {
            try
            { 
                var trainRoutes = adminServices.GetTrainRoutes();

                return View(new AdminTrainRoutesViewModel { TrainRoutes = trainRoutes });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public IActionResult TrainTypes()
        {
            try
            {

                var trainTypes = adminServices.GetTrainTypes();

                return View(new AdminTrainTypeViewModel { TrainTypes = trainTypes });
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

            var admin = adminServices.GetAdminById(userId);

            AdminEditAccountViewModel adminEditAccountViewModel = new AdminEditAccountViewModel
            {
                UserId = userId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = user_email,
                Username = user_username
            };
            return View(adminEditAccountViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditAccount([FromForm]AdminEditAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user_identity = await _userManager.FindByIdAsync(model.UserId);
            user_identity.Email = model.Email;
            user_identity.UserName = model.Username;

            var result = await _userManager.UpdateAsync(user_identity);

            Guid adminIdGuid = Guid.Empty;
            if (!Guid.TryParse(model.UserId, out adminIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            adminServices.EditAccount(adminIdGuid, model.FirstName, model.LastName);
            return Redirect(Url.Action("Index", "Admins"));
        }
    }
   
}
