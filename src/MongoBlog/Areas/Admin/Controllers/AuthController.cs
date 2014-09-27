using MongoBlog.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MongoBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        public ActionResult LogOn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("Admin.Dashboard.Index");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOn(LogOnModel model)
        {
            if (model.Username == "admin" && model.Password == "admin")
            {
                FormsAuthentication.SetAuthCookie("Administration", false);
                return RedirectToRoute("Admin.Dashboard.Index");
            }

            ViewBag.Fail = true;

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Admin.Auth.LogOn");
        }
    }
}