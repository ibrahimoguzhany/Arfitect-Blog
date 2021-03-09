using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kategori Adi")]
        [Required(ErrorMessage = "{0} adi bos gecilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        public string Name { get; set; }
        [DisplayName("Kategori Aciklamasi")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        public string Description { get; set; }
        [DisplayName("Kategori Ozel Not Alani")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden buyuk olmamalidir.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kucuk olmamalidir.")]
        public string Note { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} bos gecilmemelidir.")]
        public bool IsActive { get; set; }

        [DisplayName("Silindi Mi?")]
        [Required(ErrorMessage = "{0} bos gecilmemelidir.")]
        public bool IsDeleted { get; set; }
    }
}
