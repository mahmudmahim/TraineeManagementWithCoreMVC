using Microsoft.AspNetCore.Mvc;
using TraineeProject.Models;

namespace TraineeProject.Controllers
{
    public class BatchController : Controller
    {
        private readonly TraineeDbContext db;

        public BatchController(TraineeDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Batches.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Batch batch)
        {
            if (ModelState.IsValid)
            {
                db.Batches.Add(batch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            var batches = db.Batches.Find(id);
            if (batches == null)
            {
                return NotFound();
            }

            return View(batches);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Batch batch,int? id)
        {
            if (ModelState.IsValid)
            {
                db.Batches.Update(batch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(batch);
        }

        public IActionResult Delete(int id)
        {
            var batches = db.Batches.FirstOrDefault(x => x.BatchId == id);
            if(batches == null)
            {
                return NotFound();
            }
            return View(batches);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DoDelete(int id)
        {
            var batch = db.Batches.Find(id);
            if(batch != null)
            {
                db.Batches.Remove(batch);
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
