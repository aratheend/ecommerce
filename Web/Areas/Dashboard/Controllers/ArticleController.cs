using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Helpers;
using Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _env;


        public ArticleController(AppDbContext context, IHttpContextAccessor httpContext, IWebHostEnvironment env)
        {
            _context = context;
            _httpContext = httpContext;
            _env = env;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var articles = _context.Articles.Include(x => x.ArticleTags).ThenInclude(x => x.Tag).Include(y => y.User).ToList();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var tags = _context.Tags.ToList();
            ViewBag.Tags = new SelectList(tags, "Id", "TagName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article, List<int> tagIds, IFormFile Photo)
        {
            

            var tags = _context.Tags.ToList();
            ViewBag.Tags = new SelectList(tags, "Id", "TagName");

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {


                var path = "/uploads/" + DateTime.Now.ToString("MM-dd-yyyy") + "_" + Guid.NewGuid() + Path.GetExtension(Photo.FileName);

                using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

                var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                article.UserId = userId;
                article.PhotoUrl = path;
                article.CreatedDate = DateTime.Now;
                article.UpdatedDate = DateTime.Now;
                article.SeoUrl = SeoHelper.SeoUrlCreator(article.Title);


                await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();


                List<ArticleTag> lisTags = new();

                for (int i = 0; i < tagIds.Count; i++)
                {
                    ArticleTag articleTag = new()
                    {
                        ArticleId = article.Id, // 32, 32,32,
                        TagId = tagIds[i]       // 1, 2, 3
                    };
                    lisTags.Add(articleTag);
                }


                await _context.ArticleTags.AddRangeAsync(lisTags);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

        }
    }
}

