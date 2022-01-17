using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ProgettoFinale_GuidoCafarelli.Controllers
{
    public class ProdottiController : Controller
    {
        Model1 model = new Model1();
        public ActionResult Index(string cerca)
        {
            return View(model.Prodotti.OrderBy(x => x.Stock).Where(x => x.Nome.Contains(cerca) || x.Categorie.NomeCategoria.Contains(cerca) || cerca == null).ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AggiungiNuovoProdotto()
        {
            List<Categorie> listCategorie = new List<Categorie>();
            listCategorie = model.Categorie.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Categorie c in listCategorie)
            {
                SelectListItem x = new SelectListItem { Value = c.idCategoria.ToString(), Text = c.NomeCategoria };
                list.Add(x);
            }

            ViewBag.ListaCategorie = list;
            return View();
        }
        [HttpPost]
        public ActionResult AggiungiNuovoProdotto(Prodotti p, HttpPostedFileBase img)
        {
            p.filename = img.FileName;
            model.Prodotti.Add(p);
            model.SaveChanges();
            if (img != null)
            {
                string pathdisk = Server.MapPath("~/imgProdotti");
                img.SaveAs(Path.Combine(pathdisk, img.FileName));
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ModificaProdotto(int id)
        {
            List<Categorie> listCategorie = new List<Categorie>();
            listCategorie = model.Categorie.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Categorie c in listCategorie)
            {
                SelectListItem x = new SelectListItem { Value = c.idCategoria.ToString(), Text = c.NomeCategoria };
                list.Add(x);
            }

            ViewBag.ListaCategorie = list;
            Prodotti p = new Prodotti();
            p = model.Prodotti.Where(x => x.idProdotto == id).FirstOrDefault();
            return View(p);
        }
        [HttpPost]
        public ActionResult ModificaProdotto(Prodotti p)
        {
            model.Entry(p).State = System.Data.Entity.EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult OrdinaMagazzino(int id)
        {
            OrdiniMagazzino order = new OrdiniMagazzino();
            Prodotti p = new Prodotti();
            p = model.Prodotti.Where(x => x.idProdotto == id).FirstOrDefault();
            string nomeProdotto = p.Nome;
            string foto = p.filename;
            ViewBag.Foto = foto;
            ViewBag.Nome = nomeProdotto;
            ViewBag.Id = id;
            return View(order);

        }
        [HttpPost]
        public ActionResult OrdinaMagazzino(OrdiniMagazzino order)
        {
            Prodotti p = new Prodotti();
            p = model.Prodotti.Where(x => x.idProdotto == order.idProdotto).FirstOrDefault();
            p.Stock += order.QuantitaEntrata;
            model.Entry(p).State = System.Data.Entity.EntityState.Modified;
            model.SaveChanges();
            model.OrdiniMagazzino.Add(order);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,PuntoVendita")]
        [HttpGet]
        public ActionResult OrdinaNegozio(int id)
        {
            OrdiniNegozi order = new OrdiniNegozi();
            order.idProdotto = id;
            Prodotti p = new Prodotti();
            List<Users> list = new List<Users>();
            list = model.Users.Where(x => x.Ruolo == "PuntoVendita").ToList();
            List<SelectListItem> listNegozi = new List<SelectListItem>();
            foreach (Users u in list)
            {
                SelectListItem x = new SelectListItem { Value = u.idUser.ToString(), Text = u.User };
                listNegozi.Add(x);
            }

            ViewBag.ListaNegozi = listNegozi;
            ViewBag.Id = id;
            p = model.Prodotti.Where(x => x.idProdotto == id).FirstOrDefault();
            string nomeProdotto = p.Nome;
            string foto = p.filename;
            ViewBag.Foto= foto;
            ViewBag.Nome = nomeProdotto;
            return View(order);

        }
        [HttpPost]
        public ActionResult OrdinaNegozio(OrdiniNegozi order)
        {
            Prodotti p = new Prodotti();
            p = model.Prodotti.Where(x => x.idProdotto == order.idProdotto).FirstOrDefault();
            if (order.Quantita > p.Stock)
            {
                List<Users> list = new List<Users>();
                list = model.Users.Where(x => x.Ruolo == "PuntoVendita").ToList();
                List<SelectListItem> listNegozi = new List<SelectListItem>();
                foreach (Users u in list)
                {
                    SelectListItem x = new SelectListItem { Value = u.idUser.ToString(), Text = u.User };
                    listNegozi.Add(x);
                }

                ViewBag.ListaNegozi = listNegozi;
                int id =p.idProdotto;
                ViewBag.Id = id;
                p = model.Prodotti.Where(x => x.idProdotto == id).FirstOrDefault();
                string nomeProdotto = p.Nome;
                string foto = p.filename;
                ViewBag.Foto = foto;
                ViewBag.Nome = nomeProdotto;
                ViewBag.Errore = "Quantità non diponibile a magazzino";
                return View();
            }
            else
            {
                p.Stock -= order.Quantita;
                model.Entry(p).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
                model.OrdiniNegozi.Add(order);
                model.SaveChanges();
                ViewBag.ErroreLogin = "Quantità non disponibile a magazzino";
                return RedirectToAction("Index");
            }
            

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Giacenze()
        {
            return View();
        }

        public JsonResult GiacenzeNum()
        {

            List<Prodotti> prodotti = new List<Prodotti>();

            prodotti = model.Prodotti.Where(x => x.Stock < 1000).ToList();

            List<Giacenzenum> giacenzenum = new List<Giacenzenum>();

            foreach (var i in prodotti)
            {
                Giacenzenum g = new Giacenzenum();
                g.NomeProdotto = i.Nome;
                g.Quantita = (int)i.Stock;
                giacenzenum.Add(g);
            }

            return Json(giacenzenum, JsonRequestBehavior.AllowGet);
        }

        public class Giacenzenum
        {
            public string NomeProdotto { get; set; }

            public int Quantita { get; set; }


        }

        [HttpPost]
        public JsonResult GiacenzaCat()
        {
            var giacenzaCategoria = model.Prodotti.GroupBy(a => a.Categorie.NomeCategoria).Select(a => new { Amount = a.Sum(b => b.Stock), Name = a.Key }).OrderByDescending(a => a.Amount).ToList();
            List<Giacenzenum> list = new List<Giacenzenum>();
            foreach (var x in giacenzaCategoria)
            {
                Giacenzenum b = new Giacenzenum();
                b.NomeProdotto = x.Name;
                b.Quantita = Convert.ToInt32(x.Amount);
                list.Add(b);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public class GiacenzeCategoria
        {
            public int idCategoria { get; set; }

            public int tot { get; set; }


        }
    }
}