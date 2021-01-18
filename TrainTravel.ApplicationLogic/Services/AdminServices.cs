using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;
using TrainTravel.ApplicationLogic.Exceptions;

namespace TrainTravel.ApplicationLogic.Services
{
    public class AdminServices
    {
        private IAdminRepository adminRepository;
        private ITrainRouteRepository trainRouteRepository;
        private ITrainTypeRepository trainTypeRepository;
        private ITrainScheduleRepository trainScheduleRepository;
        private IReservationRepository reservationRepository;

        public AdminServices(IAdminRepository adminRepository, ITrainRouteRepository trainRouteRepository,
                ITrainTypeRepository trainTypeRepository, ITrainScheduleRepository trainScheduleRepository,
                IReservationRepository reservationRepository)
        {
            this.adminRepository = adminRepository;
            this.trainRouteRepository = trainRouteRepository;
            this.trainTypeRepository = trainTypeRepository;
            this.trainScheduleRepository = trainScheduleRepository;
            this.reservationRepository = reservationRepository;
        }

        public Admin CreateAdmin(string userId, string firstName, string lastName)
        {
            var admin = new Admin
            {
                UserID = Guid.Parse(userId),
                FirstName = firstName,
                LastName = lastName
            };
            adminRepository.Add(admin);
            return admin;
        }

        public void EditAccount(Guid userId, string firstName, string lastName)
        {

            var admin = adminRepository.GetAdminByUserId(userId);

            admin.FirstName = firstName;
            admin.LastName = lastName;

            adminRepository.Update(admin);
        }

