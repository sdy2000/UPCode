using Core.Servises.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.User;

namespace Core.Servises
{
    public class PermissionService: IPermissionService
    {
        private UPCodeContext _context;

        public PermissionService(UPCodeContext context)
        {
            _context = context;
        }



        public List<Role> GetAllRoles(string roleNameFilter = "")
        {
            IQueryable<Role> result = _context.Roles;

            if (!string.IsNullOrEmpty(roleNameFilter))
            {
                result = result.Where(r => r.RoleTitle == roleNameFilter);
            }

            return result.ToList();
        }
    }
}
