using GangManagementSystem.API;
using GangManagementSystem.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GangManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        HomeViewModel hvm = new HomeViewModel();
        private string user = string.Empty;
        public HomeController()
        {
            switch (Properties.Settings.Default.AppLevel)
            {
                case "L":
                    hvm.env = Properties.Settings.Default.svcGangManagementQA;
                    break;
                case "D":
                    hvm.env = Properties.Settings.Default.svcGangManagementDEV;
                    break;
                case "Q":
                    hvm.env = Properties.Settings.Default.svcGangManagementQA;
                    break;
                case "P":
                    hvm.env = Properties.Settings.Default.svcGangManagementPROD;
                    break;
            }
        }

        public ActionResult Index()
        {
            hvm.racfid = user;

            if (ProcedureApi.GetUserSession(user) != null)
            {
                hvm.Authorized = true;
            }

            return View(hvm);
        }

        public ActionResult NewGang()
        {
            user = User.Identity.Name.ToUpper();
            user = user.Substring(user.LastIndexOf("\\") + 1);

            hvm.racfid = user;

            if (ProcedureApi.GetUserSession(user) != null)
            {
                hvm.Authorized = true;
            }

            return View(hvm);
        }

        public ActionResult EditGang()
        {
            user = User.Identity.Name.ToUpper();
            user = user.Substring(user.LastIndexOf("\\") + 1);

            hvm.racfid = user;

            if (ProcedureApi.GetUserSession(user) != null)
            {
                hvm.Authorized = true;
            }

            return View(hvm);
        }
    }
}