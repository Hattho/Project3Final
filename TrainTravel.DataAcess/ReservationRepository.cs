using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTravel.ApplicationLogic.DataModel;
using TrainTravel.ApplicationLogic.Abstractions;

namespace TrainTravel.DataAcess
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {

        public ReservationRepository(TrainTravelDbContext dbContext) : base(dbContext)
        {


        }

        public Reservation GetReservationByID(Guid Id)
        {

            return dbContext.Reservations
                            .Where(res => res.ReservationID == Id)
                            .Include(res => res.TicketHolder)
                            .Include(res => res.Client)
                            .FirstOrDefault();
        }
        public IEnumerable<Reservation> GetAllReservations()
        {
            return dbContext.Reservations
                             .Include(reservation => reservation.TicketHolder)
                             .Include(reservation => reservation.Client)
                             .AsEnumerable();
        }

        public IEnumerable<Reservation> GetAllReservationsForClient(Guid Id)
        {
            return dbContext.Reservations
                             .Where(res => res.Client.UserID == Id)
                             .Include(reservation => reservation.TicketHolder)
                             .Include(reservation => reservation.Client)
                             .AsEnumerable();
        }

        public void DeleteReservationsfromTrain(ICollection<Reservation> reservations)
        {
            foreach (var rez in reservations)
                dbContext.Reservations.Remove(rez);
            dbContext.SaveChanges();
        }
    }
}
