﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.ViewModels.Clients
{
    public class ClientsReservationViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
