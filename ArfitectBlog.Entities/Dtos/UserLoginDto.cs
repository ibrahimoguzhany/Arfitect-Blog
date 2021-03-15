using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ArfitectBlog.Shared.Entities.Abstract;

namespace ArfitectBlog.Entities.Dtos
{
    public class UserLoginDto : DtoGetBase
    {
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

        [DisplayName("Beni hatırla")]
        public bool RememberMe { get; set; }


    }
}
