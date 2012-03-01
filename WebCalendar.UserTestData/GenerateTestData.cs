using System;
using System.Collections.Generic;
using System.Linq;
using WebCalendar.DAL;

namespace WebCalendar.UserTestData
{
    class GenerateTestData
    {
        /// <summary>
        /// Generates Test User data for a number of Users
        /// by default for every user are added:
        /// 30 Contacts, 5 Categories and 15 Meetings
        /// </summary>
        /// <param name="numberOfUsers">The number of users for which test data will be generated</param>
        public static void GenerateUserData(int numberOfUsers)
        {
            for (int i = 0; i < numberOfUsers; i++)
            {
                string username = Faker.NameFaker.FirstName() + Faker.StringFaker.Alpha(3) + Faker.NumberFaker.Number(20);

                User newUser = UserDAL.Create(username, username, Faker.StringFaker.Alpha(3) + Faker.InternetFaker.Email(), Faker.NameFaker.FirstName(), Faker.NameFaker.LastName());

                GenerateTestData.ContactsForUser(newUser, 30);
                GenerateTestData.CategoriesForUser(newUser, 5);
                GenerateTestData.MeetingsForUser(newUser, 15);
            }
        }

        public static void ContactsForUser(User user, int numberContacts)
        {
            for (int i = 0; i < numberContacts; i++)
            {
                string firstName = Faker.NameFaker.FirstName();
                string lastName = Faker.NameFaker.LastName();
                string email = Faker.StringFaker.Alpha(3) + Faker.InternetFaker.Email();
                string note = Faker.CompanyFaker.BS();
                string phone = Faker.PhoneFaker.Phone();
                string address = Faker.LocationFaker.City() + ", " + Faker.LocationFaker.Street();

                ContactDAL.Create(user, firstName, lastName, email, note, phone, address);
            }
        }

        public static void CategoriesForUser(User user, int numberCategories)
        {
            for (int i = 0; i < numberCategories; i++)
            {
                Random rand = new Random();
                string[] categories = { "Business", "Personal", "Co-workers", "Official Lunch", "Training" };
                string categoryName = categories[i % 5];
                string categoryDescription = Faker.CompanyFaker.BS();
                WebCalendar.DAL.CategoryDAL.Create(user, categoryName, categoryDescription);
            }
        }

        public static void MeetingsForUser(User user, int numberMeetings)
        {
            IEnumerable<Contact> contacts = ContactDAL.GetAllByUser(user);

            if (user.Contacts.Count > 3)
            {
                contacts = user.Contacts.Where(x => x.ID % 3 == 0);
            }
            else if (user.Contacts.Count > 0)
            {
                contacts.First();
            }
            else
            {
                //Console.WriteLine("The selected User don't have enough 
            }

            for (int i = 0; i < numberMeetings; i++)
            {
                DateTime date = Faker.DateTimeFaker.DateTime();
                string location = Faker.LocationFaker.City() + ", " + Faker.LocationFaker.Street();
                string description = Faker.CompanyFaker.BS();
                Category category = user.Categories.ToArray()[Faker.NumberFaker.Number(0, user.Categories.ToArray().Count() - 1)];

                MeetingDAL.Create(user, category, contacts, date, description, location);
            }
        }
    }
}