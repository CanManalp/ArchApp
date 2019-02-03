using ArchApp.Context;
using ArchApp.Entities;
using ArchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult RepoNotification(string notification, string alert)
        {

            KitapViewModel vm = new KitapViewModel();
            vm = vm.KitapMainPagePrep();
            vm.Notification = notification;
            vm.Alert = alert;
    
            return View("~/Views/Home/index.cshtml",vm);
        }
    }
}