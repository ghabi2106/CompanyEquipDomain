using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Domain.Entities;
using Domain.Entities.Identity;

namespace Domain.Context
{
    //public class DataContext : IdentityDbContext<User>
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext()
            : base("CompanyEquipConnection")
        {
            //Database.SetInitializer<DataContext>(new DatabaseInitialiser());
            //Database.SetInitializer<DataContext>(new MigrationInitialize());
        }

        #region User management entities
        //public virtual DbSet<Group> Groups { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        #endregion User management entities

        public virtual DbSet<Equipment> Equipments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User management
            modelBuilder.Entity<User>().ToTable("Users").Property(p => p.Id);
            modelBuilder.Entity<User>().ToTable("Users").Property(p => p.Id);
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserPermissions");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            //modelBuilder.Entity<IdentityRole>().ToTable("Permissions");
            #endregion User management
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

    }
}
