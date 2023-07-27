using Blog.Models.Dto;
using Blog.Models;

namespace BlogIdentity.Services.iServices
{
    public interface iBlogService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(BlogsDto dto);
        Task<T> UpdateAsync<T>(Blogs dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> GetBlogwithmaximumsubscriber<T>();
    }
}
