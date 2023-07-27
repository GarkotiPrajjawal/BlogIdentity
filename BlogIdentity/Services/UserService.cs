using BlogIdentity.Services.iServices;
using Blog.Models;
using Blog.Models.Dto;
//using Humanizer;

namespace BlogIdentity.Services
{
    public class UserService : BaseService, iUserService
    {
        // private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpClientFactory _clientFactory;
        private string UserUrl;

        public UserService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            UserUrl = configuration.GetValue<string>("ServiceUrls:UserAPI");

        }
        public Task<T> CreateAsync<T>(UsersDto dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = UserUrl + "/Users"
                //Token = token
            });
        }



        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = UserUrl + "/Users/" + id
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = UserUrl + "/Users"
                //  Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = UserUrl + "/Users/" + id
               // Token = token
            });
        }

        public Task<T> UpdateAsync<T>(Users dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = UserUrl + "/Users/" + dto.Id
                //Token = token
            });
        }
        public Task<T> LoginAsync<T>(LoginRequestDto model)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = model,
                Url = UserUrl + "/Users/login" 
                //Token = token
            });
        }
        public Task<T> SubscribeUnsubscribe<T>(SubscribeUnHelper unsubscribesubs)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = unsubscribesubs,
                Url = UserUrl + "/Users/SubscribeUnsubscribe"
                //Token = token
            });
        }
    }
}