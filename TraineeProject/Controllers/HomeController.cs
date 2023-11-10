using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Models;
using TraineeProject.Models.ViewModel;

namespace TraineeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly TraineeDbContext db;

        public HomeController(TraineeDbContext db)
        {
            this.db = db;
        }


        public IActionResult Index(int SelectBatch = 0)
        {
            BatchViewModel bvm = new BatchViewModel()
            {
                Batches = db.Batches.ToList(),
                SelectBatch = SelectBatch,
                Trainees = SelectBatch == 0 ? null : db.Batches.Include(x => x.Trainees).First(x => x.BatchId == SelectBatch).Trainees.ToList()
                
            };
            return View(bvm);
        }

       
    }
}
