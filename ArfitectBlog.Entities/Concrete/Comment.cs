using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Shared.Entities.Abstract;

namespace MyBlog.Entities.Concrete
{
    public class Comment:EntityBase,IEntity
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
