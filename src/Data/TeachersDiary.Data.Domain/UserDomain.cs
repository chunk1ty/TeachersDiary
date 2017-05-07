using System;
using System.Collections.Generic;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Domain
{
    //TODO Check which properties i need
    public class UserDomain : IMapFrom<UserEntity>, IMapTo<UserEntity>
    {
        public virtual string Email { get; set; }
      
        public virtual bool EmailConfirmed { get; set; }
       
        public virtual string PasswordHash { get; set; }
      
        public virtual string SecurityStamp { get; set; }
       
        public virtual string PhoneNumber { get; set; }
       
        public virtual bool PhoneNumberConfirmed { get; set; }
      
        public virtual bool TwoFactorEnabled { get; set; }
        
        public virtual DateTime? LockoutEndDateUtc { get; set; }
      
        public virtual bool LockoutEnabled { get; set; }
      
        public virtual int AccessFailedCount { get; set; }
        
        public virtual string Id { get; set; }
       
        public virtual string UserName { get; set; }
    }
}
