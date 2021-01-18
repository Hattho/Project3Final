using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.ApplicationLogic.Abstractions
{
    public interface IReservationRepository : IRepository<Reservation>
    {

        public IEnumerable<Reservation> GetAllReservations();

        public void DeleteReservationsfromTrain(ICollection<Reservation> reservations);

        public IEnumerable<Reservation> GetAllReservationsForClient(Guid Id);

    }
}
