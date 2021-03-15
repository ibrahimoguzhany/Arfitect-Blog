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
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5)
        {
            var postsResult = await (categoryId == null
                ? _postService.GetAllByPagingAsync(null, currentPage, pageSize)
                : _postService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize));
            return View(postsResult.Data);
        }
    }
}
