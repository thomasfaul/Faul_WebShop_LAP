﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame_v2.Web.Models
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}