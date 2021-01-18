using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.ViewModels.Clients
{
    public class ClientSearchResultViewModel
    {

        public IEnumerable<TrainRoute> TrainRoutes { get; set; }
    }
}
