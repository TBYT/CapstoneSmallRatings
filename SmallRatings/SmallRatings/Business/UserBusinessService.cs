
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
    public class UserBusinessService
    {
        UserDAO userDao = new UserDAO();
       
        public IEnumerable<UserInfo> GetAllUsers()
        {
            userDao = new UserDAO();
            IEnumerable<UserInfo> allUsers = userDao.GetAllUsers();
            return allUsers;
        }

        public bool CheckUserExists(string username)
        {
            userDao = new UserDAO();
            return userDao.CheckUserExists(username);
        }

        public bool AddUser(UserInfo user)
        {
            userDao = new UserDAO();
            return userDao.Insert(user);
        }

        public UserInfo LoginUser(LoginInfo user)
        {
            userDao = new UserDAO();
            return userDao.LoginUser(user);
        }

        public bool UpdateUser(UserInfo obj)
        {
            userDao = new UserDAO();
            return userDao.UpdateUser(obj);
        }
    }
}