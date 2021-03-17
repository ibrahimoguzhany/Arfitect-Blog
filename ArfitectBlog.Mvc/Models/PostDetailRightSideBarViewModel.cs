using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;

namespace ArfitectBlog.Mvc.Models
{
    public class PostDetailRightSideBarViewModel
    {
        public string Header { get; set; }
        public PostListDto PostListDto { get; set; }
        public User User { get; set; }

    }
}
