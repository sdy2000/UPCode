using Core.Servises.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Permissions;
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



        public bool SaveChenge()
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

        public List<Role> GetAllRoles(string roleNameFilter = "")
        {
            IQueryable<Role> result = _context.Roles;

            if (!string.IsNullOrEmpty(roleNameFilter))
            {
                result = result.Where(r => r.RoleTitle == roleNameFilter);
            }

            return result.ToList();
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int role in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = role,
                    UserId = userId
                });
            }

            SaveChenge();
        }
        public void EditUserRoles(List<int> roleIds, int userId)
        {
            _context.UserRoles
                .Where(r => r.UserId == userId)
                .ToList()
                .ForEach(r => _context.UserRoles.Remove(r));


            AddRolesToUser(roleIds, userId);
        }



        // // // // // // // // // // // PERMISSION


        public List<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }
    }
}
