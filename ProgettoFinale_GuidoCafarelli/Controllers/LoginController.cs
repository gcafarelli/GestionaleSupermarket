using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProgettoFinale_GuidoCafarelli.Controllers
{
    public class LoginController : Controller
    {
        Model1 model = new Model1();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "User, Password")] Users u)
        {
            Users UtenteDaLoggare = model.Users.Where(x => x.User == u.User).Where(x => x.Password == u.Password).FirstOrDefault();

            if (UtenteDaLoggare != null)
            {
                if (UtenteDaLoggare.Ruolo == "PuntoVendita")
                {
                    FormsAuthentication.SetAuthCookie(UtenteDaLoggare.User, false);
                    return RedirectToAction("Index", "Prodotti");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(UtenteDaLoggare.User, false);
                    return RedirectToAction("Index", "OrdiniMagazzino");
                }
            }
            else
            {
                ViewBag.ErroreLogin = "Autenticazione non corretta";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}