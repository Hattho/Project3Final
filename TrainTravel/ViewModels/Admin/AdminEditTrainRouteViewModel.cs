using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ViewModels.Admin
{
    public class AdminEditTrainRouteViewModel
    {

        public Guid TrainRouteId { set; get; }
        public string TrainRouteNo { set; get; }

        public decimal Class1Price { set; get; }
        public decimal Class2Price { set; get; }

        public int TicketsLeftC1 { set; get; }
        public int TicketsLeftC2 { set; get; }

        //public TrainSchedule TrainSchedule { set; get; }

        public DateTime DepartureDate { set; get; }
        public string DepartureCity { set; get; }
        public string DepartureHour { set; get; }

        public DateTime ArrivalDate { set; get; }
        public string ArrivalCity { set; get; }
        public string ArrivalHour { set; get; }
    }
}
