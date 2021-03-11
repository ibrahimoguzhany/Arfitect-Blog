using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyBlog.Shared.Entities.Abstract;

namespace MyBlog.Entities.Dtos
{
    public class UserAddDto:DtoGetBase
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        public string UserName { get; set; }
        [DisplayName("Email Adresi")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Parola")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Telefon numarası")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(13, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")] // +90 444 444 44 44
        [MinLength(13, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim")]
        [Required(ErrorMessage = "Lütfen bir {0} ekleyiniz.")]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
    }
}
