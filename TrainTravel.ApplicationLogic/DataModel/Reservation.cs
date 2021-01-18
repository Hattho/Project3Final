using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ApplicationLogic.DataModel
{ 
    public class Reservation
    {

        public Guid ReservationID { set; get; }

        public Client? Client { set; get; }

        public TicketHolder? TicketHolder { set; get; }

        public int Seat { set; get; }

        public string TicketType { set; get; }

    }
}
