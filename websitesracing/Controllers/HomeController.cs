using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace websitesracing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public class Customer
        {
            public string CustName { get; set; }
            public string CustEmail { get; set; }
        }

        [HttpPost]
        public JsonResult CreateCustomer(Customer model)
        {
            if (ModelState.IsValid)
            {
                //Data save to database  
                return Json(new
                {
                    success = true
                });
            }
            return Json(new
            {
                success = false,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray()
            });
        }
    }
}