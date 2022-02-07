
using SmallRatings.Models;
using SmallRatings.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallRatings.Business
{
    /// <summary>
    /// This class is like a conduit for the DataAccess Layer and Presentation Layer
    /// </summary>
    public class CommWithDataAccess
    {
        UserDAO userDao = new UserDAO();
        /// <summary>
        /// Pass through the GetAllEmployees into Presentation Layer.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserInfo> GetAllUsers()
        {
            userDao = new UserDAO();
            IEnumerable<UserInfo> allUsers = userDao.GetAllUsers();
            return allUsers;
        }

        /// <summary>
        /// Pass the employee post data to our DataAccess Layer.
        /// </summary>
        /// <param name="emp"></param>
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