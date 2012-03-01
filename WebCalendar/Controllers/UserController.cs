using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalendar.DAL;

namespace WebCalendar.Controllers
{
    public class UserController : Controller
    {
        private WebCalendarEntities db = new WebCalendarEntities();

        //
        // GET: /User/

        public ViewResult Index(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            ViewBag.Count = db.Users.Count();
            ViewBag.Start = start;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.OrderBy = orderBy;
            ViewBag.Desc = desc;

            return View();
        }

        //
        // GET: /User/GridData/?start=0&itemsPerPage=20&orderBy=ID&desc=true

        public ActionResult GridData(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            Response.AppendHeader("X-Total-Row-Count", db.Users.Count().ToString());
            ObjectQuery<User> users = db.Users;
            users = users.OrderBy("it." + orderBy + (desc ? " desc" : ""));

            return PartialView(users.Skip(start).Take(itemsPerPage));
        }

        //
        // GET: /Default5/RowData/5

        public ActionResult RowData(int id)
        {
            User user = db.Users.Single(u => u.ID == id);
            return PartialView("GridData", new User[] { user });
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return PartialView("Edit");
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.AddObject(user);
                db.SaveChanges();
                return PartialView("GridData", new User[] { user });
            }

            return PartialView("Edit", user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            User user = db.Users.Single(u => u.ID == id);
            return PartialView(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(user);
                db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                db.SaveChanges();
                return PartialView("GridData", new User[] { user });
            }

            return PartialView(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public void Delete(int id)
        {
            User user = db.Users.Single(u => u.ID == id);
            db.Users.DeleteObject(user);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
