using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Areas.Admin.Models;
using MyBlog.Mvc.Helpers.Abstract;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;

namespace MyBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        public PostController(IPostService postService, ICategoryService categoryService,UserManager<User> userManager,IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _postService.GetAllByNoneDeletedAsync();
            if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNoneDeletedAndActive();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(new PostAddViewModel
                {
                    Categories = result.Data.Categories
                });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostAddViewModel postAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var postAddDto = Mapper.Map<PostAddDto>(postAddViewModel);
                var imageResult = await ImageHelper.Upload(postAddViewModel.Title, postAddViewModel.ThumbnailFile,
                    PictureType.Post);
                postAddDto.Thumbnail = imageResult.Data.FullName;
                var result = await _postService.AddAsync(postAddDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    TempData.Add("SuccessMessage",result.Message);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(postAddViewModel);
                }
            }
            return View(postAddViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int postId)
        {
            var postResult = await _postService.GetPostUpdateDtoAsync(postId);
            var categoriesREsult = await _categoryService.GetAllByNoneDeletedAndActive();

        }
    }
}
