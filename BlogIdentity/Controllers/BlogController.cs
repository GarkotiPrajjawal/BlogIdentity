using BlogIdentity.Services;
using BlogIdentity.Services.iServices;
using Blog.Models;
using Blog.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlogIdentity.Controllers
{
    public class BlogController : Controller
    {
        private readonly iBlogService _BlogService;
        public BlogController(iBlogService BlogService)
        {
            _BlogService = BlogService;

        }
        public async Task<IActionResult> Index()
        {
            List<Blogs> list = new();
            var response = await _BlogService.GetAllAsync<List<Blogs>>();
                ;
            if (response != null)
            {
                list = response;
            }
            return View(list);
        }
        public async Task<IActionResult> GetBlogbyid(int id)
        {
            Blogs blog = new();
            var response = await _BlogService.GetAsync<Blogs>(id);
            if (response != null)
            {
                blog = response;
            }
            return View(blog);
        }
        public async Task<IActionResult> CreateBlog(BlogsDto blogdto)
        {
            await _BlogService.CreateAsync<BlogsDto>(blogdto);

            return View();
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _BlogService.DeleteAsync<Blogs>(id);

            return View();
        }

        public async Task<IActionResult> UpdateBlog(Blogs blogs)
        {
            await _BlogService.UpdateAsync<Blogs>(blogs);

            return View();
        }

        public async Task<IActionResult> GetBlogwithmaximumsubscriber()
        {
            Blogs blog=await _BlogService.GetBlogwithmaximumsubscriber<Blogs>();
            return View(blog);
        }
    }
}
