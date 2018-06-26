using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Project_MVC.Models.ViewModel.Hotel
{
    public class HotelViewModel
    {
        public System.Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelCode { get; set; }
        public string HotelDescription { get; set; }
        public System.Guid CountryId { get; set; }
        public int StarRating { get; set; }
        public string ImageName { get; set; }



        private decimal? _Price;
        public decimal Price
        {
            get
            {
                if (_Price == null)
                    _Price = 0;

                return _Price.GetValueOrDefault();
            }
            set
            {
                _Price = value == null ? 0 : value;
            }
        }


        private bool? _Wifi;
        public bool? Wifi
        {
            get
            {
                if (_Wifi == null)
                    _Wifi = false;

                return _Wifi.GetValueOrDefault();
            }
            set
            {
                _Wifi = value == null ? false : value;
            }
        }

        private bool? _Spa;
        public bool? Spa
        {
            get
            {
                if (_Spa == null)
                    _Spa = false;

                return _Spa.GetValueOrDefault();
            }
            set
            {
                _Spa = value == null ? false : value;
            }
        }

        private bool? _Pool;

        public bool? Pool
        {
            get
            {
                if (_Pool == null)
                    _Pool = false;

                return _Pool.GetValueOrDefault();
            }
            set
            {
                _Pool = value == null ? false : value;
            }
        }
    }
}