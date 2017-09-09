using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef
{
    public class TeachersDiaryDbContext : IdentityDbContext<UserEntity>, ITeachersDiaryDbContext, IUnitOfWork
    {
        public TeachersDiaryDbContext()
            : base("DefaultConnection", false)
        {
        }

        public virtual IDbSet<SchoolEntity> Schools { get; set; }

        public virtual IDbSet<ClassEntity> Classes { get; set; }

        public virtual IDbSet<StudentEntity> Students { get; set; }

        public virtual IDbSet<AbsenceEntity> Absences { get; set; }

        public static TeachersDiaryDbContext Create()
        {
            return new TeachersDiaryDbContext();
        }

        public new IDbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().ToTable("Users");

            modelBuilder.Entity<UserEntity>()
                .Ignore(x => x.EmailConfirmed)
                .Ignore(x => x.PhoneNumber)
                .Ignore(x => x.PhoneNumberConfirmed)
                .Ignore(x => x.TwoFactorEnabled);
                //.Ignore(x => x.LockoutEndDateUtc)
                //.Ignore(x => x.LockoutEnabled)
                //.Ignore(x => x.AccessFailedCount)
                //.Ignore(x => x.UserName);
                

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }

        public int GetHash
        {
            get { return base.GetHashCode(); }
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public List<TeachersDiaryRole> GetRoles()
        {
            var roles = this.Roles.Select(x => new TeachersDiaryRole()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return roles;
        }
    }

    public class TeachersDiaryRole
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}