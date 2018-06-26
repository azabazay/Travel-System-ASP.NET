using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Project_MVC.Models;
using Travel_Project_MVC.Models.ViewModel.Country;

namespace Travel_Project_MVC.Controllers
{
    public class CountryController : ControllerBaseController
    {
        //
        // GET: /Country/
        public ActionResult CountryListing(Guid continentId)
        {
            List<Country> countries = new List<Country>();
            List<CountryViewModel> viewModel = new List<CountryViewModel>();

            countries = db.Countries.ToList().Where(x => x.ContinentId == continentId).ToList();

            countries.ForEach(x => viewModel.Add(new CountryViewModel() { 
                CountryId = x.CountryId,
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                CountryDescription = x.CountryDescription,
                ImageName = x.CountryCode + ".jpg",
                ContinentId = x.ContinentId
            }));

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }
	}
}