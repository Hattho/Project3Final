using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.DataAcess
{
    public class TrainRouteRepository : BaseRepository<TrainRoute>, ITrainRouteRepository
    {
        public TrainRouteRepository(TrainTravelDbContext dbContext):base(dbContext)
        {

        }

        public TrainRoute GetTrainRouteByID( Guid Id)
        {
            var trainRoute = dbContext.TrainRoutes
                .Where(route => route.TrainRouteID == Id)
                .Include(route => route.TrainSchedule)
                .Include(route => route.Admin)
                .Include(route => route.Reservations).ThenInclude(rez => rez.TicketHolder)
                .FirstOrDefault();
            return trainRoute;
        }

        public IEnumerable<TrainRoute> GetAllRoutes()
        {
            return dbContext.TrainRoutes
                             .Include(route => route.TrainSchedule)
                             .Include(route => route.Admin)
                             .Include(route => route.Reservations)
                             .AsEnumerable();
        }


        public IEnumerable<TrainRoute> GetByAll (string departureCity, string arrivalCity, DateTime departureDate)
        {
            return dbContext.TrainRoutes
                                .Where(x => ( x.TrainSchedule.DepartureCity == departureCity || departureCity == null) &&
                                         ( x.TrainSchedule.ArrivalCity == arrivalCity || arrivalCity == null) &&
                                         ( x.TrainSchedule.DepartureDate.Date == departureDate.Date || departureDate == null))
                                .Include(route => route.TrainSchedule)
                                .Include(route => route.Admin)
                                .Include(route => route.Reservations)
                                .AsEnumerable();
        }

        public IEnumerable<TrainRoute> GetByNo(string trainRouteNo)
        {
            return dbContext.TrainRoutes
                             .Where( x => x.TrainRouteNo == trainRouteNo || trainRouteNo == null )
                             .Include(route => route.TrainSchedule)
                             .Include(route => route.Admin)
                             .Include(route => route.Reservations)
                             .AsEnumerable();
        }

        public IEnumerable<TrainRoute> GetByDepCity(string departureCity)
        {
            return dbContext.TrainRoutes
                             .Where(x => x.TrainSchedule.DepartureCity == departureCity || departureCity == null)
                             .Include(route => route.TrainSchedule)
                             .Include(route => route.Admin)
                             .Include(route => route.Reservations)
                             .AsEnumerable();
        }

        public IEnumerable<TrainRoute> GetByArivCity(string arrivalCity)
        {
            return dbContext.TrainRoutes
                             .Where(x => x.TrainSchedule.ArrivalCity == arrivalCity || arrivalCity == null)
                             .Include(route => route.TrainSchedule)
                             .Include(route => route.Admin)
                             .Include(route => route.Reservations)
                             .AsEnumerable();
        }

        public IEnumerable<TrainRoute> GetByDepArrCity(string departureCity, string arrivalCity)
        {
            return dbContext.TrainRoutes
                             .Where(x => x.TrainSchedule.DepartureCity == departureCity || departureCity == null)
                             .Where(x => x.TrainSchedule.ArrivalCity == arrivalCity || arrivalCity == null)
                             .Include(route => route.TrainSchedule)
                             .Include(route => route.Admin)
                             .Include(route => route.Reservations)
                             .AsEnumerable();
        }


        public IEnumerable<TrainRoute> GetByDepDate(DateTime departureDate)
        {
            return dbContext.TrainRoutes
                             .Where(x => x.TrainSchedule.DepartureDate == departureDate || departureDate == null)
                             .Include(route => route.TrainSchedule)
                             .Include(route => route.Admin)
                             .Include(route => route.Reservations)
                             .AsEnumerable();
        }
        public IEnumerable<TrainRoute> GetByArivDate(DateTime arrivalDate)
        {
            return dbContext.TrainRoutes
                             .Where(x => x.TrainSchedule.ArrivalDate == arrivalDate || arrivalDate == null)
                             .Include(route => route.TrainSchedule)
                             .Include(route => route.Admin)
                             .Include(route => route.Reservations)
                             .AsEnumerable();
        }
    }
}
