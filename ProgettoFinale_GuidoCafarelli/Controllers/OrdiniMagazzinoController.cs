using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgettoFinale_GuidoCafarelli.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdiniMagazzinoController : Controller
    {
        Model1 model = new Model1();
        public ActionResult Index(string cerca)
        {
            return View(model.OrdiniMagazzino.OrderBy(x => x.DataEntrata).Where(x => x.Prodotti.Nome.Contains(cerca) || cerca == null).ToList());
        }
        
    }
}