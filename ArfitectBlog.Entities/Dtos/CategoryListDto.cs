using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Shared.Entities.Abstract;

namespace ArfitectBlog.Entities.Dtos
{
    public class CategoryListDto : DtoGetBase
    {
        public IList<Category> Categories { get; set; }

    }
}
