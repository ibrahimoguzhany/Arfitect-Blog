using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Areas.Admin.Models;

namespace MyBlog.Mvc.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<PostAddViewModel, PostAddDto>();
            CreateMap<PostUpdateDto, PostUpdateViewModel>().ReverseMap();
        }
    }
}
