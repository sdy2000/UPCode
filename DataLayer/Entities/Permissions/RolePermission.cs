using DataLayer.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Permissions
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }





        #region RELATION

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        #endregion
    }
}
