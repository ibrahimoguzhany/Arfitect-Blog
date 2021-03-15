using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Shared.Entities.Abstract;

namespace ArfitectBlog.Entities.Dtos
{
    public class UserListDto : DtoGetBase
    {
        public IList<User> Users { get; set; }
    }
}
