using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Model
{
    public class UserCredentials {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class User : UserCredentials
    {
        public string Salt { get; set; }
        public List<Role> Roles { get; set; }
        public List<Region> Regions { get; set; }
    }
}