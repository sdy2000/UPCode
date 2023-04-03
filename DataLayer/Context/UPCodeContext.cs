using Datalayer.Entities.Wallets;
using DataLayer.Entities.Courses;
using DataLayer.Entities.Orders;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<UserCourse> UserCourses { get; set; }

        #endregion

        #region WALLET

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion

        #region PERMISSION

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region COURSE

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
               .SelectMany(t => t.GetForeignKeys())
               .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<CourseEpisode>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Course>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Course>()
                .Property(c => c.SubGroupId)
                .IsRequired(false);


            modelBuilder.Entity<Course>()
             .HasOne<CourseGroup>(f => f.Group)
             .WithMany(g => g.Groups)
             .HasForeignKey(f => f.GroupId)
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Course>()
               .HasOne<CourseGroup>(f => f.SubGroup)
               .WithMany(g => g.SubGroups)
               .HasForeignKey(f => f.SubGroupId)
               .OnDelete(DeleteBehavior.Restrict);







            base.OnModelCreating(modelBuilder);
        }
    }
}
