

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var position = _context.Positions.ToList();
            return View(position);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Position position)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var editedData = _context.Positions.FirstOrDefault(x => x.Id == id);
            return View(editedData);
        }

        [HttpPost]
        public IActionResult Update(Position position)
        {
            _context.Positions.Update(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deletedData = _context.Positions.SingleOrDefault(x => x.Id == id);
            return View(deletedData);
        }

        [HttpPost]
        public IActionResult Delete(Position position)
        {
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

