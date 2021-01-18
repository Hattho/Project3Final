using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ApplicationLogic.DataModel
{

    public class TrainSchedule
    {
        public Guid TrainScheduleID { set; get; }

        public DateTime DepartureDate { set; get; }
        public string DepartureCity { set; get; }
        public string DepartureHour { set; get; }

        public DateTime ArrivalDate { set; get; }
        public string ArrivalCity  { set; get; }
        public string ArrivalHour { set; get; }
    }
}
