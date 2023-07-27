using BlogIdentity.Services.iServices;
using Blog.Models.Dto;
using Blog.Models;
//using Humanizer;

namespace BlogIdentity.Services
{
    public class BlogService : BaseService, iBlogService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string UserUrl;

        public BlogService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            UserUrl = configuration.GetValue<string>("ServiceUrls:UserAPI");

        }
        public Task<T> CreateAsync<T>(BlogsDto dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = UserUrl + "/Blog"
                //Token = token
            });
        }



        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = UserUrl + "/Blog/" + id
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = UserUrl + "/Blog"
                //  Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = UserUrl + "/Blog/" + id
                // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(Blogs dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = UserUrl + "/Blog/" + dto.Id
                //Token = token
            });
        }
        public Task<T> GetBlogwithmaximumsubscriber<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = UserUrl + "/Blog/GetBlogwithmaximumsubscriber"
                //Token = token
            });
        }
    }
}
