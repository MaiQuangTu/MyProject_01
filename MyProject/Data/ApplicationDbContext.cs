using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models.Dto;
using MyProject.Services.Base;

namespace MyProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext
    {
        private static bool _Created = false;

        public ApplicationDbContext()
        {
            if (!_Created)
            {
                _Created = true;
                Database.EnsureCreated();

            }
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Course> Course { get; set; }


        public override int SaveChanges()
        {
            var selectedEntityList = ChangeTracker.Entries()
                                    .Where(x => x.Entity is BaseEntity &&
                                    (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in selectedEntityList)
            {

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreateDate = DateTime.UtcNow.AddHours(1);
                }

                if (entity.State == EntityState.Modified)
                {
                    ((BaseEntity)entity.Entity).ModifyDate = DateTime.UtcNow.AddHours(1);
                }

                return base.SaveChanges();
            }

            return base.SaveChanges();

        }
    }
}
 