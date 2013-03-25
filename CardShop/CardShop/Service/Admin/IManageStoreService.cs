using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardShop.Models;

namespace CardShop.Service
{
    public interface IManageStoreService
    {
        Store EditStore(Store ownedStore, Store changesToStore, out bool success);

        Store OwnedStore(int ownerId);
    }
}
