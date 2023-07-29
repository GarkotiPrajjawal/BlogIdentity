using AutoMapper;
using Blog.Data.Repository;
using Blog.Data.Repository.iRepository;
using Blog.Models;
using Blog.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("Blog")]
    public class BlogController : ControllerBase
    {
        private readonly iBlogsRepository _BlogsRepository;
        private readonly IMapper _mapper;
        public BlogController(iBlogsRepository blogsRepository, IMapper mapper)
        {
            _BlogsRepository = blogsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBlogs()
        {
            var blogslist = await _BlogsRepository.GetAllAsync();
            if (blogslist == null)
            {
                return BadRequest();
            }
            return Ok(blogslist);
        }
        [HttpGet("{id:int}", Name = "GetBlogByid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlogbyid(int id)
        {
            if (id == null) { return BadRequest(); }

            Blogs blog = await _BlogsRepository.GetAsync(u => u.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBlog([FromBody] BlogsDto blogdto)
        {
            Blogs blogs = await _BlogsRepository.GetAsync(u => u.Title.ToLower() == blogdto.Title.ToLower());
            if (blogdto == null || blogs != null)
            {
                return BadRequest();
            }

            // Check the SubscriptionsUsed and SubscriptionsAllowed condition
            if (blogdto.SubscriptionsUsed > blogdto.SubscriptionsAllowed)
            {
                return BadRequest();
            }

            Blogs bloginsert = _mapper.Map<Blogs>(blogdto);
            await _BlogsRepository.CreateAsync(bloginsert);
            return StatusCode(201);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteBlog")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (id == 0) { return BadRequest(); }
            Blogs Blogcheck = await _BlogsRepository.GetAsync(u => u.Id == id);
            if (Blogcheck == null) { return NotFound(); }
            await _BlogsRepository.RemoveAsync(Blogcheck);
            return Ok();
        }

        [HttpPut("{id:int}", Name = "UpdateBlog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] Blogs blogs)
        {
            if (blogs == null || (id != blogs.Id)) { return BadRequest(); }
            Blogs blogcheck = await _BlogsRepository.GetAsync(u => u.Id == id, false);
            if (blogcheck == null) { return BadRequest(); }
            await _BlogsRepository.UpdateAsync(blogs);
            return Ok();
        }

        [HttpGet("GetBlogwithmaximumsubscriber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlogwithmaximumsubscriber()
        {
            var blogslist = await _BlogsRepository.GetAllAsync();
            if (blogslist == null || blogslist.Count == 0)
            {
                return NotFound();
            }
            int m = Int32.MinValue;
            Blogs blogresult = null;
            foreach (var blog in blogslist)
            {
                if (blog.SubscriptionsUsed>m)
                {
                    m=blog.SubscriptionsUsed;
                    blogresult=blog;
                }
            }
                return Ok(blogresult);
        }

    }
}
