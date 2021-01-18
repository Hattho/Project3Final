using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.DataAcess
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(TrainTravelDbContext dbContext) : base(dbContext)
        {

        }
        public Client GetClientByUserId(Guid userId)
        {
            return dbContext.Clients
                            .Where(client => client.UserID == userId)
                            .FirstOrDefault();
        }

    }
}
