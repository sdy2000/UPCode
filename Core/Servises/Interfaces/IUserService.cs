using Core.DTOs;
using DataLayer.Entities.User;

namespace Core.Servises.Interfaces
{
    public interface IUserService
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        User GetUserByUserId(int userId);
        User GetUserByUserName(string userName);
        User GetUsreByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool SaveChange();


        #region ACCOUNT

        IsRegisterViewModel RegisterUser(RegisterViewModel register);
        bool ActiveAccount(string activeCode);
        User LoginUser(LoginViewModel login);


        #endregion
    }
}
