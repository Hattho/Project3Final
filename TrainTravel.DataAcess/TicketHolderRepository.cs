using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.DataAcess
{
    public class TicketHolderRepository : BaseRepository<TicketHolder>, ITicketHolderRepository
    {
        public TicketHolderRepository(TrainTravelDbContext dbContext) : base(dbContext)
        {


        }
    }
}
