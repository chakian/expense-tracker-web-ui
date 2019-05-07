using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business.Context;
using ExpenseTracker.WebUI.Models.Expense;

namespace ExpenseTracker.WebUI.Controllers
{
    public class ExpenseController : Controller
    {
        private ExpenseTrackerContext db = new ExpenseTrackerContext();

        // GET: Expense
        public ActionResult Index()
        {
            return View(db.Expenses.ToList());
        }

        // GET: Expense/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.ContextObjects.Expense addModel = db.Expenses.Find(id);
        //    if (addModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(addModel);
        //}

        // GET: Expense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Date,Amount,Description,AccountID")] Models.ContextObjects.Expense addModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Expenses.Add(addModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(addModel);
        //}

        // GET: Expense/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.ContextObjects.Expense addModel = db.Expenses.Find(id);
        //    if (addModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(addModel);
        //}

        // POST: Expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Amount,Description,AccountID")] AddModel addModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addModel);
        }

        // GET: Expense/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.ContextObjects.Expense addModel = db.Expenses.Find(id);
        //    if (addModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(addModel);
        //}

        // POST: Expense/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Models.ContextObjects.Expense addModel = db.Expenses.Find(id);
        //    db.Expenses.Remove(addModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
