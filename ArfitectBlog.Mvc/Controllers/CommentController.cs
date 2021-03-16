using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Mvc.Models;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Shared.Utilities.Extensions;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ArfitectBlog.Mvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<JsonResult> Add(CommentAddDto commentAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _commentService.AddAsync(commentAddDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var commentAddAjaxViewModel = JsonSerializer.Serialize(new CommentAddAjaxViewModel()
                    {
                        CommentDto = result.Data,
                        CommentAddPartial = await this.RenderViewToStringAsync("_CommentAddPartial", commentAddDto)
                    }, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
                    return Json(commentAddAjaxViewModel);
                }
                ModelState.AddModelError("", result.Message);
            }
            var commentAddAjaxErrorModel = JsonSerializer.Serialize(new CommentAddAjaxViewModel()
            {
                CommentAddDto = commentAddDto,
                CommentAddPartial = await this.RenderViewToStringAsync("_CommentAddPartial", commentAddDto)
            });
            return Json(commentAddAjaxErrorModel);
        }
        
    }
}
