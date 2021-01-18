using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.DataAcess
{
    public class TrainTypeRepository : BaseRepository<TrainType>, ITrainTypeRepository
    {
        public TrainTypeRepository(TrainTravelDbContext dbContext) : base(dbContext)
        {

        }

        public TrainType GetTrainTypeByID(Guid Id)
        {

            return dbContext.TrainTypes
                            .Where(trainType => trainType.TrainTypeID == Id)
                            .Include(trainType => trainType.TrainRoutes)
                            .FirstOrDefault();
        }

        public IEnumerable<TrainType> GetAllTrainTypes()
        {
            return dbContext.TrainTypes
                             .Include(trainType => trainType.TrainRoutes)
                             .AsEnumerable();
        }

    }
}
