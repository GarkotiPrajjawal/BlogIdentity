using Blog.Data.Repository.iRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class BlogStatusRepository : Repository<PendingBlogs>, iBlogStatusRepository
    {
        private readonly ApplicationDbContext _db;

        public BlogStatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