        public Admin GetAdminById(string adminId)
        {
            Guid adminIdGuid = Guid.Empty;
            if (!Guid.TryParse(adminId, out adminIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            var admin = adminRepository.GetById(adminIdGuid);
            if (admin == null)
            {
                throw new EntityNotFoundException(adminIdGuid);
            }

            return admin;

        }


        public TrainType GetTrainTypeById(string trainTypeId)
        {
            Guid trainTypeIdGuid = Guid.Empty;
            if (!Guid.TryParse(trainTypeId, out trainTypeIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            var trainType = trainTypeRepository.GetById(trainTypeIdGuid);
            if (trainType == null)
            {
                throw new EntityNotFoundException(trainTypeIdGuid);
            }

            return trainType;

        }

        public TrainRoute GetTrainRouteById (Guid trainRouteId)
        {
            var trainRoute = trainRouteRepository.GetTrainRouteByID(trainRouteId);

            if(trainRoute == null)
            {
                throw new EntityNotFoundException(trainRouteId);
            }
            return trainRoute;
        }

        public IEnumerable<TrainRoute> GetAdminTrainRoute(string adminId)
        {
            Guid adminIdGuid = Guid.Empty;
            if (!Guid.TryParse(adminId, out adminIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            return trainRouteRepository.GetAll()
                                .Where(trainRoute => trainRoute.Admin != null && trainRoute.Admin.AdminID == adminIdGuid)
                                .AsEnumerable();
        }

        public void AddTrainRoute(string adminId, string trainRouteNo, decimal classPrice1,
                                  decimal classPrice2, DateTime departureDate, string departureCity,
                                  string departureHour, DateTime arrivalDate, string arrivalCity, string arrivalHour,
                                  Guid trainTypeId)
        {

            Guid adminIdGuid = Guid.Empty;
            if (!Guid.TryParse(adminId, out adminIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var admin = adminRepository.GetAdminByUserId(adminIdGuid);
            if (admin == null)
            {
                throw new EntityNotFoundException(adminIdGuid);
            }

            var trainType = trainTypeRepository.GetTrainTypeByID(trainTypeId);


            var trainSchedule = trainScheduleRepository.Add(new TrainSchedule()
            {
                TrainScheduleID = Guid.NewGuid(),
                DepartureDate = departureDate,
                DepartureCity = departureCity,
                DepartureHour = departureHour,
                ArrivalCity = arrivalCity,
                ArrivalDate = arrivalDate,
                ArrivalHour = arrivalHour
            });

            var trainRoute = trainRouteRepository.Add(new TrainRoute()
            {
                TrainRouteID = Guid.NewGuid(),
                Admin = admin,
                TrainRouteNo = trainRouteNo,
                Class1Price = classPrice1,
                Class2Price = classPrice2,
                TicketsLeftC1 = trainType.CapacityClass1,
                TicketsLeftC2 = trainType.CapacityClass2,
                TrainSchedule = trainSchedule
            });


            trainType.TrainRoutes.Add(trainRoute);
            trainTypeRepository.Update(trainType);

        }

        //public void EditTrainRoute
        public void EditTrainRoute(Guid TrainRouteID, string trainRouteNo, decimal classPrice1,
                                  decimal classPrice2, DateTime departureDate, string departureCity,
                                  string departureHour, DateTime arrivalDate, string arrivalCity, string arrivalHour,
                                  int ticketsLeftC1, int ticketsLeftC2)
        {
            var route = trainRouteRepository.GetTrainRouteByID(TrainRouteID);
            route.TrainRouteNo = trainRouteNo;
            route.Class1Price = classPrice1;
            route.Class2Price = classPrice2;
            route.TicketsLeftC1 = ticketsLeftC1;
            route.TicketsLeftC2 = ticketsLeftC2;
            route.TrainSchedule.DepartureCity = departureCity;
            route.TrainSchedule.DepartureDate = departureDate;
            route.TrainSchedule.DepartureHour = departureHour;
            route.TrainSchedule.ArrivalCity = arrivalCity;
            route.TrainSchedule.ArrivalDate = arrivalDate;
            route.TrainSchedule.ArrivalHour = arrivalHour;

            trainRouteRepository.Update(route);

        }

        //Delete a Train Route
        public bool DeleteTrainRoute(TrainRoute trainRoute)
        {
            var trainSchedule = trainRoute.TrainSchedule;
            trainScheduleRepository.Delete(trainSchedule);

            //delete reservations
            var reservations = trainRoute.Reservations;
            reservationRepository.DeleteReservationsfromTrain(reservations);

            return trainRouteRepository.Delete(trainRoute);
        }


        //public void SearchTrainRoute


        //GetBy Fnc
        public IEnumerable<TrainRoute> GetRouteByAll(string departureCity, string arrivalCity, DateTime date)
        {
            if (departureCity != null && arrivalCity != null && date != null)
            {
                return trainRouteRepository.GetByAll(departureCity, arrivalCity, date);
            }
            if(departureCity != null && arrivalCity != null)
            {
                return trainRouteRepository.GetByDepArrCity(departureCity, arrivalCity);
            }
            if (departureCity != null)
            {
                return trainRouteRepository.GetByDepCity(departureCity);
            }
            if (arrivalCity != null)
            {
                return trainRouteRepository.GetByArivCity(arrivalCity);
            }
            if (date != null)
            {
                return trainRouteRepository.GetByDepDate(date);
            }
            return trainRouteRepository.GetAllRoutes();

        }

        public IEnumerable<TrainRoute> GetRouteByNo(string trainRouteNo)
        {
            return trainRouteRepository.GetByNo(trainRouteNo);   
        }
        public IEnumerable<TrainRoute> GetRouteByDepCity(string departureCity)
        {
            return trainRouteRepository.GetByDepCity(departureCity);
        }

        public IEnumerable<TrainRoute> GetRouteByArivCity(string arrivalCity)
        {
            return trainRouteRepository.GetByArivCity(arrivalCity);
        }


        public void AddTrainType( int capacityClass1, int capacityClass2, string modelType)
        {
            trainTypeRepository.Add(new TrainType()
            {
                TrainTypeID = Guid.NewGuid(),
                Model = modelType,
                CapacityClass1 = capacityClass1,
                CapacityClass2 = capacityClass2
            });

        }

        public IEnumerable<TrainType> GetTrainTypes()
        {
            return trainTypeRepository.GetAllTrainTypes();
        }

        public IEnumerable<TrainRoute> GetTrainRoutes()
        {
            return trainRouteRepository.GetAllRoutes();
        }




    }
}

