using RegisterLogin.Domain.Entities;
using RegisterLogin.Infrastructure.Context;
using RegisterLogin.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterLogin.Infrastructure.Repositories.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MyContext _context;

        public LoginRepository(MyContext context)
        {
            _context = context;
        }

        public void Add(Member member)
        {
            _context.Members.Add(member);
        }

        public Member? GetUser(string email, string pass)
        {
            return _context.Members.FirstOrDefault(x => x.Email == email && x.Password == pass);
        }

        public Member? GetUserByEmail(string email)
        {
            return _context.Members.FirstOrDefault(x => x.Email == email);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Members.Any(m => m.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Members.Any(x => x.UserName == userName);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(Member member)
        {
            _context.Members.Update(member);
        }
    }
}
