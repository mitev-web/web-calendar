using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;
using WebCalendar.DAL;

namespace WebCalendar
{
    public class UserModel : DAO
    {
        public static bool UserIsValid(string user, string pass)
        {
            var u = (from e in db.Users where e.UserName == user select e).FirstOrDefault();

            if (u != null)
            {
                if (u.Password == UserDAL.CreatePasswordHash(pass, u.PasswordSalt))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        //private static string CreateSalt()
        //{
        //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //    byte[] buff = new byte[32];
        //    rng.GetBytes(buff);

        //    return Convert.ToBase64String(buff);
        //}

        //private static string CreatePasswordHash(string password, string salt)
        //{
        //    string saltAndPwd = "mixing" + password + "with some" + salt;

        //    System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    byte[] combined = encoder.GetBytes(saltAndPwd);
        //    hash.ComputeHash(combined);
        //    string hashCode = Convert.ToBase64String(hash.Hash);

        //    return hashCode;
        //}

        public MembershipUser CreateUser(string username, string password, string email)
        {
            User u = new User();

            u.UserName = username;
            u.Email = email;
            u.PasswordSalt = UserDAL.CreateSalt();
            u.Password = UserDAL.CreatePasswordHash(password, u.PasswordSalt);
            db.AddToUsers(u);
            db.SaveChanges();

            return CreateMembershipUser(u.UserName);
        }


        public static string GetUserNameByEmail(string email)
        {
            var result = from u in db.Users where (u.Email == email) select u;

            if (result.Count() != 0)
            {
                User u = result.FirstOrDefault();
                return u.UserName;
            }
            else
            {
                return "";
            }
        }

        public MembershipUser CreateMembershipUser(string username)
        {
            var result = from u in DAO.db.Users where (u.UserName == username) select u;

            if (result.Count() != 0)
            {
                var dbuser = result.FirstOrDefault();

                string _username = dbuser.UserName;
                int _providerUserKey = dbuser.ID;
                string _email = dbuser.Email;
                string _passwordQuestion = "";
                string _comment = "";
                bool _isApproved = true;
                bool _isLockedOut = false;
                DateTime _creationDate = DateTime.Now;
                DateTime _lastLoginDate = DateTime.Now;
                DateTime _lastActivityDate = DateTime.Now;
                DateTime _lastPasswordChangedDate = DateTime.Now;
                DateTime _lastLockedOutDate = DateTime.Now;

                MembershipUser user = new MembershipUser("CustomMembershipProvider",
                    _username,
                    _providerUserKey,
                    _email,
                    _passwordQuestion,
                    _comment,
                    _isApproved,
                    _isLockedOut,
                    _creationDate,
                    _lastLoginDate,
                    _lastActivityDate,
                    _lastPasswordChangedDate,
                    _lastLockedOutDate);
                return user;
            }
            else
            {
                return null;
            }
        }

    }
}