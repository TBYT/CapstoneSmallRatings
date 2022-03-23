﻿
using SmallRatings.Models;
using SmallRatings.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallRatings.Business
{
    /// <summary>
    /// This class is like a conduit for the User DataAccess Layer and Presentation Layer
    /// </summary>
    public class ProBusinessService
    {
        ProDAO proDao = new ProDAO();
       /*
        public IEnumerable<ProInfo> GetAllUsers()
        {
            proDao = new ProDAO();
            IEnumerable<ProInfo> allUsers = proDao.GetAllUsers();
            return allUsers;
        }*/

        public bool CheckDupe(ProInfo proInfo)
        {
            proDao = new ProDAO();
            return proDao.CheckDuplication(proInfo);
        }

        public bool NewBusiness(ProInfo proInfo)
        {
            proDao = new ProDAO();
            return proDao.NewBusiness(proInfo);
        }

        public int GetPro(int userId)
        {
            return proDao.GetProID(userId);
        }
        /*
       public bool AddUser(ProInfo user)
       {
           proDao = new ProDAO();
           return proDao.Insert(user);
       }

       public UserInfo LoginUser(ProInfo user)
       {
           proDao = new ProDAO();
           return proDao.LoginUser(user);
       }

       public bool UpdateUser(ProInfo obj)
       {
           proDao = new ProDAO();
           return proDao.UpdateUser(obj);
       }*/
    }
}