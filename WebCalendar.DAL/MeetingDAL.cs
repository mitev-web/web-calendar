using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WebCalendar.DAL
{
    public class MeetingDAL : DAO
    {

        /// <summary>
        /// Gets all Meetings
        /// </summary>
        /// <returns>collection of all Meetings</returns>
        public static IEnumerable<Meeting> GetAll()
        {
            IEnumerable<Meeting> meetings = from meeting in db.Meetings
                                            select meeting;

            return meetings;
        }

        /// <summary>
        /// Gets all Meetings by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>Collection of Meetings</returns>
        public static IEnumerable<Meeting> GetAllByUserID(int userID)
        {
            IEnumerable<Meeting> meetings = from meeting in db.Meetings
                                            where meeting.UserID == userID
                                            select meeting;

            return meetings;
        }

        /// <summary>
        /// Gets the Meeting by ID.
        /// </summary>
        /// <param name="meetingID">The meeting ID.</param>
        /// <returns>Meeting with this ID</returns>
        public static Meeting GetByID(int meetingID)
        {
            Meeting meeting = (from m in db.Meetings where m.ID == meetingID select m).FirstOrDefault();

            return meeting;
        }

        /// <summary>
        /// Creates the specified Meeting
        /// </summary>
        /// <param name="user">The owner of this meeting</param>
        /// <param name="category">The category.</param>
        /// <param name="contacts">The contacts.</param>
        /// <param name="date">The date.</param>
        /// <param name="description">The description.</param>
        /// <param name="location">The location.</param>
        public static void Create(User user, Category category, IEnumerable<Contact> contacts, DateTime date, string description, string location)
        {
            Meeting m = new Meeting();
            m.User = user;
            m.Date = date;
            m.Description = description;
            m.Location = location;
            m.Category = category;

            foreach (Contact c in contacts)
            {
                m.Contacts.Add(c);
            }

            db.SaveChanges();
        }

        /// <summary>
        /// Updates the specified meeting.
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        /// <param name="category">The category.</param>
        /// <param name="date">The date.</param>
        /// <param name="description">The description.</param>
        /// <param name="location">The location.</param>
        public static void Update(Meeting meeting, Category category, DateTime date, string description, string location)
        {
            meeting.Category = category;
            meeting.Date = date;
            meeting.Description = description;
            meeting.Location = location;
            db.SaveChanges();
        }

        /// <summary>
        /// Counts all Meetings by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>The number of Meetings owned by the user</returns>
        public static int CountAllByUserID(int userID)
        {
            int count = (from meeting in db.Meetings where meeting.UserID == userID select meeting).Count();

            return count;
        }

        /// <summary>
        /// Adds additional contacts to the Meeting
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        /// <param name="contacts">The contacts.</param>
        public static void AddContacts(Meeting meeting, IEnumerable<Contact> contacts)
        {
            foreach (Contact c in contacts)
            {
                meeting.Contacts.Add(c);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Adds additional contact to the meeting
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        /// <param name="contact">The contact.</param>
        public static void AddContact(Meeting meeting, Contact contact)
        {
            meeting.Contacts.Add(contact);
            db.SaveChanges();
        }

        /// <summary>
        /// Removes contact from the meeting
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        /// <param name="contact">The contact.</param>
        public static void RemoveContact(Meeting meeting, Contact contact)
        {
            meeting.Contacts.Remove(contact);
            db.SaveChanges();
        }

        /// <summary>
        /// Removes all contacts from the Meeting
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        public static void ClearContacts(Meeting meeting)
        {
            meeting.Contacts.Clear();
            db.SaveChanges();
        }

        /// <summary>
        /// Changes the category of a Meeting
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        /// <param name="category">The category.</param>
        public static void ChangeCategory(Meeting meeting, Category category)
        {
            meeting.Category = category;

            db.SaveChanges();
        }

        /// <summary>
        /// Removes the Meeting by ID.
        /// </summary>
        /// <param name="meetingID">The meeting ID.</param>
        public static void RemoveByID(int meetingID)
        {
            object meetingForDeletion;

            EntityKey meetingKey = new EntityKey("WebCalendarEntities.Meetings", "ID", meetingID);

            if (db.TryGetObjectByKey(meetingKey, out meetingForDeletion))
            {
                try
                {
                    db.DeleteObject(meetingForDeletion);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                                                                      "The Meeting with an ID of '{0}' could not be deleted.\n" +
                                                                      "Make sure that any related objects are already deleted.\n",
                        meetingKey.EntityKeyValues[0].Value), ex);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                                  "The Meeting with an ID of '{0}' could not be found.\n" +
                                                                  "Make sure that Product exists.\n",
                    meetingKey.EntityKeyValues[0].Value));
            }
        }
    }
}