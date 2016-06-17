using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class RoleModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoleModels
        public ActionResult Index()
        {
            return View(db.IdentityRoles.ToList());
        }

        // GET: RoleModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleModels roleModels = db.IdentityRoles.Find(id);
            if (roleModels == null)
            {
                return HttpNotFound();
            }
            return View(roleModels);
        }

        // GET: RoleModels/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] RoleModels roleModels)
        {
            if (ModelState.IsValid)
            {
                db.IdentityRoles.Add(roleModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roleModels);
        }

        // GET: RoleModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleModels roleModels = db.IdentityRoles.Find(id);
            if (roleModels == null)
            {
                return HttpNotFound();
            }
            return View(roleModels);
        }

        // POST: RoleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] RoleModels roleModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roleModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roleModels);
        }

        // GET: RoleModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleModels roleModels = db.IdentityRoles.Find(id);
            if (roleModels == null)
            {
                return HttpNotFound();
            }
            return View(roleModels);
        }

        // POST: RoleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RoleModels roleModels = db.IdentityRoles.Find(id);
            db.IdentityRoles.Remove(roleModels);
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
