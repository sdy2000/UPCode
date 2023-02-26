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

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public User GetUserByEmail(string email)
        {
            string userEmail = FixedText.FixedEmail(email);
            return _context.Users.SingleOrDefault(u => u.Email == userEmail);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
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
                    bool isSendEmail = SendEmail.Send(user.Email, "Activation", body);

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

        public bool ActiveAccount(string activeCode)
        {
            User user = GetUserByActiveCode(activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GeneratorUniqCode();

            UpdateUser(user);
            bool isUpdate = SaveChange();

            return isUpdate;
        }

        public User LoginUser(LoginViewModel login)
        {
            string hachPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixedEmail(login.Email);

            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hachPassword);
        }

        public bool SendResetPasswordEmail(string email)
        {
            string fixedEmail = FixedText.FixedEmail(email);
            User user = GetUserByEmail(fixedEmail);

            #region SEND RESET PASSWORD EMAIL

            if (user!=null)
            {
                try
                {
                    string body = EmailBodyGenerator.SendResetPaswordEmail(user.UserName, user.ActiveCode);
                    bool isSendEmail = SendEmail.Send(user.Email, "Reset Password", body);

                    return isSendEmail;
                }
                catch
                {
                    return false;
                }
            }

            #endregion

            return false;
        }
    }
}
