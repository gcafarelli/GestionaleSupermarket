using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgettoFinale_GuidoCafarelli.Controllers
{
    [Authorize(Roles = "Admin,PuntoVendita")]
    public class OrdiniNegoziController : Controller
    {
        Model1 model = new Model1();
        public ActionResult Index(string cerca)
        {
            return View(model.OrdiniNegozi.OrderByDescending(x => x.Data).Where(x => x.Users.User.Contains(cerca) || x.Prodotti.Nome.Contains(cerca) || cerca == null).ToList());
        }
    }
}