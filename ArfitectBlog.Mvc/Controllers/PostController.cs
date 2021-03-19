using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Data.Concrete;
using ArfitectBlog.Entities.ComplexTypes;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Mvc.Models;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.Extensions.Options;

namespace ArfitectBlog.Mvc.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly PostRightSideBarWidgetOptions _postRightSideBarWidgetOptions;


        public PostController(IPostService postService, IOptionsSnapshot<PostRightSideBarWidgetOptions> postRightSideBarWidgetOptions)
        {
            _postService = postService;
            _postRightSideBarWidgetOptions = postRightSideBarWidgetOptions.Value;
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
                var userPosts = await _postService.GetAllByUserIdOnFilter(
                    userId: postResult.Data.Post.UserId,
                    filterBy: _postRightSideBarWidgetOptions.FilterBy, 
                    orderBy: _postRightSideBarWidgetOptions.OrderBy, 
                    isAscending: _postRightSideBarWidgetOptions.IsAscending, 
                    takeSize: _postRightSideBarWidgetOptions.TakeSize,
                    categoryId: _postRightSideBarWidgetOptions.CategoryId, 
                    startAt: _postRightSideBarWidgetOptions.StartAt, 
                    endAt: _postRightSideBarWidgetOptions.EndAt,
                    minViewCount: _postRightSideBarWidgetOptions.MinViewCount,
                    maxCommentCount: _postRightSideBarWidgetOptions.MaxCommentCount,
                    minCommentCount: _postRightSideBarWidgetOptions.MinCommentCount, 
                    maxViewCount: _postRightSideBarWidgetOptions.MinViewCount);

                await _postService.IncreaseViewCountAsync(postId);
                return View(new PostDetailViewModel()
                {
                    PostDto = postResult.Data,
                    PostDetailRightSideBarViewModel = new PostDetailRightSideBarViewModel
                    {
                        PostListDto = userPosts.Data,
                        Header = _postRightSideBarWidgetOptions.Header,
                        User=postResult.Data.Post.User
                    },
                });
            }

            return NotFound();
        }
    }
}
