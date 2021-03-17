using System.Collections.Generic;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Shared.Entities.Abstract;

namespace ArfitectBlog.Entities.Dtos
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
