using AutoMapper;
using Blog.Data.Repository;
using Blog.Data.Repository.iRepository;
using Blog.Models;
using Blog.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly iUsersRepository _UsersRepository;
        private readonly IMapper _mapper;
        private readonly iBlogsRepository _BlogsRepository;
        private string secretKey;
        public UsersController(iBlogsRepository blogsRepository, iUsersRepository usersRepository, IConfiguration configuration, IMapper mapper)
        {
            _BlogsRepository = blogsRepository;
            _UsersRepository = usersRepository;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers()
        {
            List<Users> userslist = await _UsersRepository.GetAllAsync();
            if (userslist == null)
            {
                return BadRequest();
            }
            return Ok(userslist);
        }
        [HttpGet("{id:int}", Name = "GetUserByid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserbyid(int id)
        {
            if (id == null) { return BadRequest(); }

            Users users = await _UsersRepository.GetAsync(u => u.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UsersDto userdto)
        {
            Users users = await _UsersRepository.GetAsync(u => u.Name.ToLower() == userdto.Name.ToLower());
            if (userdto == null || users != null) { return BadRequest(); }
            Users Userinsert = _mapper.Map<Users>(userdto);
            await _UsersRepository.CreateAsync(Userinsert);
            return StatusCode(201);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0) { return BadRequest(); }
            Users usercheck = await _UsersRepository.GetAsync(u => u.Id == id);
            if (usercheck == null) { return NotFound(); }
            await _UsersRepository.RemoveAsync(usercheck);
            return Ok();
        }
        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users users)
        {
            if (users == null || (id != users.Id)) { return BadRequest(); }
            Users usercheck = await _UsersRepository.GetAsync(u => u.Id == id, false);
            if (usercheck == null) { return BadRequest(); }
            await _UsersRepository.UpdateAsync(users);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            Users usersauthentication = await _UsersRepository.GetAsync(u => u.Email.ToLower() == model.Email.ToLower() &&  u.Password.ToLower() == model.Password.ToLower());
            if (usersauthentication == null)
            {
                return BadRequest();
            }
            var roles = usersauthentication.Role;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
          new Claim(ClaimTypes.Name, usersauthentication.Name.ToString()),
          new Claim(ClaimTypes.Role, roles)
              }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var sendToken = tokenHandler.WriteToken(token);
            return Ok(sendToken);
        }

        [HttpPost("SubscribeUnsubscribe")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubscribeUnsubscribe([FromBody] SubscribeUnHelper unsubscribesubs)
        {
            Boolean status = true;
            Users users = await _UsersRepository.GetAsync(u => u.Id == unsubscribesubs.Userid,false);
            Blogs blogs = await _BlogsRepository.GetAsync(u => u.Id == unsubscribesubs.Blogid,false);
            if (users == null || unsubscribesubs == null || blogs==null) { return BadRequest(); }
            foreach (var i in users.blogsubscribed)
            {
                if(i== unsubscribesubs.Blogid)
                {
                    status=false; break;
                }
            }
            if (status)
            {
                if(blogs.SubscriptionsUsed>= blogs.SubscriptionsAllowed)
                {
                    return BadRequest();
                }
                users.blogsubscribed.Add(unsubscribesubs.Blogid);
                blogs.SubscriptionsUsed = 1 + blogs.SubscriptionsUsed;
            }
            else
            {
                users.blogsubscribed.Remove(unsubscribesubs.Blogid);
                blogs.SubscriptionsUsed =  blogs.SubscriptionsUsed -1 ;
            }
            await _UsersRepository.UpdateAsync(users);
            await _BlogsRepository.UpdateAsync(blogs);
            unsubscribesubs.Isstatus= status;
            return Ok(unsubscribesubs);
        }

        


    }
}
