using Bussines.Abstract;
using Core.Results.Concrete;
using DataAcces.SqlServerDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ServiceResult GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return new ServiceResult { IsSuccess = true, Data = user };
            }
            return new ServiceResult { IsSuccess = false, Message = "User not found." };
        }
    }
}
