using System.Collections.Generic;
using MyBlog.Entities.Concrete;

namespace MyBlog.Entities.Dtos
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
