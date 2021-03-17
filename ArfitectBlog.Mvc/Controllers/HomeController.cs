using System;
using System.Data.SqlTypes;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Dtos;

namespace ArfitectBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;

        public HomeController(IPostService postService, IOptions<AboutUsPageInfo> aboutUsPageInfo)
        {
            _postService = postService;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var postsResult = await (categoryId == null
                ? _postService.GetAllByPagingAsync(null, currentPage, pageSize, isAscending)
                : _postService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize, isAscending));
            return View(postsResult.Data);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            return View();
        }
    }
}
