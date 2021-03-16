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
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var postsResult = await (categoryId == null
                ? _postService.GetAllByPagingAsync(null, currentPage, pageSize, isAscending)
                : _postService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize, isAscending));
            return View(postsResult.Data);
        }
    }
}
