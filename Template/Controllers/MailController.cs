using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.BL.Helper;
using Template.BL.Models;



namespace Template.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SendMail(MailVM mail)
        {
            TempData["Msg"] = SendMailHelper.MailSender(mail);
            return RedirectToAction("Index");
        }
    }
}
