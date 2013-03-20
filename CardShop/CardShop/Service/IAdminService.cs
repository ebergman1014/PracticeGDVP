﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardShop.Models;

namespace CardShop.Service
{
    public interface IAdminService
    {
        Store EditDiscount(Store changesToStore);

        Store OwnedStore(int ownerId);
    }
}
