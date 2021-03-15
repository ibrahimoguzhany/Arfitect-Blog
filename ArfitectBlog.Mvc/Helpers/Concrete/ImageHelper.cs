using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ArfitectBlog.Shared.Utilities.Extensions;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArfitectBlog.Entities.ComplexTypes;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Mvc.Helpers.Abstract;
using ArfitectBlog.Shared.Utilities.Results.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;
using ArfitectBlog.Shared.Utilities.Results.Concrete;

namespace ArfitectBlog.Mvc.Helpers.Concrete
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

        public async Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null)
        {
            // Eğer folderName değişkeni null gelir ise, o zaman resim tipine göre (PictureType) klasör adı ataması yapılır.
            folderName ??= pictureType == PictureType.User ? userImagesFolder : postImagesFolder;

                // Eger folderName degiskeni ile gelen klasor adi sistemimizde mevcut degilse, yeni bir klasor olusturulur.
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            // Resimin yuklenme sirasindaki ilk adi oldFileName adli degiskene atanir.
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);

            //Resimin uzantisi fileExtension adli degiskene atanir
            string fileExtension = Path.GetExtension(pictureFile.FileName);


            Regex regex = new Regex("[*'\",._&#^@]");
            name = regex.Replace(name, string.Empty);

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
            return new DataResult<ImageUploadedDto>(ResultStatus.Success, message, new ImageUploadedDto()
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });
        }

        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {

            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto()
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.Error, $"Böyle bir resim bulunamadı.",null);
            }
        }
    }
}
