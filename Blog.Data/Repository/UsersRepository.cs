using Blog.Data.Repository.iRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class UsersRepository : Repository<Users>, iUsersRepository
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
