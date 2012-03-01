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
    public class ContactController : Controller
    {
        private WebCalendarEntities db = new WebCalendarEntities();

        //
        // GET: /Contact/

        public ViewResult Index(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            ViewBag.Count = db.Contacts.Count();
            ViewBag.Start = start;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.OrderBy = orderBy;
            ViewBag.Desc = desc;

            return View();
        }

        //
        // GET: /Contact/GridData/?start=0&itemsPerPage=20&orderBy=ID&desc=true

        public ActionResult GridData(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            Response.AppendHeader("X-Total-Row-Count", db.Contacts.Count().ToString());
            ObjectQuery<Contact> contacts = db.Contacts.Include("User");
            contacts = contacts.OrderBy("it." + orderBy + (desc ? " desc" : ""));

            return PartialView(contacts.Skip(start).Take(itemsPerPage));
        }

        //
        // GET: /Default5/RowData/5

        public ActionResult RowData(int id)
        {
            Contact contact = db.Contacts.Single(c => c.ID == id);
            return PartialView("GridData", new Contact[] { contact });
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName");
            return PartialView("Edit");
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.AddObject(contact);
                db.SaveChanges();
                return PartialView("GridData", new Contact[] { contact });
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", contact.UserID);
            return PartialView("Edit", contact);
        }

        //
        // GET: /Contact/Edit/5

        public ActionResult Edit(int id)
        {
            Contact contact = db.Contacts.Single(c => c.ID == id);
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", contact.UserID);
            return PartialView(contact);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Attach(contact);
                db.ObjectStateManager.ChangeObjectState(contact, EntityState.Modified);
                db.SaveChanges();
                return PartialView("GridData", new Contact[] { contact });
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", contact.UserID);
            return PartialView(contact);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost]
        public void Delete(int id)
        {
            Contact contact = db.Contacts.Single(c => c.ID == id);
            db.Contacts.DeleteObject(contact);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
