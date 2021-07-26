using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// user model class that i created to map data from the users.json file
/// </summary>
namespace CohesionIB.ApiEngineer.CodeChallenge.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool TermsAndConditions { get; set; }
        public ulong InvitationCode { get; set; }

        [DefaultValue(default(List<long>))]
        public List<long> DeviceID { get; set; }

    }

    public class UserList
    {
        public List<User> users { get; set; }
    }

}
