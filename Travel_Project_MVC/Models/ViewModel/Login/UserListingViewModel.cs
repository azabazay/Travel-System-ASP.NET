using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Project_MVC.Models.ViewModel.Login
{
    public class UserListingViewModel
    {
        public System.Guid UserId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}