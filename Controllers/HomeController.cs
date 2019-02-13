using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FetchtoApp.Models;

namespace FetchtoApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        UserManageModel user;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "About Us.";

            return View();
        }
        public ActionResult Features()
        {
            ViewBag.Message = "Features.";

            return View();
        }
        public ActionResult Rate_Calculator()
        {
            ViewBag.Message = "Rate_Calculator.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }
        public ActionResult Reset()
        {
            ViewBag.Message = "Reset";

            return View();
        }

    }
}