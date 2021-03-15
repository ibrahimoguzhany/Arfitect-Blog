using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Entities.ComplexTypes;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Shared.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;

namespace ArfitectBlog.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null);
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }

}
