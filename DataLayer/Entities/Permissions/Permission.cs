using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Permissions
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }


        [Required]
        [MaxLength(50)]
        public string PermissionTitle { get; set; }


        public int? ParentId { get; set; }




        #region RELATION

        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
