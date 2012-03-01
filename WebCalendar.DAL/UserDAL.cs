using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace WebCalendar.DAL
{
    public class UserDAL : DAO
    {
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>Returns collection of all Users</returns>
        /// 
        public static IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The ID of the User</param>
        /// <returns>The User with this ID</returns>
        public static User GetByID(int id)
        {
            var user = db.Users.Where(x => x.ID == id).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Gets the user by name
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User with this name</returns>
        public static User GetByUserName(string userName)
        {
            var user = db.Users.Where(x => x.UserName == userName).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Creates new User
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>The newly created User</returns>
        public static User Create(string username, string password, string email, string firstName, string lastName)
        {
            User u = new User();
            u.UserName = username;
            u.Email = email;
            u.FirstName = firstName;
            u.LastName = lastName;
            u.PasswordSalt = CreateSalt();
            u.Password = CreatePasswordHash(password, u.PasswordSalt);

            db.AddToUsers(u);
            db.SaveChanges();

            return u;
        }

        /// <summary>
        /// Creates Password Salt
        /// </summary>
        /// <returns>Password Salt string</returns>
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Mixes password and salt and hashes them
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>Hashed Password string</returns>
        public static string CreatePasswordHash(string password, string salt)
        {
            string saltAndPwd = "mixing" + password + "with some" + salt;

            System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            byte[] combined = encoder.GetBytes(saltAndPwd);
            hash.ComputeHash(combined);
            string hashCode = Convert.ToBase64String(hash.Hash);

            return hashCode;
        }

        /// <summary>
        /// Removes the User by ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public static void RemoveByID(int userID)
        {
            object userForDeletion;

            EntityKey userKey = new EntityKey("WebCalendarEntities.Users", "ID", userID);

            if (db.TryGetObjectByKey(userKey, out userForDeletion))
            {
                try
                {
                    db.DeleteObject(userForDeletion);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                                                                      "The user with an ID of '{0}' could not be deleted.\n" +
                                                                      "Make sure that any related objects are already deleted.\n",
                        userKey.EntityKeyValues[0].Value), ex);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                                  "The user with an ID of '{0}' could not be found.\n" +
                                                                  "Make sure that it exists.\n",
                    userKey.EntityKeyValues[0].Value));
            }
        }
    }
}