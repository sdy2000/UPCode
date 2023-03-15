using Datalayer.Entities.Wallets;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class UPCodeContext:DbContext
    {
        public UPCodeContext(DbContextOptions<UPCodeContext> options)
            :base(options)
        {

        }

        #region USER

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserGender> UserGenders { get; set; }

        #endregion

        #region WALLET

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion

        #region PERMISSION

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);








            base.OnModelCreating(modelBuilder);
        }
    }
}
