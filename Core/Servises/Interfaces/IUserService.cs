using Core.DTOs;
using DataLayer.Entities.User;

namespace Core.Servises.Interfaces
{
    public interface IUserService
    {
        bool AddUser(User user);
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool SaveChange();


        #region ACCOUNT

        IsRegisterViewModel RegisterUser(RegisterViewModel register);

        #endregion
    }
}
