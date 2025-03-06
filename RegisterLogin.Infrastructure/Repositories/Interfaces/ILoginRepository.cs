using RegisterLogin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterLogin.Infrastructure.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        void Add(Member member);
        void Save();
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        Member? GetUser(string email, string pass);
        Member? GetUserByEmail(string email);
        void UpdateUser(Member member);
        bool IsCorrectPassword(string pass);
    }
}
