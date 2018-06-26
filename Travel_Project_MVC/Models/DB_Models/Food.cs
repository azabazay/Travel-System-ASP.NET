using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Project_MVC.Models.DB_Models
{
    public class Food
    {
        public Guid FoodId { get; set; }
        public string FoodName { get; set; }
        public int FoodType { get; set; }
        public Guid HotelId { get; set; }
    }
}