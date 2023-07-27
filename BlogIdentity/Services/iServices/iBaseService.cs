using Blog.Models;

namespace BlogIdentity.Services.iServices
{
    public interface iBaseService
    {
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
