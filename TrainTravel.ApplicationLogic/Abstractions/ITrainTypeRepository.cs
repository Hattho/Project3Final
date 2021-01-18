using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.ApplicationLogic.Abstractions
{
    public interface ITrainTypeRepository: IRepository<TrainType>
    {
        public TrainType GetTrainTypeByID(Guid Id);

        public IEnumerable<TrainType> GetAllTrainTypes();


    }
}
