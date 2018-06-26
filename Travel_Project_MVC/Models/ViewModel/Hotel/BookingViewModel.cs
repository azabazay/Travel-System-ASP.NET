using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Project_MVC.Models.ViewModel.Hotel
{
    public class BookingViewModel
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public DateTime BookDateFrom { get; set;}
        public DateTime BookDateTo { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public int Rooms { get; set; }
        public int PersonStay { get; set; }
    }
}