using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.DataAcess
{
    public class trainScheduleRepository : BaseRepository<TrainSchedule>, ITrainScheduleRepository
    {

        public trainScheduleRepository(TrainTravelDbContext dbContext) : base(dbContext)
        {

        }
    }
}
