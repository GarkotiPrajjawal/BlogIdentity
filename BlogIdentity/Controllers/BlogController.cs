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
            if (response != null)
            {
                foreach (var item in response)
                {
                    if (item.Status == "approved")
                    {
                        list.Add(item);
                    }
                }
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
        public async Task<IActionResult> CreateBlog(PendingBlogsDto pendingblogdto)
        {
            BlogsDto blogsdto= new BlogsDto();
            blogsdto.Title = pendingblogdto.Title;
            blogsdto.Content=pendingblogdto.Content;
            blogsdto.Category = pendingblogdto.Category;
            blogsdto.SubscriptionsAllowed = pendingblogdto.SubscriptionsAllowed;
            blogsdto.SubscriptionsUsed = pendingblogdto.SubscriptionsUsed;
            blogsdto.Status = "pending";
            await _BlogService.CreateAsync<BlogsDto>(blogsdto);

            return View();
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _BlogService.DeleteAsync<Blogs>(id);

            return View();
        }

        public async Task<IActionResult> UpdateBlog(PendingBlogs pendingblogs)
        {
            Blogs blogs = new Blogs();
            blogs.Id = pendingblogs.Id;
            blogs.Title = pendingblogs.Title;
            blogs.Content= pendingblogs.Content;
            blogs.Category = pendingblogs.Category;
            blogs.SubscriptionsAllowed = pendingblogs.SubscriptionsAllowed;
            blogs.SubscriptionsUsed = pendingblogs.SubscriptionsUsed;
            blogs.Status = "pending";
            await _BlogService.UpdateAsync<Blogs>(blogs);

            return View();
        }

        public async Task<IActionResult> GetBlogwithmaximumsubscriber()
        {
            Blogs blog=await _BlogService.GetBlogwithmaximumsubscriber<Blogs>();
            return View(blog);
        }
        public async Task<IActionResult> GetPendingBlog()
        {
            List<Blogs> list = new();
            var response = await _BlogService.GetAllAsync<List<Blogs>>();
            if (response != null)
            {
                foreach (var item in response)
                {
                    if(item.Status == "pending")
                    {
                        list.Add(item);
                    }
                }
            }
            return View(list);
        }
        public async Task<IActionResult> ApproveBlog(int id)
        {
            Blogs blogs = await _BlogService.GetAsync<Blogs>(id);
            blogs.Status = "approved";
            await _BlogService.UpdateAsync<Blogs>(blogs);
            return RedirectToAction("GetPendingBlog");
        }
        public async Task<IActionResult> RejectBlog(int id)
        {
            Blogs response = await _BlogService.GetAsync<Blogs>(id);
            response.Status = "rejected";
            await _BlogService.UpdateAsync<Blogs>(response);
            return RedirectToAction("GetPendingBlog");
        }
    }
}
