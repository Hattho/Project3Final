using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.DataAcess
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(TrainTravelDbContext dbContext) : base(dbContext)
        {

        }

        public Admin GetAdminByUserId(Guid userId)
        {
            return dbContext.Admins
                            .Where(admin => admin.UserID == userId)
                            .FirstOrDefault();
        }
    }
}
