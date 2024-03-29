﻿using DataLayer.Entities.Permissions;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Users
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }


        [Display(Name = "Role title")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [MaxLength(50, ErrorMessage = "{0} cannot be greater than {1}!")]
        public string RoleTitle { get; set; }


        [Display(Name = "Creation date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        [Display(Name = "Deleted")]
        public bool IsDelete { get; set; }





        #region RELATIONS

        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
