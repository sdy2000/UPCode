using Core.Servises.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Core.Servises
{
    public class PermissionService: IPermissionService
    {
        private UPCodeContext _context;

        public PermissionService(UPCodeContext context)
        {
            _context = context;
        }



        public bool SaveChange()
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


        // // // // // // // // // // // ROLE

        public void UpdateRole(Role role)
        {

            var updateRole = GetRoleById(role.RoleId);

            updateRole.RoleTitle = role.RoleTitle;
            updateRole.IsDelete = role.IsDelete;


            _context.Update(updateRole);
            SaveChange();
        }

        public int AddRole(Role role)
        {
            Role newRole = new Role()
            {
                RoleTitle = role.RoleTitle,
                IsDelete = false
            };
            _context.Roles.Add(newRole);
            SaveChange();

            return newRole.RoleId;
        }
        
        public bool IsExistRole(string roleTitle)
        {
            return _context.Roles.Any(r => r.RoleTitle == roleTitle);
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

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
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

            SaveChange();
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

        public List<int> GetPermissionRole(int roleId)
        {
            return _context.RolePermissions
                .Where(up => up.RoleId == roleId)
                .Select(up => up.PermissionId)
                .ToList();
        }

        public List<string> GetPermissionTitleRole(int roleId)
        {
            return _context.RolePermissions
                .Where(up => up.RoleId == roleId)
                .Include(p => p.Permission)
                .Select(p => p.Permission.PermissionTitle)
                .ToList();
        }

        public void AddPermissionToRole(List<int> permissionIds, int RoleId)
        {
            foreach (int per in permissionIds)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = per,
                    RoleId = RoleId
                });
            }

            SaveChange();
        }

        public void EditPermissionToRole(List<int> permissionIds, int RoleId)
        {
            _context.RolePermissions
                .Where(rp => rp.RoleId == RoleId)
                .ToList()
                .ForEach(rp => _context.RolePermissions.Remove(rp));

            AddPermissionToRole(permissionIds, RoleId);
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            if (userName == null)
                return false;

            int userId = _context.Users.SingleOrDefault(u => u.UserName == userName).UserId;

            List<int> userRoles = _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(r => r.RoleId)
                .ToList();

            if (!userRoles.Any())
                return false;

            List<int> rolePermission = _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Select(r => r.RoleId)
                .ToList();

            return rolePermission.Any(p => userRoles.Contains(p));
        }
    }
}
