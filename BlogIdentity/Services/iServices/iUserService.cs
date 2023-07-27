using Blog.Models;
using Blog.Models.Dto;

namespace BlogIdentity.Services.iServices
{
    public interface iUserService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(UsersDto dto);
        Task<T> UpdateAsync<T>(Users dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> LoginAsync<T>(LoginRequestDto model);
        Task<T> SubscribeUnsubscribe<T>(SubscribeUnHelper unsubscribesubs);
    }
}
