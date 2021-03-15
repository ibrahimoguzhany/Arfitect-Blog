using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Entities.Concrete;

namespace ArfitectBlog.Mvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int PostsCount { get; set; }
        public int CommentsCount { get; set; }
        public int UsersCount { get; set; }
        public PostListDto Posts { get; set; }

    }
}
