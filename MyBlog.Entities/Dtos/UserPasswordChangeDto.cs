using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şuanki Parolanız")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Parolanız")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Parolanızın Tekrarı")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage = "Girmiş olduğunuz yeni parola ile yeni parolanızın tekrar alanları birbiri ile aynı olmalıdır.")]
        public string RepeatPassword { get; set; }
    }
}
