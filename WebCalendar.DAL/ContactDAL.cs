using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WebCalendar.DAL
{
    public class ContactDAL : DAO
    {
        /// <summary>
        /// Gets all Contacts.
        /// </summary>
        /// <returns>Collection of all Contacts</returns>
        public static IEnumerable<Contact> GetAll()
        {
            return db.Contacts;
        }

        /// <summary>
        /// Gets all Contacts by user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Collection of Users</returns>
        public static IEnumerable<Contact> GetAllByUser(User user)
        {
            var e = db.Contacts.Where(x => x.User == user);
            return e;
        }

        /// <summary>
        /// Gets all by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>Collection of Contacts</returns>
        public static IEnumerable<Contact> GetAllByUserID(int userID)
        {
            IEnumerable<Contact> contacts = from contact in db.Contacts
                                            where contact.UserID == userID
                                            select contact;
            return contacts;
        }

        /// <summary>
        /// Gets the Contact by ID.
        /// </summary>
        /// <param name="contactID">The contact ID.</param>
        /// <returns>The Contact with the same ID</returns>
        public static Contact GetByID(int contactID)
        {
            Contact contact = (from c in db.Contacts where c.ID == contactID select c).First();

            return contact;
        }

        /// <summary>
        /// Gets all Contacts by meeting ID.
        /// </summary>
        /// <param name="meetingID">The meeting ID.</param>
        /// <returns>Collection of Contacts</returns>
        public static IEnumerable<Contact> GetAllByMeetingID(int meetingID)
        {
            IEnumerable<Contact> contacts = (from m in db.Meetings where m.ID == meetingID select m.Contacts).First();

            return contacts;
        }

        /// <summary>
        /// Counts all Contacts by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>The number of Contacts</returns>
        public static int CountAllByUserID(int userID)
        {
            int count = (from con in db.Contacts where con.UserID == userID select con).Count();

            return count;
        }

        /// <summary>
        /// Updates the specified Contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="note">The note.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="address">The address.</param>
        public static void Update(Contact contact, string firstName, string lastName, string email, string note, string phone, string address)
        {
            contact.FirstName = firstName;
            contact.LastName = lastName;
            contact.Email = email;
            contact.Note = note;
            contact.Phone = phone;
            contact.Address = address;
            db.SaveChanges();
        }

        /// <summary>
        /// Creates the specified Contact.
        /// </summary>
        /// <param name="user">The owner of the Contacts</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="note">The note.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="address">The address.</param>
        public static void Create(User user, string firstName, string lastName, string email, string note, string phone, string address)
        {
            Contact c = new Contact()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Note = note,
                Phone = phone,
                Address = address,
                User = user
            };

            db.Contacts.AddObject(c);
            db.SaveChanges();
        }

        /// <summary>
        /// Removes the Contact by ID.
        /// </summary>
        /// <param name="contactID">The contact ID.</param>
        public static void RemoveByID(int contactID)
        {
            object contactForDeletion;

            EntityKey contactKey = new EntityKey("WebCalendarEntities.Contacts", "ID", contactID);

            if (db.TryGetObjectByKey(contactKey, out contactForDeletion))
            {
                try
                {
                    db.DeleteObject(contactForDeletion);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                                                                      "The Contact with an ID of '{0}' could not be deleted.\n" +
                                                                      "Make sure that any related objects are already deleted.\n",
                        contactKey.EntityKeyValues[0].Value), ex);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                                  "The Contact with an ID of '{0}' could not be found.\n" +
                                                                  "Make sure that Contact exists.\n",
                    contactKey.EntityKeyValues[0].Value));
            }
        }
    }
}