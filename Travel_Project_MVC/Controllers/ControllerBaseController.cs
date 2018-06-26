using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Project_MVC.Models;
using System.Data.Entity;

namespace Travel_Project_MVC.Controllers 
{
    public class ControllerBaseController : Controller
    {
        public Travel_DatabaseEntities db = new Travel_DatabaseEntities();
        //
        // GET: /ControllerBase/
        public ActionResult Index()
        {
            return View();
        }
	}
}