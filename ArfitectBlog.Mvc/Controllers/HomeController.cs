using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Services.Abstract;

namespace ArfitectBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId)
        {
            var postsResult = await (categoryId == null
                ? _postService.GetAllByNonDeletedAndActiveAsync()
                : _postService.GetAllByCategoryAsync(categoryId.Value));
            return View(postsResult.Data);
        }
    }
}
