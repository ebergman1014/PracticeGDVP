﻿using CardShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShop.Auth
{
    public interface IUserAuth
    {
        User User { get; set; }
        User ActingAs { get; set; }

        User getActingUser();
    }
}
