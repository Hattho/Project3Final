using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ApplicationLogic.DataModel
{
    public class Client
    {
        [Key]
        public Guid ClientID { set; get; }

        public Guid UserID { set; get; }

        public string FirstName{ set; get; }

        public string LastName { set; get; }

        public string Email{ set; get; }

        public string PhoneNo { set; get; }


    }
}
