﻿using ITO5032_Assignment.Enums;
using ITO5032_Assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ITO5032_Assignment.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginsList()
        {
            ExternalLoginListViewModel extView = new ExternalLoginListViewModel();
            extView.ReturnUrl = "";
            return View(extView);
        }
        public ActionResult Index()
        {
            var sorted = db.Bookings
                            .Where(b => b.start_datetime > DateTime.Now)
                            .OrderByDescending(b => b.start_datetime)
                            .ToList();
            List<Booking> bookings = new List<Booking>();
            if(sorted.Count > 2)
                bookings.Add(sorted[2]);
            if(sorted.Count > 1)
                bookings.Add(sorted[1]);
            if (sorted.Count > 0)
                bookings.Add(sorted[0]);

            LoginViewModel model = new LoginViewModel();
            foreach(Booking booking in bookings)
            {
                booking.Bookable = db.Bookables.Where(b => b.id == booking.Bookable_id).ToList()[0];
                booking.Bookable.Location = db.Locations.Where(l => l.id == booking.Bookable.Location_id).ToList()[0];
            }
            model.bookings = bookings;

            return View(model);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            try
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            } catch
            {
                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //result = UserManager.AddToRole(user.Id, "User");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    AppUser appUser = new AppUser();
                    appUser.role_id = 1;
                    appUser.email = model.Email;
                    appUser.password = model.Password;
                    appUser.first_name = model.first_name;
                    appUser.last_name = model.last_name;
                    String[] dob = model.date_of_birth.Split('/');
                    appUser.date_of_birth = new DateTime(Int32.Parse(dob[2]), Int32.Parse(dob[0]), Int32.Parse(dob[1]));
                    appUser.address1 = model.address1;
                    appUser.address2 = model.address2;
                    appUser.username = model.Email;
                    appUser.salt = "test";
                    appUser.external_id = user.Id;
                    AppUsersController appUserController = new AppUsersController();
                    ActionResult actionResult = appUserController.Create(appUser);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult RenderAdminButton()
        {
            var id = User.Identity.GetUserId();
            var user = db.AppUsers.Where(u => u.external_id == id).ToList();

            if (user.Count > 0 && user[0].role_id == Roles.ADMIN.Id)
            {
                ViewData["isAdmin"] = "ADMIN";
                return PartialView("_AdminButton");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}