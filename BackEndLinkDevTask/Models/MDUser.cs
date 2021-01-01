using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndLinkDevTask.Models
{
    public class MDUser : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public string Name { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public string Address { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public string Phone { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public DateTime? Birthdate { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public string Email { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public string Password { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public bool IsAdmin { get; set; }

        public string Token { get; set; }
        public MDUser() { }

        public static MDUser AddCustomerUser(MDUser user)
        {
            user.IsAdmin = false;
            user.Email = user.Email.ToLower();
            if (!CheckIfExists(user))
            {
                MDUser userAdded = user.Add<MDUser>(out string msg);
                return userAdded;
            }
            else
            {
                return null;
            }
        }
        //return userid if success and zero if failed
        public static int Login(string email,string password)
        {
            MDUser user = new MDUser().GetByParameter<MDUser>("Email", email.ToLower(), out string msg).FirstOrDefault();
            if (user == null) { return 0; }
            if (user.Password == password) { return user.Id; }
            else return 0;
        }

        private static bool CheckIfExists(MDUser user)
        {
            MDUser existUser = user.GetByParameter<MDUser>("Email", user.Email, out string msg).FirstOrDefault();
            if (existUser == null) { return false; }
            else return true;
        }

        public static MDUser GetById(int id)
        {
            MDUser user = new MDUser().GetByParameter<MDUser>("Id", id.ToString(), out string msg).FirstOrDefault();
            return user;
        }

        public override string GetTableName()
        {
            return "TBUsers";
        }
    }
}