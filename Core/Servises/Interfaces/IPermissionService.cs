using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;

namespace Core.Servises.Interfaces
{
    public interface IPermissionService
    {

        bool SaveChange();


        #region ROLE

        int AddRole(Role role);
        bool IsExistRole(string roleTitle);
        List<Role> GetAllRoles(string roleNameFilter = "");
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditUserRoles(List<int> roleIds, int userId);

        #endregion

        #region PERMISSION

        List<Permission> GetAllPermissions();
        void AddPermissionToRole(List<int> permissionIds, int RoleId);

        #endregion
    }
}
