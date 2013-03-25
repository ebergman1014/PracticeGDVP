using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardShop.Models;

namespace CardShop.Service.Admin
{
    public interface IManageUserService
    {
        User DeleteUser(int id, out bool isSuccess);
        List<User> GetAllUsers(out bool isSuccess);
        User GetUser(int id, out bool isSuccess);
        User CreateUser(User user, out bool isSuccess);

        IEnumerable GetRoleView();

        User EditUser(User user, System.Data.EntityState entityState, out bool isSuccess);

        void ActAsUser(int id, out bool success);

        void StopActingAsUser(out bool success);
    }
}
