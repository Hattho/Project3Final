﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTravel.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        public string PhotoPath { get; set; }
    }
}