﻿using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using TeachersDiary.Data.Entities;
using TeachersDiary.Services;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Domain
{
    public class ClassDomain : IMap<ClassEntity>, ICustomMappings
    {
        public ClassDomain()
        {
            Students = new List<StudentDomain>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int SchoolId { get; set; }

        public double TotalExcusedAbsences
        {
            get
            {
                return Students.Sum(x => x.TotalExcusedAbsences);
            }
        }

        public double TotalNotExcusedAbsences
        {
            get
            {
                return Students.Sum(x => x.TotalNotExcusedAbsences);
            }
        }

        public UserDomain ClassTeacher { get; set; }
        public string ClassTeacherId { get; set; }

        public IList<StudentDomain> Students { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            IEncryptingService encryptingService = new EncryptingService();

            configuration.CreateMap<ClassEntity, ClassDomain>()
                .ForMember(domain => domain.Id, x => x.MapFrom(entity => encryptingService.EncodeId(entity.Id)));

            configuration.CreateMap<ClassDomain, ClassEntity>()
                .ForMember(entity => entity.Id, x => x.MapFrom(domain => encryptingService.DecodeId(domain.Id)));
        }
    }
}
