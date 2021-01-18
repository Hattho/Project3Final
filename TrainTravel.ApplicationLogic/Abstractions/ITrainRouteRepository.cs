using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.ApplicationLogic.Abstractions
{
    public interface ITrainRouteRepository :IRepository<TrainRoute>
    {
        public TrainRoute GetTrainRouteByID(Guid Id);
        public IEnumerable<TrainRoute> GetAllRoutes();
        public IEnumerable<TrainRoute> GetByAll(string departureCity, string arrivalCity, DateTime departureDate);
        public IEnumerable<TrainRoute> GetByNo(string trainRouteNo);
        IEnumerable<TrainRoute> GetByDepCity(string departureCity);
        IEnumerable<TrainRoute> GetByArivCity(string arrivalCity);
        IEnumerable<TrainRoute> GetByDepArrCity(string departureCity, string arrivalCity);
        IEnumerable<TrainRoute> GetByDepDate(DateTime departureDate);
        IEnumerable<TrainRoute> GetByArivDate(DateTime arrivalDate);


    }
}
