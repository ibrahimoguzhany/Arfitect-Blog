using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyBlog.Entities.Concrete;

namespace MyBlog.Mvc.Areas.Admin.Models
{
    public class PostAddViewModel
    {
        [DisplayName("Baslik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string Title { get; set; }
        [DisplayName("Icerik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MinLength(20, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string Content { get; set; }
        [DisplayName("Küçük Resim")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        public IFormFile ThumbnailFile { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar Adı")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(50, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string SeoAuthor { get; set; }
        [DisplayName("Makale Açıklaması")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(150, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]

        public string SeoDescription { get; set; }
        [DisplayName("Makale Etiketleri")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        public int CategoryId { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        public bool IsActive { get; set; }
        public IList<Category> Categories { get; set; }
    }
}
