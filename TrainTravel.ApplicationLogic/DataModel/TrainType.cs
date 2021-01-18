using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ApplicationLogic.DataModel
{

    public class TrainType
    {

        public Guid TrainTypeID { set; get; }

        public string Model { set; get; }

        public int CapacityClass1 { set; get; }

        public int CapacityClass2 { set; get; }

        public ICollection<TrainRoute>? TrainRoutes { get; set; }
    }
}
