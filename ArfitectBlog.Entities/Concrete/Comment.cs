using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Shared.Entities.Abstract;

namespace ArfitectBlog.Entities.Concrete
{
    public class Comment:EntityBase,IEntity
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
