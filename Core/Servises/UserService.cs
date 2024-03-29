﻿using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Senders;
using Core.Servises.Interfaces;
using Datalayer.Entities.Wallets;
using DataLayer.Context;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users
                .SingleOrDefault(u => u.UserName == userName)
                .UserId;
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

        public bool IsExistActiveCode(string activeCode)
        {
            return _context.Users.Any(u => u.ActiveCode == activeCode);
        }

        public string UserImagePath(string folderName, string imgName)
        {
            string path = Path.Combine(
                 Directory.GetCurrentDirectory(),
                 "wwwroot/UserAvatar/" + folderName,
                 imgName);

            return path;
        }

        public string SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg")
        {


            if (img != null && img.IsImage() && !FileValidator.CheckIfExiclFile(img))
            {

                string normalPath = UserImagePath("NormalSize", imgName);
                string thumbPath = UserImagePath("ThumbSize", imgName);
                string iconPath = UserImagePath("IconSize", imgName);

                if (imgName != "No-Photo.jpg")
                {
                    if (File.Exists(normalPath))
                        File.Delete(normalPath);

                    if (File.Exists(thumbPath))
                        File.Delete(thumbPath);

                    if (File.Exists(iconPath))
                        File.Delete(iconPath);
                }

                imgName = new string
                (Path.GetFileNameWithoutExtension(img.FileName).Take(10).ToArray()).Replace(' ', '-') + "-" +
                NameGenerator.GeneratorUniqCode() + "-" +
                DateTime.Now.ToString("yyyymmssfff") + Path.GetExtension(img.FileName);

                normalPath = UserImagePath("NormalSize", imgName);
                thumbPath = UserImagePath("ThumbSize", imgName);
                iconPath = UserImagePath("IconSize", imgName);

                using (var stream = new FileStream(normalPath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }


                #region RESIZE IMAGE TO THUMB

                ImageConvertor imgResizeThumb = new ImageConvertor();

                imgResizeThumb.Image_resize(normalPath, thumbPath, 184);

                #endregion

                #region RESIZE IMAGE TO ICON

                ImageConvertor imgResize = new ImageConvertor();

                imgResize.Image_resize(normalPath, iconPath, 64);

                #endregion


                return imgName;
            }
            else if (imgName != "No-Photo.jpg")
            {
                return imgName;
            }
            else
            {
                return "No-Photo.jpg";
            }
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

            bool addUser = AddUser(user);


            #region SEND ACTIVATION EMAIL

            if (addUser)
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

            if (user != null)
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

        public bool ResetPassword(string activeCode, string password)
        {
            User user = GetUserByActiveCode(activeCode);


            string hashPassword = PasswordHelper.EncodePasswordMd5(password);
            user.Password = hashPassword;
            user.ActiveCode = NameGenerator.GeneratorUniqCode();

            UpdateUser(user);
            bool isSaveChenge = SaveChange();

            return isSaveChenge;
        }


        // // // // // // // // // UserPanel


        public InformationUserViewModel GetUserInformation(string userName)
        {
            User user = _context.Users.Where(u => u.UserName == userName)
                .Include(u => u.UserGender)
                .SingleOrDefault();

            InformationUserViewModel information = new InformationUserViewModel()
            {
                UserName = userName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegisterDate = user.RegisterDate.ToString("dd/MM/yyyy"),
                PhonNumber = user.PhonNumber,
                UesrGender = user.UserGender?.GenderTitle,
                Wallet = BalanceUserWallet(userName)
            };

            return information;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string userName)
        {
            return _context.Users
                .Where(u => u.UserName == userName)
                .Select(u => new SideBarUserPanelViewModel()
                {
                    UserName = u.UserName,
                    RegisterDate = u.RegisterDate.ToString("dd/MM/yyyy"),
                    UserAvatar = u.UserAvatar
                }).SingleOrDefault();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string userName)
        {
            return _context.Users
               .Where(u => u.UserName == userName)
               .Select(u => new EditProfileViewModel()
               {
                   Email = u.Email,
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   UserAvatar = u.UserAvatar,
                   PhonNumber = u.PhonNumber,
                   GenderId = u.GenderId
               }).Single();
        }

        public List<SelectListItem> GetGenderForEditUser()
        {
            return _context.UserGenders
                .Select(g => new SelectListItem()
                {
                    Text = g.GenderTitle,
                    Value = g.GenderId.ToString()
                })
                .ToList();
        }

        public EditProfileInfoViewModel EditProfile(EditProfileViewModel editProfile, string userName)
        {
            EditProfileInfoViewModel result = new EditProfileInfoViewModel();
            User user = GetUserByUserName(userName);

            user.FirstName = editProfile.FirstName;
            user.LastName = editProfile.LastName;
            user.PhonNumber = editProfile.PhonNumber;
            user.GenderId = (editProfile.GenderId != 0 && editProfile.GenderId <= 3) ? editProfile.GenderId : null;
            user.UserAvatar = SaveOrUpDateImg(editProfile.UserImage, editProfile.UserAvatar);

            string newEmial = FixedText.FixedEmail(editProfile.Email);
            if (user.Email != newEmial)
            {
                if (IsExistEmail(newEmial))
                {
                    result.IsEmailExist = true;
                    result.IsSuccess = false;

                    return result;
                }

                #region SEND ACTIVATION EMAIL

                try
                {
                    string body = EmailBodyGenerator.SendActiveEmail(user.UserName, user.ActiveCode);
                    result.IsSendActiveEmail = SendEmail.Send(newEmial, "Active Email", body);
                    user.IsActive = false;
                    user.Email = newEmial;
                }
                catch
                {
                    result.IsSendActiveEmail = false;
                    result.IsSuccess = false;

                    return result;
                }

                #endregion

            }

            UpdateUser(user);
            result.IsSuccess = SaveChange();

            return result;
        }

        public bool CompareOldPassword(string oldPassword, string userName)
        {
            string newPas = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(u => u.UserName == userName && u.Password == newPas);
        }

        public bool ChengPassword(string newPas, string userName)
        {
            var user = GetUserByUserName(userName);
            string hashpas = PasswordHelper.EncodePasswordMd5(newPas);

            user.Password = hashpas;

            if (UpdateUser(user))
                return SaveChange();

            return false;
        }


        // // // // // // // // // // // WALLET   

        public int AddWallet(Wallet wallet)
        {
            try
            {
                _context.Wallets.Add(wallet);
                SaveChange();
                return wallet.WalletId;
            }
            catch
            {
                return 0;
            }
        }
        public bool UpdateWallet(Wallet wallet)
        {
            try
            {
                _context.Wallets.Update(wallet);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _context.Wallets.Find(walletId);
        }

        public int BalanceUserWallet(string userName)
        {
            var userId = GetUserIdByUserName(userName);

            var enter = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay == true)
                .Select(w => w.Amount)
                .ToList();

            var exit = _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 2 && w.IsPay == true)
                .Select(w => w.Amount)
                .ToList();

            return (enter.Sum() - exit.Sum());
        }

        public List<WalletViewModel> GetWalletUser(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            return _context.Wallets
                .Where(w => w.UserId == userId)
                .Select(w => new WalletViewModel()
                {
                    Amount = w.Amount,
                    IsPay = w.IsPay,
                    Type = w.TypeId,
                    DateTime = w.CreateDate,
                    Description = w.Description
                })
                .ToList();
        }

        public int ChargeWallet(string userName, int amount, string description, bool isPay = false)
        {
            var wallet = new Wallet()
            {
                UserId = GetUserIdByUserName(userName),
                TypeId = 1,
                Amount = amount,
                Description = description,
                IsPay = isPay,
                CreateDate = DateTime.Now
            };

            int walletId = AddWallet(wallet);
            if (walletId != 0)
                SaveChange();

            return walletId;
        }


        // // // // // // // // // // //   USER PANEL   


        public UserForAdminViewModel GetUserForAdmin(int pageId = 1, string userNameFilter = "",
            string emailFilter = "", int genderId = 0, int take = 10)
        {
            IQueryable<User> result = _context.Users
                .Include(u => u.UserGender);

            if (!string.IsNullOrEmpty(userNameFilter))
            {
                result = result.Where(u => u.UserName.Contains(userNameFilter));
            }
            if (!string.IsNullOrEmpty(emailFilter))
            {
                result = result.Where(u => u.Email.Contains(FixedText.FixedEmail(emailFilter)));
            }
            if (genderId != 0)
            {
                result = result.Where(u => u.GenderId == genderId);
            }

            // SHOW ITEM IN PAGE
            int skip = (pageId - 1) * take;

            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.UserCount = result.Count();
            list.Users = result.ToList();

            return list;
        }

        public int AddUserFromAdmin(CreateUserForAdminViewModel user)
        {
            User addUser = new User();
            addUser.UserName = user.UserName;
            addUser.Email = FixedText.FixedEmail(user.Email);
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.FirstName = user.FirstName;
            addUser.LastName = user.LastName;
            addUser.PhonNumber = user.PhonNumber;
            addUser.ActiveCode = NameGenerator.GeneratorUniqCode();
            addUser.IsActive = (user.IsActive == 1) ? true : false;
            addUser.GenderId = (user.GenderId != 0 && user.GenderId <= 3) ? user.GenderId : null;
            addUser.UserAvatar = SaveOrUpDateImg(user.UserAvatar);


            if (AddUser(addUser))
                SaveChange();
            else
                return 0;


            if (user.AddWallet != 0)
            {
                ChargeWallet(user.UserName, user.AddWallet.Value, "Charging wallet by admin", true);
            }

            return addUser.UserId;
        }

        public EditUserForAdminViewModel GetUserForEditInAdmin(int userId)
        {
            return _context.Users
                 .Where(u => u.UserId == userId)
                 .Include(r => r.UserRoles)
                 .Select(u => new EditUserForAdminViewModel()
                 {
                     UserId = u.UserId,
                     UserName = u.UserName,
                     Email = u.Email,
                     FirstName = u.FirstName,
                     LastName = u.LastName,
                     PhonNumber = u.PhonNumber,
                     IsActive = (u.IsActive) ? 1 : 2,
                     GenderId = u.GenderId,
                     UserAvatar = u.UserAvatar,
                     UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()

                 })
                 .Single();
        }

        public int UpdateUserFromAdmin(EditUserForAdminViewModel user)
        {
            User updateUser = GetUserByUserId(user.UserId);
            updateUser.UserName = user.UserName;
            updateUser.Email = FixedText.FixedEmail(user.Email);
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.PhonNumber = user.PhonNumber;
            updateUser.IsActive = (user.IsActive == 1) ? true : false;
            updateUser.GenderId = (user.GenderId != 0 && user.GenderId <= 3) ? user.GenderId : null;
            updateUser.UserAvatar = SaveOrUpDateImg(user.NewUserAvatar, user.UserAvatar);
            if (user.Password != null)
            {
                updateUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            }


            if (UpdateUser(updateUser))
                SaveChange();
            else
                return 0;


            if (user.AddWallet != 0)
            {
                ChargeWallet(user.UserName, user.AddWallet.Value, "Charging wallet by admin", true);
            }

            return updateUser.UserId;
        }

        public DeleteUserForAdminViewModel GetUserForDelete(int userId)
        {
            return _context.Users
                 .Where(u => u.UserId == userId)
                 .Include(r => r.UserRoles)
                 .Include(g => g.UserGender)
                 .Select(u => new DeleteUserForAdminViewModel()
                 {
                     UserId = u.UserId,
                     UserName = u.UserName,
                     Email = u.Email,
                     FirstName = u.FirstName,
                     LastName = u.LastName,
                     PhonNumber = u.PhonNumber,
                     Gender = u.UserGender.GenderTitle,
                     UserAvatar = u.UserAvatar,
                     UserRoles = u.UserRoles.Select(r => r.RoleId).ToList(),
                     RegisterDate = u.RegisterDate

                 })
                 .Single();
        }

        public bool DeleteUser(int userId)
        {
            var user = GetUserByUserId(userId);

            user.IsDelete = true;
            user.IsActive = false;

            if (UpdateUser(user))
                return SaveChange();

            return false;
        }
    }
}
