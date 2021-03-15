using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Shared.Entities.Abstract;

namespace ArfitectBlog.Entities.Concrete
{
    public class Category:EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
