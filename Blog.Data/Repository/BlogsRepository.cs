using Blog.Data.Repository.iRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class BlogsRepository : Repository<Blogs>, iBlogsRepository
    {
        private readonly ApplicationDbContext _db;

        public BlogsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
   
}
