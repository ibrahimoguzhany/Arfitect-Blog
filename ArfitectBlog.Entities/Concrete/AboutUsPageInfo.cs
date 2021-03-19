using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArfitectBlog.Entities.Concrete
{
    public class AboutUsPageInfo
    {
        [DisplayName("Baslik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(150, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string Header { get; set; }
        [DisplayName("Icerik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(5000, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(4, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string Content { get; set; }
        [DisplayName("Seo Aciklama")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(4, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo Etiketleri")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(4, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string SeoTags { get; set; }
        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(60, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir")]
        [MinLength(4, ErrorMessage = "{0} alani {1} karakterden kucuk olmamalidir")]
        public string SeoAuthor { get; set; }
    }
}
