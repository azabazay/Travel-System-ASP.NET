using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Project_MVC.Models.ViewModel.Country
{
    public class CountryViewModel
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryDescription { get; set; }
        public Guid ContinentId { get; set; }
        public string CountryCode { get; set;}
        public string ImageName { get; set; }
    }
}