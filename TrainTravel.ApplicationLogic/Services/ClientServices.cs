using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.DataModel;
using TrainTravel.ApplicationLogic.Exceptions;

namespace TrainTravel.ApplicationLogic.Services
{
    public class ClientServices
    {
        private IClientRepository clientRepository;
        private IReservationRepository reservationRepository;
        private ITicketHolderRepository ticketHolderRepository;
        private ITrainRouteRepository trainRouteRepository;

        public ClientServices(IClientRepository clientRepository, IReservationRepository reservationRepository,
                              ITicketHolderRepository ticketHolderRepository, 
                              ITrainRouteRepository trainRouteRepository) 
        {
            this.clientRepository = clientRepository;
            this.reservationRepository = reservationRepository;
            this.ticketHolderRepository = ticketHolderRepository;
            this.trainRouteRepository = trainRouteRepository;
        }

        public Client CreateClient(string userId, string firstName, string lastName, string email, string phoneNo)
        {
            var client = new Client
            {
                UserID = Guid.Parse(userId),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNo = phoneNo
            };
            clientRepository.Add(client);
            return client;
        }

        public Client GetClientById(string clientId)
        {
            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(clientId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            return client;

        }

        public bool BuyTicket(string clientId, string firstName, string lastName,
                              string discountCode, string ticketCategory, Guid TrainRouteId, string ticketType)
        {
            int seat_appointed = 0;
            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(clientId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            var trainRoute = trainRouteRepository.GetTrainRouteByID(TrainRouteId);

            var ticketHolder = ticketHolderRepository.Add(new TicketHolder()
            {

                TicketHolderID = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                CNP = discountCode
            });

            
            if(ticketCategory == "TicketsLeftC1")
            {
                if (trainRoute.TicketsLeftC1 > 0)
                {
                    var seat = trainRoute.TicketsLeftC1;
                    trainRoute.TicketsLeftC1--;
                    trainRouteRepository.Update(trainRoute);
                    seat_appointed = seat;
                }
                else
                {
                    return false;
                }
            }
            else if ( ticketCategory == "TicketsLeftC2")
            {
                if (trainRoute.TicketsLeftC2 > 0)
                {
                    var seat = trainRoute.TicketsLeftC2;
                    trainRoute.TicketsLeftC2--;
                    trainRouteRepository.Update(trainRoute);
                    seat_appointed = seat;
                }
                else
                {
                    return false;
                }
            }

            

            var reservation = reservationRepository.Add(new Reservation()
            {
                ReservationID = Guid.NewGuid(),
                Client = client,
                TicketHolder = ticketHolder,
                Seat = seat_appointed,
                TicketType = ticketType          
            });

            if (trainRoute != null)
            {
                trainRoute.Reservations.Add(reservation);
                trainRouteRepository.Update(trainRoute);
                return true;
            }
            return false;

        }

        public IEnumerable<Reservation> GetReservations(string userId)
        {
            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            return reservationRepository.GetAllReservationsForClient(client.UserID);

        }

        public void EditAccount(Guid userId, string firstName, string lastName, string email, string phoneNo)
        {

            var client = clientRepository.GetClientByUserId(userId);

            client.FirstName = firstName;
            client.LastName = lastName;
            client.Email = email;
            client.PhoneNo = phoneNo;

            clientRepository.Update(client);
        }





    }
}
