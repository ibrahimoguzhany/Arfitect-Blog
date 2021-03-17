using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Data.Concrete;
using ArfitectBlog.Mvc.Models;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;

namespace ArfitectBlog.Mvc.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var searchResult = await _postService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            if (searchResult.ResultStatus == ResultStatus.Success)
            {
                return View(new PostSearchViewModel()
                {
                    PostListDto = searchResult.Data,
                    Keyword = keyword
                });
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int postId)
        {
            var postResult = await _postService.GetAsync(postId);
            if (postResult.ResultStatus == ResultStatus.Success)
            {
                await _postService.IncreaseViewCountAsync(postId);
                return View(postResult.Data);
            }

            return NotFound();
        }
    }
}
