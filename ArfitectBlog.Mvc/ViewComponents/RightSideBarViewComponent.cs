using ArfitectBlog.Mvc.Models;
using ArfitectBlog.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArfitectBlog.Mvc.ViewComponents
{
    public class RightSideBarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public RightSideBarViewComponent(ICategoryService categoryService, IPostService postService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesResult = await _categoryService.GetAllByNoneDeletedAndActiveAsync();
            var postsResult = await _postService.GetAllByViewCountAsync(isAscending: false, takeSize: 5);
            return View(new RightSideBarViewModel()
            {
             Categories  = categoriesResult.Data.Categories,
             Posts = postsResult.Data.Posts
            });
        }
    }
}
