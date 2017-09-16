﻿using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptingService _encryptingService;
        private readonly IEntityFrameworkGenericRepository<AbsenceEntity> _entityFrameworkGenericRepository;

        public AbsenceService(
            IEncryptingService encryptingService, 
            IUnitOfWork unitOfWork, 
            IEntityFrameworkGenericRepository<AbsenceEntity> entityFrameworkGenericRepository)
        {
            _encryptingService = encryptingService;
            _unitOfWork = unitOfWork;
            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
        }

        public void CalculateStudentsAbsencesForLastMonth(List<StudentDomain> students)
        {
            Guard.WhenArgument(students, nameof(students)).IsNull().Throw();

            var absences = new List<AbsenceEntity>();

            var previousMonthId = DateTime.UtcNow.Month - 1;
            var twoMonthAgoId = previousMonthId - 1;

            // first time calculate
            if (students.FirstOrDefault().Absences.Count == twoMonthAgoId)
            {
                foreach (var student in students)
                {
                    var totalExcusedAbsences = student.Absences.Sum(x => x.Excused);
                    var totalNotExcusedAbsences = student.Absences.Sum(x => x.NotExcused);

                    var studentId = _encryptingService.DecodeId(student.Id);

                    var absence = new AbsenceEntity()
                    {
                        Excused = student.EnteredTotalExcusedAbsences - totalExcusedAbsences,
                        NotExcused = student.EnteredTotalNotExcusedAbsences - totalNotExcusedAbsences,
                        StudentId = studentId,
                        MonthId = previousMonthId
                    };

                    absences.Add(absence);
                }

                _entityFrameworkGenericRepository.AddRange(absences);
            }
            else
            {
                foreach (var student in students)
                {
                    var totalExcusedAbsences = student.Absences.Take(twoMonthAgoId).Sum(x => x.Excused);
                    var totalNotExcusedAbsences = student.Absences.Take(twoMonthAgoId).Sum(x => x.NotExcused);

                    var studentId = _encryptingService.DecodeId(student.Id);
                    var absenseId = _encryptingService.DecodeId(student.Absences.LastOrDefault().EncodedId);

                    var absence = new AbsenceEntity()
                    {
                        Id = absenseId,
                        Excused = student.EnteredTotalExcusedAbsences - totalExcusedAbsences,
                        NotExcused = student.EnteredTotalNotExcusedAbsences - totalNotExcusedAbsences,
                        StudentId = studentId,
                        MonthId = previousMonthId
                    };

                    absences.Add(absence);
                    _entityFrameworkGenericRepository.Update(absence);
                }

               
            }

            _unitOfWork.Commit();
        }
    }
}