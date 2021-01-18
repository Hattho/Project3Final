using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ApplicationLogic.DataModel
{

    public class TicketHolder
    {

        public Guid TicketHolderID { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string CNP { set; get; }
    }
}
