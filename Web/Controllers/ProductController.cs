using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Detail(int id)
        {
            var product = _context.Products.Include(x=>x.Category).SingleOrDefault(x => x.Id == id);
            var relatedProducts = _context.Products.Where(x => x.CategoryId == product.CategoryId && x.Id != product.Id).Take(3).ToList();
            ProductDetailVM vm = new ProductDetailVM()
            {
                Product = product,
                RelatedProducts = relatedProducts
            };
            return View(vm);
        }
    }
}

