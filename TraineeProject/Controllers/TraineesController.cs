using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Models;

namespace TraineeProject.Controllers
{
    public class TraineesController : Controller
    {
        private readonly TraineeDbContext db;

        public TraineesController(TraineeDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Trainees.Include(x=>x.Batch).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.batches = db.Batches.ToList();  
            return View();
        }

        [HttpPost]

        public IActionResult Create(Trainee trainee)
        {
            if(ModelState.IsValid)
            {
                db.Trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            var trainee = db.Trainees.Find(id);
            if(trainee == null)
            {
                return NotFound();  
            }
            ViewBag.batches = db.Batches.ToList();
            return View(trainee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Trainee trainee,int id)
        {
            if(id != trainee.TraineeId)
            {
                return NotFound();
            }
            ViewBag.batches = db.Batches.ToList();
            if (ModelState.IsValid)
            {
                db.Trainees.Update(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainee);
        }

        public IActionResult Delete(int? id)
        {
            var trainee = db.Trainees.FirstOrDefault(x=>x.TraineeId == id);
            if(trainee == null)
            {
                return NotFound();
            }
            ViewBag.batches = db.Batches.ToList();
            return View(trainee);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DoDelete(int id)
        {
            var trainees = db.Trainees.Find(id);
            if(trainees != null)
            {
                db.Trainees.Remove(trainees);
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
