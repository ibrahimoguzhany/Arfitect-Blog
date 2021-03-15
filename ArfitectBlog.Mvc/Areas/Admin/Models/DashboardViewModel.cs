using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Mvc.Areas.Admin.Models
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
