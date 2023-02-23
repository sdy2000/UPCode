using Core.Convertors;
using Core.Servises.Interfaces;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Servises
{
    public class UserService : IUserService
    {
        private UPCodeContext _context;

        public UserService(UPCodeContext context)
        {
            _context = context;
        }




        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            string Email = FixedText.FixedEmail(email);
            return _context.Users.Any(u => u.Email == Email);
        }
    }
}
