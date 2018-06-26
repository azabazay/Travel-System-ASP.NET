using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Project_MVC.Models;
using Travel_Project_MVC.Models.Systems;
using Travel_Project_MVC.Models.ViewModel.Hotel;
using Travel_Project_MVC.Models.ViewModel.Shared;

namespace Travel_Project_MVC.Controllers
{
    public class HotelController : ControllerBaseController
    {
        //
        // GET: /Hotel/
        public ActionResult HotelListing(Guid countryId)
        {
            List<Hotel> hotels = new List<Hotel>();
            List<HotelViewModel> viewModel = new List<HotelViewModel>();

            hotels = db.Hotels.ToList().Where(x => x.CountryId == countryId).ToList();

            hotels.ForEach(x => viewModel.Add(new HotelViewModel()
            {
                HotelCode = x.HotelCode,
                HotelId = x.HotelId,
                HotelName = x.HotelName,
                HotelDescription = x.HotelDescription,
                CountryId = x.CountryId,
                StarRating = x.StarRating,
                ImageName = x.HotelCode + ".jpg",
                Price = x.Price.GetValueOrDefault(),
                Pool = x.Pool.GetValueOrDefault(),
                Spa = x.Spa.GetValueOrDefault(),
                Wifi = x.Wifi.GetValueOrDefault()
            }));

            ViewData["CountryId"] = countryId;
            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult CreateHotel(Guid countryId)
        {
            HotelViewModel viewModel = new HotelViewModel();
            viewModel.HotelId = Guid.Empty;
            viewModel.CountryId = countryId;

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult EditHoteById(Guid hotelId)
        {
            HotelViewModel viewModel = new HotelViewModel();
            Hotel foundHotel = db.Hotels.ToList().Where(x => x.HotelId == hotelId).FirstOrDefault();

            if(foundHotel != null)
            {
                viewModel = new HotelViewModel()
                {
                    HotelId = foundHotel.HotelId,
                    HotelName = foundHotel.HotelName,
                    HotelDescription = foundHotel.HotelDescription,
                    CountryId = foundHotel.CountryId,
                    StarRating = foundHotel.StarRating,
                    HotelCode = foundHotel.HotelCode,
                    Price = foundHotel.Price.GetValueOrDefault(),
                    Pool = foundHotel.Pool,
                    Spa = foundHotel.Spa,
                    Wifi = foundHotel.Wifi
                };
            }

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView("CreateHotel");
            else
                return View("CreateHotel");
        }

        public ActionResult UpdateHotelLogo(Guid hotelId)
        {
            HotelViewModel viewModel = new HotelViewModel();
            viewModel.HotelId = hotelId;

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult HotelBooking(Guid hotelId)
        {
            BookingViewModel viewModel = new BookingViewModel() { BookDateFrom = DateTime.Now, BookDateTo = DateTime.Now };
            Guid userId = MySession.CurrentSession.UserId;
            viewModel.HotelId = hotelId;
            viewModel.UserId = userId;

            Booking myBooking = db.Bookings.ToList().Where(x => x.UserId == userId && x.HotelId == hotelId).FirstOrDefault();

            if (myBooking != null)
            {
                viewModel.BookingId = myBooking.BookingId;
                viewModel.Rooms = myBooking.Rooms;
                viewModel.PersonStay = myBooking.PersonsStay;
                viewModel.BookDateFrom = myBooking.BookDateFrom;
                viewModel.BookDateTo = myBooking.BookDateTo;
            }

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult EditBookingById(Guid BookingId)
        {
            BookingViewModel viewModel = new BookingViewModel() { BookDateFrom = DateTime.Now, BookDateTo = DateTime.Now };
            Guid userId = MySession.CurrentSession.UserId;
            viewModel.UserId = userId;

            Booking myBooking = db.Bookings.ToList().Where(x => x.BookingId == BookingId).FirstOrDefault();

            if (myBooking != null)
            {
                viewModel.BookingId = myBooking.BookingId;
                viewModel.Rooms = myBooking.Rooms;
                viewModel.PersonStay = myBooking.PersonsStay;
                viewModel.BookDateFrom = myBooking.BookDateFrom;
                viewModel.BookDateTo = myBooking.BookDateTo;
                viewModel.HotelId = myBooking.HotelId;
            }
            
            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView("HotelBooking");
            else
                return View("HotelBooking");
        }

        public ActionResult ViewMyBookings()
        {
            List<BookingViewModel> viewModel = new List<BookingViewModel>();
            Guid userId = MySession.CurrentSession.UserId;

            List<Booking> myBookings = db.Bookings.ToList().Where(x => x.UserId == userId).ToList();

            myBookings.ForEach(x => viewModel.Add(new BookingViewModel()
            {
                BookingId = x.BookingId,
                BookDateFrom = x.BookDateFrom,
                BookDateTo = x.BookDateTo,
                HotelId = x.HotelId,
                UserId = x.UserId,
                Rooms = x.Rooms,
                PersonStay = x.PersonsStay,
                HotelName = db.Hotels.ToList().Where(y => y.HotelId == x.HotelId).FirstOrDefault().HotelName
            }));

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        public JsonResult BookingCreateUpdate(BookingViewModel viewModel)
        {
            bool isSuccess = false;
            AjaxReturnModel returnModel = new AjaxReturnModel();

            Booking booking = new Booking();

            if (viewModel.BookingId == Guid.Empty)
            {
                booking = new Booking()
                {
                    BookingId = Guid.NewGuid(),
                    UserId = viewModel.UserId,
                    BookDateFrom = viewModel.BookDateFrom,
                    BookDateTo = viewModel.BookDateTo,
                    HotelId = viewModel.HotelId,
                    Rooms = viewModel.Rooms,
                    PersonsStay = viewModel.PersonStay
                };

                db.Bookings.Add(booking);
                db.SaveChanges();
            }
            else
            {
                booking = db.Bookings.ToList().Where(x => x.BookingId == viewModel.BookingId).FirstOrDefault();

                booking.BookDateFrom = viewModel.BookDateFrom;
                booking.BookDateTo = viewModel.BookDateTo;
                booking.Rooms = viewModel.Rooms;
                booking.PersonsStay = viewModel.PersonStay;

                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
            }

            returnModel.recordId = booking.BookingId;
            isSuccess = true;

            if (isSuccess)
            {
                returnModel.ReturnStatus = Enum.ReturnStatus.success;
            }
            else
            {
                returnModel.ReturnStatus = Enum.ReturnStatus.error;
                returnModel.ErrorMessages.Add("Failed for some reason");
            }

            return Json(returnModel);
        }

        [HttpPost]
        public JsonResult HotelCreateUpdate(HotelViewModel viewModel)
        {
            bool isSuccess = false;
            AjaxReturnModel returnModel = new AjaxReturnModel();

            Hotel hotel = new Hotel();

            if (viewModel.HotelId == Guid.Empty)
            {
                hotel = new Hotel()
                {
                    HotelId = Guid.NewGuid(),
                    HotelName = viewModel.HotelName,
                    HotelDescription = viewModel.HotelDescription,
                    CountryId = viewModel.CountryId,
                    StarRating = viewModel.StarRating,
                    IsDeleted = false,
                    HotelCode = viewModel.HotelCode,
                    Price = viewModel.Price,
                    Pool = viewModel.Pool.GetValueOrDefault(),
                    Spa = viewModel.Spa.GetValueOrDefault(),
                    Wifi = viewModel.Wifi.GetValueOrDefault()
                };

                db.Hotels.Add(hotel);
                db.SaveChanges();
            }
            else
            {
                hotel = db.Hotels.ToList().Where(x => x.HotelId == viewModel.HotelId).FirstOrDefault();

                hotel.HotelName = viewModel.HotelName;
                hotel.HotelDescription = viewModel.HotelDescription;
                hotel.CountryId = viewModel.CountryId;
                hotel.StarRating = viewModel.StarRating;
                hotel.HotelCode = viewModel.HotelCode;
                hotel.StarRating = viewModel.StarRating;
                hotel.Price = viewModel.Price;
                hotel.Pool = viewModel.Pool.GetValueOrDefault();
                hotel.Spa = viewModel.Spa.GetValueOrDefault();
                hotel.Wifi = viewModel.Wifi.GetValueOrDefault();

                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
            }

            returnModel.recordId = hotel.HotelId;
            isSuccess = true;

            if (isSuccess)
            {
                returnModel.ReturnStatus = Enum.ReturnStatus.success;
            }
            else
            {
                returnModel.ReturnStatus = Enum.ReturnStatus.error;
                returnModel.ErrorMessages.Add("Failed for some reason");
            }

            return Json(returnModel);
        }

        [HttpPost]
        public JsonResult CancelBooking(Guid bookingId)
        {
            AjaxReturnModel returnModel = new AjaxReturnModel();

            Booking booking = db.Bookings.ToList().Where(x => x.BookingId == bookingId).FirstOrDefault();

            bool isSuccess = true;

            if(booking != null)
            {
                db.Entry(booking).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
            {
                isSuccess = false;
                returnModel.ErrorMessages.Add("Booking not found.");
            }

            if (isSuccess)
            {
                returnModel.ReturnStatus = Enum.ReturnStatus.success;
            }
            else
            {
                returnModel.ReturnStatus = Enum.ReturnStatus.error;
                returnModel.ErrorMessages.Add("Failed for some reason");
            }

            return Json(returnModel);
        }
	}
}