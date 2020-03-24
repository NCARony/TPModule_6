using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BO;
using TP1_Module_6.Data;
using TP1_Module_6.Models;

namespace TP1_Module_6.Controllers
{
    public class SamouraisController : Controller
    {
        private TpMd6Context db = new TpMd6Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            var vm = new SamouraiVM();
            vm.Armes = db.Armes.ToList();
            vm.ArtMartials = db.ArtMartials.ToList();
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiVM vm)
        {
            if (ModelState.IsValid)
            { 
                if (vm.IdSelectedArme.HasValue)
                {
                    vm.Samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == vm.IdSelectedArme.Value);
                }

                if (vm.IdSelectedArtMartial.HasValue)
                {
                    vm.Samourai.ArtMartials = db.ArtMartials.FirstOrDefault(am => am.Id == vm.IdSelectedArtMartial.Value);
                }

                db.Samourais.Add(vm.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
             }
            vm.Armes = db.Armes.ToList();
            return View(vm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamouraiVM vm = new SamouraiVM();
            vm.Samourai = db.Samourais.Find(id);
            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }
            vm.Armes = db.Armes.ToList();
            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiVM vm)
        {
            if (ModelState.IsValid)
            {
/*                var samourai = db.Samourais.Find(vm.Samourai.Id);
                samourai.Nom = vm.Samourai.Nom;
                samourai.Force = vm.Samourai.Force;*/

                db.Samourais.Attach(vm.Samourai);


                if (vm.IdSelectedArme != null)
                {
                    vm.Samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == vm.IdSelectedArme.Value);
                }
                else
                {
                    vm.Samourai.Arme = null;
                }
                db.Entry(vm.Samourai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
