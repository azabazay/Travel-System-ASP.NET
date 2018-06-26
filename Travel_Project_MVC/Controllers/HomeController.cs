using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Project_MVC.Models;
using Travel_Project_MVC.Models.ViewModel.Home;

namespace Travel_Project_MVC.Controllers
{
    public class HomeController : ControllerBaseController
    {
        //
        // GET: /Home/
        public ActionResult HomePage()
        {
            List<Continent> continents = db.Continents.ToList();

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.Continents = new List<ContinentViewModel>();

            foreach (Continent cont in continents)
            {
                viewModel.Continents.Add(new ContinentViewModel() 
                {
                    ContinentId = cont.ContinentId,
                    ContinentName = cont.ContinentName,
                    ContinentDescription = cont.ContinentDescription,
                    ContinentCode = cont.ContinentCode,
                    ImageName = cont.ContinentCode + ".jpg"
                });
            }

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }
	}
}