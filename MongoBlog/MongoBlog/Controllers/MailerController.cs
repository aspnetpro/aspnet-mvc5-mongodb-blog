using ActionMailer.Net.Mvc;
using MongoBlog.Models.FormModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Controllers
{
    public class MailerController : MailerBase
    {
        public EmailResult Contact(ContactFormModel model)
        {
            From = String.Format("{0} <{1}>", model.Name, model.Email);
            To.Add("mbanagouro@outlook.com");
            Subject = "Contato pelo blog";

            return Email("Contact", model);
        }
    }
}