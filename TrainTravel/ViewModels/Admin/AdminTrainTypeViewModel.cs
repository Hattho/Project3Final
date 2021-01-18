using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.ViewModels.Admin
{
    public class AdminTrainTypeViewModel
    {
        public IEnumerable<TrainType> TrainTypes { get; internal set; }

    }
}
