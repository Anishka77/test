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
    public class QuestionModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionModels
        public ActionResult Index()
        {
            return View(db.QuestionModels.ToList());
        }

        // GET: QuestionModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModel questionModel = db.QuestionModels.Find(id);
            if (questionModel == null)
            {
                return HttpNotFound();
            }
            return View(questionModel);
        }

        // GET: QuestionModels/Create
        [Authorize(Roles ="Guide")]
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }

        // POST: QuestionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Guide")]
        public ActionResult Create([Bind(Include = "Id,Description,RightAnswer,Answer1,Answer2,Answer3,UserName,CategoryId")] QuestionModel questionModel)
        {
            if (ModelState.IsValid)
            {
                questionModel.UserName = HttpContext.User.Identity.Name;
                db.QuestionModels.Add(questionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateCategoryDropDownList(questionModel.CategoryId);

            return View(questionModel);
        }

        // GET: QuestionModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModel questionModel = db.QuestionModels.Find(id);
            if (questionModel == null)
            {
                return HttpNotFound();
            }
            return View(questionModel);
        }

        // POST: QuestionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,RightAnswer,Answer1,Answer2,Answer3,UserName")] QuestionModel questionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionModel);
        }

        // GET: QuestionModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModel questionModel = db.QuestionModels.Find(id);
            if (questionModel == null)
            {
                return HttpNotFound();
            }
            return View(questionModel);
        }

        // POST: QuestionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuestionModel questionModel = db.QuestionModels.Find(id);
            db.QuestionModels.Remove(questionModel);
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

        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var departmentsQuery = from d in db.CategoryModels
                                   orderby d.Name
                                   select d;
            ViewBag.CategoryID = new SelectList(departmentsQuery, "Id", "Name", selectedCategory);
        }

    }
}
