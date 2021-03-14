using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Helpers.Abstract;
using MyBlog.Shared.Utilities.Extensions;
using MyBlog.Shared.Utilities.Results.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using MyBlog.Shared.Utilities.Results.Concrete;

namespace MyBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private const string imgFolder = "img";
        private const string userImagesFolder = "userImages";
        private const string postImagesFolder = "postImages";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public async Task<IDataResult<UploadedImageDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null)
        {
            // Eğer folderName değişkeni null gelir ise, o zaman resim tipine göre (PictureType) klasör adı ataması yapılır.
            folderName ??= pictureType == PictureType.User ? userImagesFolder : postImagesFolder;

                // Eger folderName degiskeni ile gelen klasor adi sistemimizde mevcut degilse, yeni bir klasor olusturulur.
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            // Resimin yuklenme sirasindaki ilk adi oldFIleName adli degiskene atanir.
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);

            //Resimin uzantisi fileExtension adli degiskene atanir
            string fileExtension = Path.GetExtension(pictureFile.FileName);

            DateTime dateTime = DateTime.Now;
            /*
             * Parametre ile gelen degeler kullanilarak yeni bir resim adi olusturulur.
             * Ornegin OguzhanYilmaz_234_1_64_24_5_19_2021.png
             */
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";

            //Kendi parametlerimiz ile sistemimize uygun yeni bir dosya yolu (path) olusturulur.
            string path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            //Sistemimiz icin olusturulan yeni dosya yoluna resim kopyalanir.
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            //Resim tipine gore kullanici icin mesaj olusturulur.
            string message = pictureType == PictureType.User
                ? $"{name} kullanıcısının resmi başarıyla yüklenmiştir."
                : $"{name} makalenin resmi başarıyla yüklenmiştir.";
            return new DataResult<UploadedImageDto>(ResultStatus.Success, message, new UploadedImageDto()
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });
        }
    }
}
