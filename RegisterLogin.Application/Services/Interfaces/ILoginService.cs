using RegisterLogin.Domain.Entities;
using RegisterLogin.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterLogin.Application.Services.Interfaces
{
    public interface ILoginService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        void AddUser(RegisterViewModel register);
        Member? LoginMember(LoginViewModel login);
        Member? GetUserByEmail(string email);
        void UpdateUser(ResetPasswordViewModel reset);
        bool IsCorrectPassword(string email, string pass);
        void UpdatePassword(ChangePasswordViewModel change);
        ChangePasswordViewModel changePassword(string email);

    }
}
