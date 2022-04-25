
using SmallRatings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallRatings.Services
{
    interface IProDataService
    {
        /*List<ProInfo> GetAllUsers();*/
        int NewBusiness(ProInfo model);
        /*int Delete(ProInfo model);*/

        /*bool CheckUserExists(ProInfo model);*/
        int GetProID(int userId);
    }
}
