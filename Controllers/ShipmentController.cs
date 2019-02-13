using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FetchtoApp.Controllers
{
    
    public class ShipmentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewShipment()
        {
            return View();
        }


    }
}