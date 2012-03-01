using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalendar;
using WebCalendar.Models;
using WebCalendar.DAL;

namespace WebCalendar.Controllers
{
    public class HomeController : Controller
    {
        WebCalendarEntities db = new WebCalendarEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";
            
            return View(db.Users.ToList());
        }

        public string About()
        {
          //  return User.Identity.Name;

            if (User.Identity.IsAuthenticated)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }


        public ActionResult Wellcome()
        {

            return View();

        }
        public ActionResult Register()
        {

            return View();

        }
    }
}
