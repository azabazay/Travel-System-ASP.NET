using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel_Project_MVC.Models;
using Travel_Project_MVC.Models.Systems;
using Travel_Project_MVC.Models.ViewModel.Login;
using Travel_Project_MVC.Models.ViewModel.Shared;

namespace Travel_Project_MVC.Controllers
{
    public class UserController : ControllerBaseController
    {
        public ActionResult LoginPage()
        {
            return View();
        }

        public ActionResult UserListing()
        {
            List<User> users = db.Users.ToList();

            List<UserListingViewModel> viewModel = new List<UserListingViewModel>();

            foreach (User us in users)
            {
                viewModel.Add(new UserListingViewModel() 
                { 
                    UserId = us.UserId,
                    LoginId = us.LoginId,
                    FirstName = us.FirstName,
                    LastName = us.LastName,
                    Mobile = us.Mobile,
                    Email = us.Email

                });
            }

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult EditUser(Guid userId)
        {   
            UserListingViewModel viewModel = new UserListingViewModel();

            if (userId == Guid.Empty)
            {

            }
            else
            {
                User us = db.Users.ToList().Where(x => x.UserId == userId).FirstOrDefault();

                viewModel = new UserListingViewModel()
                {
                    UserId = us.UserId,
                    LoginId = us.LoginId,
                    FirstName = us.FirstName,
                    LastName = us.LastName,
                    Mobile = us.Mobile,
                    Email = us.Email,
                    Password = us.Password

                };
            }

            ViewData.Model = viewModel;

            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [HttpGet]
        public JsonResult UserCreateUpdate(UserListingViewModel viewModel)
        {
            bool isSuccess = false;
            AjaxReturnModel returnModel = new AjaxReturnModel();

            User user = new User();

            if(viewModel.UserId == Guid.Empty)
            {
                user = new User()
                {
                    UserId = Guid.NewGuid(),
                    LoginId = viewModel.LoginId,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Mobile = viewModel.Mobile,
                    Email = viewModel.Email,
                    Password = viewModel.Password
                };

                db.Users.Add(user);
                db.SaveChanges();
            }
            else
            {
                user = db.Users.ToList().Where(x => x.UserId == viewModel.UserId).FirstOrDefault();

                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.Mobile = viewModel.Mobile;
                user.Email = viewModel.Email;
                user.Password = viewModel.Password;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            returnModel.recordId = user.UserId;
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

            return Json(returnModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteUser(Guid userId)
        {
            bool isSuccess = false;
            AjaxReturnModel returnModel = new AjaxReturnModel();

            User user = new User();

            if (userId != Guid.Empty)
            {
                user = db.Users.ToList().Where(x => x.UserId == userId).FirstOrDefault();

                db.Entry(user).State = EntityState.Deleted;
                db.SaveChanges();
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

            return Json(returnModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Login(string UserName, string Password)
        {
            bool isLoginSuccess = false;
            AjaxReturnModel returnModel = new AjaxReturnModel();

            User foundUser = db.Users.ToList().Where(x => x.LoginId == UserName && x.Password == Password).FirstOrDefault();

            isLoginSuccess = !(foundUser == null);

            if (isLoginSuccess)
            {
                MySession.CurrentSession = new MySession() { UserId = foundUser.UserId, LoginDate = DateTime.Now};

                returnModel.ReturnStatus = Enum.ReturnStatus.success;
            }
            else
            {
                returnModel.ReturnStatus = Enum.ReturnStatus.error;
                returnModel.ErrorMessages.Add("Incorrect login credentials");
            }

            return Json(returnModel);
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UserId,LoginId,Password,FirstName,LastName,Mobile,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid();
                
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UserId,LoginId,Password,FirstName,LastName,Mobile,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
