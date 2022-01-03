using System;
using System.Collections.Generic;

#nullable disable

namespace UserAPI.UserModel
{
    public partial class Username:Login
    {
        //public int UserId { get; set; }
        //public string UserName1 { get; set; }
        //public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public string Key { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
    }
}
