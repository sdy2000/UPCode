using DataLayer.Entities.User;

namespace Core.Servises.Interfaces
{
    public interface IPermissionService
    {
        List<Role> GetAllRoles(string roleNameFilter = "");
    }
}
