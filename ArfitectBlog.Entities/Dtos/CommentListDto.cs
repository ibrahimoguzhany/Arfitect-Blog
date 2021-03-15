using System.Collections.Generic;
using ArfitectBlog.Entities.Concrete;

namespace ArfitectBlog.Entities.Dtos
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
