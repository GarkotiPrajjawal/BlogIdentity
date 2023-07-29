using AutoMapper;
using Blog.Models.Dto;
using Blog.Models;

namespace BlogApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<Blogs, BlogsDto>().ReverseMap();
        }
    }
}
