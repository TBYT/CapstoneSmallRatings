
using SmallRatings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallRatings.Services
{
    interface IUserDataService
    {
        List<UserInfo> GetAllUsers();
        bool Insert(UserInfo model);
        int Delete(UserInfo model);

        bool CheckUserExists(string username);
        UserInfo LoginUser(LoginInfo model);
    }
}
