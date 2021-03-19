using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Mvc.Areas.Admin.Models;
using AutoMapper;

namespace ArfitectBlog.Mvc.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<PostAddViewModel, PostAddDto>();
            CreateMap<PostUpdateDto, PostUpdateViewModel>().ReverseMap();
            CreateMap<PostRightSideBarWidgetOptions, PostRightSideBarWidgetOptionsViewModel>();
        }
    }
}
