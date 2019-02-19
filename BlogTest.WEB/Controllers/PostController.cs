using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace BlogTest.WEB.Controllers
{
    public class PostController : Controller
    {
        private const int ON_PAGE = 10;

        private readonly DAL.Interfaces.IUnitOfWork UnitOfWork_;
        private readonly IMapper Mapper_;
        private readonly UserManager<IdentityUser> UserManager_;

        public PostController(UserManager<IdentityUser> userManager, DAL.Interfaces.IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork_ = unitOfWork;
            UserManager_ = userManager;
            Mapper_ = mapper;
        }
        public IActionResult Show(long id, int? page) //Show post, id is post id, and page is number of showing comments page
        {
            if (page == null)
                page = 1;
            var post = UnitOfWork_.BlogPostsRepository.Get(id);
            if(post!=null)
            {
                var model = new Models.BlogPostViewModel()
                {
                    AddedComment = new Models.CommentViewModel(), //Comment user might add
                    IsAdmin = User.IsInRole("Admin"),
                    CurrentUser = UserManager_.GetUserId(User)
                };
                Mapper_.Map(post, model);
                model.PageCount = UnitOfWork_.CommentsRepository.GetCommentsPageCount(id, ON_PAGE);
                if (page > model.PageCount)
                    page = model.PageCount;
                model.PageNum = (int)page;

                var comments = UnitOfWork_.CommentsRepository.GetCommentsPage(post.Id, model.PageNum, ON_PAGE); //Comments on this page
                if (comments != null)
                {
                    Mapper_.Map(comments, model.DisplayedComments);
                } //Adding them to list of displaying comments
                return View(model);
            }
            return View("Error");
        }
        
        public IActionResult Delete(long id, int page, long postId)
        {
            if (!UnitOfWork_.CommentsRepository.Remove(id))
                return View("Error"); //There's no such comment in database
            UnitOfWork_.Commit();
            return RedirectToAction("Show", "Post", new { id = postId, page });//Open same post, same page
        }
        [HttpPost]
        public IActionResult Create(Models.BlogPostViewModel model) //Checking if added comment is valid and saving it to database
        {
            if (ModelState.IsValid) 
            {
                var entity = new DAL.Models.Comment
                {
                    Text = model.AddedComment.Text,
                    AuthorId = UserManager_.GetUserId(User),
                    PostId = model.Id,
                    PostingTime = DateTime.Now
                };
                UnitOfWork_.CommentsRepository.Add(entity);
                UnitOfWork_.Commit();
                return RedirectToAction("Show", "Post", new { id = model.Id });
            }
            return View("Show", model); //If not - retry
        }
    }
}