using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Mvc.Areas.Admin.Models;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ArfitectBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager, ICommentService commentService, IPostService postService, ICategoryService categoryService)
        {
            _userManager = userManager;
            _commentService = commentService;
            _postService = postService;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "SuperAdmin,AdminArea.Home.Read")]
        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountByNonDeletedAsync();
            var postsCountResult = await _postService.CountByNonDeletedAsync();
            var commentsCountResult = await _commentService.CountByNonDeletedAsync();
            var usersCount = await _userManager.Users.CountAsync();
            var postsResult = await _postService.GetAllAsync();
            if (
                categoriesCountResult.ResultStatus == ResultStatus.Success &&
                postsCountResult.ResultStatus == ResultStatus.Success &&
                commentsCountResult.ResultStatus == ResultStatus.Success && usersCount > -1 &&
                postsResult.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel
                {
                    CategoriesCount = categoriesCountResult.Data,
                    PostsCount = postsCountResult.Data,
                    CommentsCount = commentsCountResult.Data,
                    UsersCount = usersCount,
                    Posts = postsResult.Data
                });
            }
            return NotFound();
        }
    }
}
