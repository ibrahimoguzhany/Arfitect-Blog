using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArfitectBlog.Entities.Dtos
{
    public class ImageDeletedDto
    {
        public string FullName { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
