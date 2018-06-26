using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Project_MVC.Models.ViewModel.Home
{
    public class HomeViewModel
    {
        public List<ContinentViewModel> Continents { get; set; }
    }

    public class ContinentViewModel
    {
        public Guid ContinentId { get; set; }
        public string ContinentName { get; set; }
        public string ContinentDescription { get; set; }
        public string ContinentCode { get; set; }
        public string ImageName { get; set; }
    }
}