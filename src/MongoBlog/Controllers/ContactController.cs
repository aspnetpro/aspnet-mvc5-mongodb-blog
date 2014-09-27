using MongoBlog.Models.FormModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactFormModel model)
        {
            var mailer = new MailerController();
            mailer.Contact(model).Deliver();

            return RedirectToAction("Index");
        }
    }
}