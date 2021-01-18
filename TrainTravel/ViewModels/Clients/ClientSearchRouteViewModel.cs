using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ViewModels.Clients
{
    public class ClientSearchRouteViewModel
    {
        public string DepartureCity { set; get; }

        public string ArrivalCity { set; get; }

        public DateTime DepartureDate { set; get; }
    }
}
