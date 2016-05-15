using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

using jsk.goBudgetMe.Models;

namespace jsk.goBudgetMe.Services
{
    public class AccountService : IAccountService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationUser _currentUser;
        private string _currentUserName;

        public AccountService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _currentUser = null;
            _currentUserName = null;
        }

        public ApplicationUser CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }

                if (string.IsNullOrEmpty(_currentUserName))
                {
                    // Get the user name of the currently logged in user. 
                    _currentUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
                }

                if (!string.IsNullOrEmpty(_currentUserName))
                {
                    _currentUser = _userManager.FindByNameAsync(_currentUserName).Result;
                    if (_currentUser == null)
                    {
                        string errMsg = string.Format("User with id {0} is authenticated but no user record is found.", _currentUserName);
                        throw new Exception(errMsg);
                    }
                }
                return _currentUser;
            }
        }

        public string CurrentUserId
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentUserName))
                {
                    return _currentUserName;
                }

                // Get the user ID of the currently logged in user. 
                // using System.Security.Claims;
                _currentUserName = _httpContextAccessor.HttpContext.User.Identity.Name;

                return _currentUserName;
            }
        }
    }
}
