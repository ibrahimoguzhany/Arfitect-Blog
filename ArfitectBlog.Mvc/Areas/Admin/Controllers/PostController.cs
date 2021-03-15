using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ArfitectBlog.Entities.ComplexTypes;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Mvc.Areas.Admin.Models;
using ArfitectBlog.Mvc.Helpers.Abstract;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;

namespace ArfitectBlog.Mvc.Areas.Admin.Controllers
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
        [Authorize(Roles = "SuperAdmin,Post.Read")]
        public async Task<IActionResult> Index()
        {
            var result = await _postService.GetAllByNoneDeletedAsync();
            if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Post.Create")]
        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNoneDeletedAndActiveAsync();
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
        [Authorize(Roles = "SuperAdmin,Post.Create")]
        public async Task<IActionResult> Add(PostAddViewModel postAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var postAddDto = Mapper.Map<PostAddDto>(postAddViewModel);
                var imageResult = await ImageHelper.Upload(postAddViewModel.Title, postAddViewModel.ThumbnailFile,
                    PictureType.Post);
                postAddDto.Thumbnail = imageResult.Data.FullName;
                var result = await _postService.AddAsync(postAddDto, LoggedInUser.UserName,LoggedInUser.Id);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    TempData.Add("SuccessMessage",result.Message);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNoneDeletedAndActiveAsync();
            postAddViewModel.Categories = categories.Data.Categories;
            return View(postAddViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Post.Update")]
        public async Task<IActionResult> Update(int postId)
        {
            var postResult = await _postService.GetPostUpdateDtoAsync(postId);
            var categoriesResult = await _categoryService.GetAllByNoneDeletedAndActiveAsync();
            if (postResult.ResultStatus == ResultStatus.Success && categoriesResult.ResultStatus==ResultStatus.Success)
            {
                var postUpdateViewModel = Mapper.Map<PostUpdateViewModel>(postResult.Data);
                postUpdateViewModel.Categories = categoriesResult.Data.Categories;
                return View(postUpdateViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Post.Update")]
        public async Task<IActionResult> Update(PostUpdateViewModel postUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = postUpdateViewModel.Thumbnail;
                if (postUpdateViewModel.ThumbnailFile != null)
                {
                    var uploadedImageResult = await ImageHelper.Upload(postUpdateViewModel.Title,
                        postUpdateViewModel.ThumbnailFile, PictureType.Post);
                    postUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if(oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var postUpdateDto = Mapper.Map<PostUpdateDto>(postUpdateViewModel);
                var result = await _postService.UpdateAsync(postUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    if (isNewThumbnailUploaded)
                    {
                        ImageHelper.Delete(oldThumbnail);
                    }

                    TempData.Add("SuccessMessage", result.Message);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNoneDeletedAndActiveAsync();
            postUpdateViewModel.Categories = categories.Data.Categories;
            return View(postUpdateViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Post.Delete")]
        public async Task<JsonResult> Delete(int postId)
        {
            var result = await _postService.DeleteAsync(postId, LoggedInUser.UserName);
            var postResult = JsonSerializer.Serialize(result);
            return Json(postResult);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Post.Read")]
        public async Task<JsonResult> GetAllPosts()
        {
            var posts = await _postService.GetAllByNonDeletedAndActiveAsync();
            var postResult = JsonSerializer.Serialize(posts, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(postResult);
        }

        [Authorize(Roles = "SuperAdmin,Post.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedPosts()
        {
            var result = await _postService.GetAllByDeletedAsync();
            return View(result.Data);

        }
        [Authorize(Roles = "SuperAdmin,Post.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedPosts()
        {
            var result = await _postService.GetAllByDeletedAsync();
            var posts = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(posts);
        }
        [Authorize(Roles = "SuperAdmin,Post.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int postId)
        {
            var result = await _postService.UndoDeleteAsync(postId, LoggedInUser.UserName);
            var undoDeletePostResult = JsonSerializer.Serialize(result);
            return Json(undoDeletePostResult);
        }
        [Authorize(Roles = "SuperAdmin,Post.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int postId)
        {
            var result = await _postService.HardDeleteAsync(postId);
            var hardDeletedPostResult = JsonSerializer.Serialize(result);
            return Json(hardDeletedPostResult);
        }
    }
}
