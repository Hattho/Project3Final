using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace TrainTravel.ApplicationLogic.DataModel
{

    public class TrainRoute
    {
        public Guid TrainRouteID { set; get; }

        public string TrainRouteNo { set; get; }

        public Admin? Admin { set; get; }

        public TrainSchedule? TrainSchedule { set; get; }

        public decimal Class1Price { set; get; }

        public decimal Class2Price { set; get; }

        public int TicketsLeftC1 { set; get; }
        public int TicketsLeftC2 { set; get; }

        public ICollection<Reservation>? Reservations { get; set; }


    }
}
