﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Shared.Entities.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;

namespace ArfitectBlog.Entities.Dtos
{
    public class PostDto:DtoGetBase
    {
        public Post Post { get; set; }
    }
}
