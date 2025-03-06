using RegisterLogin.Application.Services.Interfaces;
using RegisterLogin.Domain.Common.Convertors;
using RegisterLogin.Domain.Common.Generator;
using RegisterLogin.Domain.Common.Security;
using RegisterLogin.Domain.Entities;
using RegisterLogin.Domain.ViewModels;
using RegisterLogin.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterLogin.Application.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;
        public LoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        public void AddUser(RegisterViewModel register)
        {
            var member = new Member()
            {
                UserName = register.UserName,
                Email = FixedText.FixedEmail(register.Email),
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                ActiveCode = NameGenerator.GenerateUniqCode(),
                IsActive = false,
                BrithDay = register.BrithDay
            };
            _repository.Add(member);
            _repository.Save();
        }

        public Member? GetUserByEmail(string email)
        {
            string x = FixedText.FixedEmail(email);
            var member = _repository.GetUserByEmail(x);
            if (member == null)
            {
                return null;
            }
            return member;
        }

        public bool IsCorrectPassword(string email, string pass)
        {
            string x = FixedText.FixedEmail(email);
            var member = _repository.GetUserByEmail(x);
            if (member == null)
            {
                return false;
            }
            var passToHash = PasswordHelper.EncodePasswordMd5(pass);
            return _repository.IsCorrectPassword(passToHash);
        }

        public bool IsExistEmail(string email)
        {
            return _repository.IsExistEmail(email);
        }

        public bool IsExistUserName(string userName)
        {
            return _repository.IsExistUserName(userName);
        }

        public Member? LoginMember(LoginViewModel login)
        {
            string email = FixedText.FixedEmail(login.Email);
            string pass = PasswordHelper.EncodePasswordMd5(login.Password);
            var member = _repository.GetUser(email, pass);
            if (member == null)
            {
                return null;
            }
            return member;
        }

        public void UpdatePassword(ChangePasswordViewModel change)
        {
            var member = GetUserByEmail(change.Email);
            member.Password = PasswordHelper.EncodePasswordMd5(change.NewPassword);
            _repository.UpdateUser(member);
            _repository.Save();
        }

        public void UpdateUser(ResetPasswordViewModel reset)
        {
            var user = GetUserByEmail(reset.Email);
            user.Password = PasswordHelper.EncodePasswordMd5(reset.Password);
            _repository.UpdateUser(user);
            _repository.Save();
        }
    }
}
