using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Dtos;

namespace ArfitectBlog.Mvc.Models
{
    public class PostSearchViewModel
    {
        public PostListDto PostListDto { get; set; }
        public string Keyword { get; set; }

    }
}
