using Assignment2.Models;
using Assignment2.Models.ViewModels;
using AutoMapper;

namespace Assignment2.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<PostCreateViewModel, Post>().ReverseMap();
            CreateMap<PostUpdateViewModel, Post>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
