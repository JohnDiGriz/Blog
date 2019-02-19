using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogTest.WEB.Models;

namespace BlogTest.WEB.Controllers
{
    public class HomeController : Controller
    {
        const int ON_PAGE = 10;
        private AutoMapper.IMapper Mapper_;
        private DAL.Interfaces.IUnitOfWork UnitOfWork_;
        public HomeController(DAL.Interfaces.IUnitOfWork unitOfWork, AutoMapper.IMapper mapper)
        {
            UnitOfWork_ = unitOfWork;
            Mapper_ = mapper;
        }
        public IActionResult Index(int id) //List of post, id is page number
        {
            var model = new Models.BlogListViewModel();
            model.PageCount = UnitOfWork_.BlogPostsRepository.GetPageCount(ON_PAGE);
            if (id > model.PageCount)
                id = model.PageCount;
            model.PageNum = id;
            var blogPosts = UnitOfWork_.BlogPostsRepository.GetPage(model.PageNum, ON_PAGE);//Get posts displaying on this page
            if (blogPosts != null)
            {
                Mapper_.Map(blogPosts, model.BlogPosts);
                int i = 0;
                foreach(var post in blogPosts)
                {
                    model.BlogPosts[i].CommentsCount = post.Comments.Count;
                    i++;
                } //Number of comments on each post
                model.IsAdmin= User.IsInRole("Admin");
                return View(model);
            }
            return View("Error");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
