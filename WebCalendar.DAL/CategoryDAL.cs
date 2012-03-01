using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace WebCalendar.DAL
{
    public class CategoryDAL : DAO
    {

        /// <summary>
        /// Gets all Categories
        /// </summary>
        /// <returns>Collection of Categories</returns>
        public static IEnumerable<Category> GetAll()
        {
            var e = from c in db.Categories
                    select c;

            return e;
        }

        /// <summary>
        /// Gets all Categories by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>Collection of Categories</returns>
        public static IEnumerable<Category> GetAllByUserID(int userID)
        {
            var e = from c in db.Categories
                    where c.UserID == userID
                    select c;

            return e;
        }

        /// <summary>
        /// Gets Category by ID.
        /// </summary>
        /// <param name="categoryID">The category ID.</param>
        /// <returns>The Category with the same ID</returns>
        public static Category GetByID(int categoryID)
        {
            Category category = (from c in db.Categories where c.ID == categoryID select c).FirstOrDefault();

            return category;
        }

        /// <summary>
        /// Creates new Category
        /// </summary>
        /// <param name="user">Owner of the Category</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryDescription">The category description.</param>
        public static void Create(User user, string categoryName, string categoryDescription)
        {
            Category c = new Category();
            c.Name = categoryName;
            c.Description = categoryDescription;
            c.User = user;
            db.AddToCategories(c);
            db.SaveChanges();
        }

 
        /// <summary>
        /// Updates the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryDescription">The category description.</param>
        public static void Update(Category category, string categoryName, string categoryDescription)
        {
            category.Name = categoryName;
            category.Description = categoryDescription;
            db.SaveChanges();
        }

        /// <summary>
        /// Removes the Category by ID.
        /// </summary>
        /// <param name="categoryID">The category ID.</param>
        public static void RemoveByID(int categoryID)
        {
            object categoryForDeletion;

            EntityKey categoryKey = new EntityKey("WebCalendarEntities.Categories", "ID", categoryID);

            if (db.TryGetObjectByKey(categoryKey, out categoryForDeletion))
            {
                try
                {
                    db.DeleteObject(categoryForDeletion);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                                                                      "The Category with an ID of '{0}' could not be deleted.\n" +
                                                                      "Make sure that any related objects are already deleted.\n",
                        categoryKey.EntityKeyValues[0].Value), ex);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                                  "The Category with an ID of '{0}' could not be found.\n" +
                                                                  "Make sure that Category exists.\n",
                    categoryKey.EntityKeyValues[0].Value));
            }
        }


    }
}