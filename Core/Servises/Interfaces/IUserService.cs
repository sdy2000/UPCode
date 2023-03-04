using Core.DTOs;
using DataLayer.Entities.User;
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
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool IsExistActiveCode(string activeCode);
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
        #endregion
    }
}
