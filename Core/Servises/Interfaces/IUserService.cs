﻿using Core.DTOs;
using Datalayer.Entities.Wallets;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Servises.Interfaces
{
    public interface IUserService
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        User GetUserByUserId(int userId);
        User GetUserByUserName(string userName);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        int GetUserIdByUserName(string userName);
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool IsExistActiveCode(string activeCode);
        string UserImagePath(string folderName, string imgName);
        string SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg");
        bool SaveChange();


        #region ACCOUNT

        IsRegisterViewModel RegisterUser(RegisterViewModel register);
        bool ActiveAccount(string activeCode);
        User LoginUser(LoginViewModel login);
        bool SendResetPasswordEmail(string email);
        bool ResetPassword(string activeCode,string password);

        #endregion

        #region USE PANEL

        InformationUserViewModel GetUserInformation(string userName);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string userName);
        EditProfileViewModel GetDataForEditProfileUser(string userName);
        List<SelectListItem> GetGenderForEditUser();
        EditProfileInfoViewModel EditProfile(EditProfileViewModel editProfile,string userName);
        bool CompareOldPassword(string oldPassword, string userName);
        bool ChengPassword(string newPas, string userName);
        #endregion

        #region WALLET

        int AddWallet(Wallet wallet);
        bool UpdateWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int walletId);
        int BalanceUserWallet(string userName);
        List<WalletViewModel> GetWalletUser(string userName);
        int ChargeWallet(string userName, int amount, string description, bool isPay = false);

        #endregion

        #region ADMIN PANEL

        UserForAdminViewModel GetUserForAdmin(int pageId = 1, string userNameFilter = "",
            string emailFilter = "", int genderId = 0, int take = 10);
        int AddUserFromAdmin(CreateUserForAdminViewModel user);
        EditUserForAdminViewModel GetUserForEditInAdmin(int userId);
        int UpdateUserFromAdmin(EditUserForAdminViewModel user);
        DeleteUserForAdminViewModel GetUserForDelete(int userId);
        bool DeleteUser(int userId);

        #endregion
    }
}
