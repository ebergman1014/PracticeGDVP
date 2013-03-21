using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardShop.Models;

namespace CardShop.Service
{
    public interface IAdminService
    {
        Store EditStore(Store ownedStore, Store changesToStore);

        Store OwnedStore(int ownerId);
    }
}
