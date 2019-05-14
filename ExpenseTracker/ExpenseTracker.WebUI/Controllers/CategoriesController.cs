using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business.Context;
using ExpenseTracker.Business.Context.DbModels;
using Microsoft.AspNet.Identity;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ExpenseTrackerContext db = new ExpenseTrackerContext();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.Budget).Include(c => c.InsertUser).Include(c => c.UpdateUser);
            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,BudgetId")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.InsertUserId = User.Identity.GetUserId();
                category.InsertTime = DateTime.Now;
                category.UpdateUserId = User.Identity.GetUserId();
                category.UpdateTime = DateTime.Now;
                category.IsActive = true;

                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Name", category.BudgetId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Name", category.BudgetId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name,BudgetId,InsertUserId,InsertTime,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.UpdateUserId = User.Identity.GetUserId();
                category.UpdateTime = DateTime.Now;

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Name", category.BudgetId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
