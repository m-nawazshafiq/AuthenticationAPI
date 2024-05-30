using AppDataAccess.Model;
using AppDataAccess.Repository;
using AppDataAccess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess
{
    public class UserRepository : IRepository<User>
    {
        public List<User> users;
        static UserRepository instance { get; set; }
        public static UserRepository Instance { get { if (instance == null) instance = new UserRepository(); return instance; } }
        public UserRepository() {
            User usr1 = new User();
            usr1.UserName = "Nawaz";
            usr1.Salt = Md5Hash.GenerateSalt(10);
            usr1.Password = Md5Hash.ComputeHash("1122", usr1.Salt);
            usr1.Roles = new List<Role>() { new Role() { Name = "Player" } };
            usr1.Regions = new List<Region>() { new Region() { Name = "b_game" } };

            User usr2 = new User();
            usr2.UserName = "Hassan";
            usr2.Salt = Md5Hash.GenerateSalt(10);
            usr2.Password = Md5Hash.ComputeHash("1133", usr1.Salt);
            usr2.Roles = new List<Role>() { new Role() { Name = "Admin" } };
            usr2.Regions = new List<Region>() { new Region() { Name = "vip_chararacter_personalize" } };
            users = new List<User>() { usr1, usr2 };
        }
        public User GetFirstOrDefault(Func<User, bool> filter)
        {
            return users.FirstOrDefault(filter);
        }
    }
}
