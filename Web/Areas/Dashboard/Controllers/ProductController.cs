using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Dashboard.ViewModels;
using Web.Data;
using Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var products = _context.Products.Include(x=>x.Category).ToList();
            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories,"Id","CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile Photo)
        {
            var path = "/uploads/" + DateTime.Now.ToString("MM-dd-yyyy") + "_" + Guid.NewGuid() + Path.GetExtension(Photo.FileName);

            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                Photo.CopyTo(fileStream);
            }


            var myUrl = product.Name
                .ToLower()
                .Replace(" ", "-");
            product.SeoUrl = myUrl;
            product.PhotoUrl = path;
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.FirstOrDefault(x=>x.Id == id);
            var categories = _context.Categories.ToList();

            ProductEditViewModel vm = new()
            {
                Product = product,
                Categories = categories
            };
            return View(vm);
        }


        [HttpPost]
        public IActionResult Update(Product product, int id, int CategoryId, IFormFile updatedPhoto)
        {
            

            var myUrl = product.Name
                .ToLower()
                .Replace(" ", "-");
            var updatedProduct = _context.Products.FirstOrDefault(x=>x.Id == id);
            updatedProduct.Name = product.Name;
            updatedProduct.Price = product.Price;
            updatedProduct.Discount = product.Discount;
            updatedProduct.Description = product.Description;
            updatedProduct.Quantity = product.Quantity;
            updatedProduct.SeoUrl = myUrl;
            updatedProduct.CategoryId = CategoryId;

            if (updatedPhoto != null)
            {
                var path = "/uploads/" + DateTime.Now.ToString("MM-dd-yyyy") + "_" + Guid.NewGuid() + Path.GetExtension(updatedPhoto.FileName);

                using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    updatedPhoto.CopyTo(fileStream);
                }
                updatedProduct.PhotoUrl = path;
            }
            else
            {
                updatedProduct.PhotoUrl = product.PhotoUrl;
            }

            _context.Products.Update(updatedProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.SingleOrDefault(x=>x.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

