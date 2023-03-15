using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;

namespace Core.Servises.Interfaces
{
    public interface IPermissionService
    {
        bool SaveChenge();


        #region ROLE

        List<Role> GetAllRoles(string roleNameFilter = "");
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditUserRoles(List<int> roleIds, int userId);

        #endregion

        #region PERMISSION

        List<Permission> GetAllPermissions();

        #endregion
    }
}
