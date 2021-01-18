using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ApplicationLogic.DataModel
{
 
    public class Admin
    {
        [Key]
        public Guid AdminID { set; get; }
        public Guid UserID { set; get; }


        public string? Code { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }


    }
}
