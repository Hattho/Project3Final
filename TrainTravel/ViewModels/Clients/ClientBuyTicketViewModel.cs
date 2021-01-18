using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.ViewModels.Clients
{
    public class ClientBuyTicketViewModel
    {
        public string TicketType { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DiscountCode { get; set; }
        public Guid TrainRouteId { get; set; }

        public string TrainRouteNo { set; get; }
        public decimal Class1Price { set; get; }
        public decimal Class2Price { set; get; }
        public string TicketCategory { set; get; }

        public DateTime DepartureDate { set; get; }
        public string DepartureCity { set; get; }
        public string DepartureHour { set; get; }

        public DateTime ArrivalDate { set; get; }
        public string ArrivalCity { set; get; }
        public string ArrivalHour { set; get; }

    }
}
