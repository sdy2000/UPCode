using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Senders;
using Core.Servises.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.User;
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


        public bool AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);

                return true;
            }
            catch
            {
                return false;
            }
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
        public bool SaveChange()
        {
            try
            {
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }




        // // // // // // // // Account

        public IsRegisterViewModel RegisterUser(RegisterViewModel register)
        {
            IsRegisterViewModel result = new IsRegisterViewModel();

            #region VALIDATION

            if (register == null)
            {
                result.IsSuccess = false;

                return result;
            }
            if (IsExistUserName(register.UserName))
            {
                result.IsExistUserName = true;
                result.IsSuccess = false;

                return result;
            }
            if (IsExistEmail(register.Email))
            {
                result.IsExistEmail = true;
                result.IsSuccess = false;

                return result;

            }

            #endregion

            var user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixedEmail(register.Email),
                ActiveCode = NameGenerator.GeneratorUniqCode(),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                UserAvatar = "No-Photo.jpg"
            };

            bool addUser =  AddUser(user);


            #region SEND ACTIVATION EMAIL

            if(addUser)
            {
                try
                {
                    string body = EmailBodyGenerator.SendActiveEmail(user.UserName, user.ActiveCode);
                    bool isSendEmail = SendEmail.Send(user.Email, "فعال سازی", body);

                    result.IsSendActiovationEmail = true;
                }
                catch
                {
                    result.IsSendActiovationEmail = false;
                    result.IsSuccess = false;

                    return result;
                }
            }

            #endregion


            result.IsSuccess = SaveChange();

            return result;
        }

    }
}
