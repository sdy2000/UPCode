using Core.DTOs;
using DataLayer.Entities.User;
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
        string imgPath(string folderName, string imgName);
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

        int BalanceUserWallet(string userName);

        #endregion
    }
}
