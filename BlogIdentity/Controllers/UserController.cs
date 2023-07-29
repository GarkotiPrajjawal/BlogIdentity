using BlogIdentity.Models;
using BlogIdentity.Services.iServices;
using Blog.Models;
using Blog.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BlogIdentity.Controllers
{
    public class UserController : Controller
    {
        private readonly iUserService _UserService;
        public UserController(iUserService UserService)
        {
           _UserService=UserService;

        }

        public async Task<IActionResult> Index()
        {
            List<Users> list = new();
            var response = await _UserService.GetAllAsync<List<Users>>();
            if (response != null)
            {
                list = response;
            }
            return View(list);
        }
        

        
        public async Task<IActionResult> GetUserbyid(int id)
        {
            Users user = new();
            var response = await _UserService.GetAsync<Users>(id);
            if (response != null)
            {
                user = response;
            }
            return View(user);
        }
        public async Task<IActionResult> CreateUser(Userhelper userhelper)
        {
            UsersDto userdto = new UsersDto();
            userdto.Name = userhelper.Name; 
            userdto.Email= userhelper.Email;
            userdto.Password = userhelper.Password;
            userdto.Role= userhelper.Role;
            userdto.blogsubscribed.Add(0);
            await _UserService.CreateAsync<UsersDto>(userdto);
           
            return View();
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            await _UserService.DeleteAsync<Users>(id);

            return View();
        }

        public async Task<IActionResult> UpdateUser(Users users)
        {
            await _UserService.UpdateAsync<Users>(users);

            return View();
        }

        public async Task<IActionResult> SubscribeUnsubscribe(Subscribeunsubscribe unsubscribesubs)
        {
            SubscribeUnHelper subscribeUnHelper = new SubscribeUnHelper();
            subscribeUnHelper.Isstatus = false;
            subscribeUnHelper.Userid = unsubscribesubs.Userid;
            subscribeUnHelper.Blogid = unsubscribesubs.Blogid;
            var status1=await _UserService.SubscribeUnsubscribe<SubscribeUnHelper>(subscribeUnHelper);
            Boolean status = status1.Isstatus;
            subscribeUnHelper.Isstatus = status;

            return View(unsubscribesubs);
        }


    }
}