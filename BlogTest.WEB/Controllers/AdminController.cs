using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
namespace BlogTest.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager_;
        private readonly DAL.Interfaces.IUnitOfWork UnitOfWork_;
        private readonly IMapper Mapper_;

        public AdminController(UserManager<IdentityUser> userManager, DAL.Interfaces.IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork_ = unitOfWork;
            Mapper_ = mapper;
            UserManager_ = userManager;
        }

        [HttpPost]
        public IActionResult Edit(Models.BlogPostCreationViewModel model) //Checking is edited post is valid and saving it to database 
        {
            if (ModelState.IsValid)
            {
                var entity = UnitOfWork_.BlogPostsRepository.Get((long)model.Id);
                if (entity == null)
                    return View("Error");
                Mapper_.Map(model, entity);
                UnitOfWork_.BlogPostsRepository.Update(entity);
                UnitOfWork_.Commit();
                return RedirectToAction("Show", "Post", new { id = model.Id }); //Opening edited post
            }
            return View("Create", model);
        }
        [HttpPost]
        public IActionResult Create(Models.BlogPostCreationViewModel model) //Checking is new post is valid and saving it to database
        {
            if (ModelState.IsValid)
            {
                var entity = new DAL.Models.BlogPost();
                model.PostingTime = DateTime.Now; //Adding posting time
                model.AuthorId = UserManager_.GetUserId(User); //Setting current user as author
                Mapper_.Map(model, entity);
                UnitOfWork_.BlogPostsRepository.Add(entity);
                UnitOfWork_.Commit();
                return RedirectToAction("Show", "Post", new { id = model.Id }); //Openint edited post
            }
            model.IsEdit = false; //View thinks that since model already exist we must be editing existing post, 
                        //so we making sure it stays in "create" mode
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(long id)
        {
            var model = new Models.BlogPostCreationViewModel();
            var post = UnitOfWork_.BlogPostsRepository.Get(id);
            if(post!=null)
            {
                Mapper_.Map(post, model);
                model.IsEdit = true; //We telling view that we editing existing post, not creating new
                return View("Create", model);
            }
            return View("Error");
        }
        public IActionResult Delete(long id, int page)
        {
            if (!UnitOfWork_.BlogPostsRepository.Remove(id))
                return View("Error"); //There's no such post in database
            UnitOfWork_.Commit();
            return RedirectToAction("Index", "Home", new { id = page }); //Return to homepage
        }
    }
}
