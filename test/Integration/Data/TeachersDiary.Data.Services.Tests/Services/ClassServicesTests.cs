using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;
using TeachersDiary.Clients.Mvc;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Extensions;
using TeachersDiary.Data.Ef.GenericRepository;
using TeachersDiary.Data.Entities;
using TeachersDiary.Domain;
using TeachersDiary.Services.Encrypting;
using TeachersDiary.Services.Mapping;

namespace TeachersDiary.Data.Services.Tests.Services
{
    [TestFixture]
    public class ClassServicesTests
    {
        private static SchoolEntity school = new SchoolEntity()
        {
            Name = "School"
        };

        private static List<ClassEntity> classes = new List<ClassEntity>()
        {
            new ClassEntity()
            {
                Name = "1a",
            },
            new ClassEntity()
            {
                Name = "2a"
            }
        };

        private static List<StudentEntity> students = new List<StudentEntity>()
        {
            new StudentEntity()
            {
                FirstName = "FirstName1",
                LastName = "LastName1"
            },
            new StudentEntity()
            {
                FirstName = "FirstName2",
                LastName = "LastName2"
            }
        };

        private static List<AbsenceEntity> absences = new List<AbsenceEntity>
        {
            new AbsenceEntity()
            {
                Excused = 1,
                NotExcused = 2
            },
            new AbsenceEntity()
            {
                Excused = 11,
                NotExcused = 22
            },
        };

        private static IKernel kernel;

        [SetUp]
        public void SetUp()
        {
            kernel = NinjectConfig.CreateKernel();

            var dbContext = kernel.Get<TeachersDiaryDbContext>();

            dbContext.Schools.Add(school);
            dbContext.SaveChanges();

            classes[0].SchoolId = school.Id;
            classes[1].SchoolId = school.Id;

            dbContext.Classes.AddRange(classes);
            dbContext.SaveChanges();

            students[0].ClassId = classes[0].Id;
            students[1].ClassId = classes[1].Id;

            dbContext.Students.AddRange(students);
            dbContext.SaveChanges();

            absences[0].StudentId = students[0].Id;
            absences[1].StudentId = students[1].Id;

            dbContext.Absences.AddRange(absences);
            dbContext.SaveChanges();

            AutoMapperConfig.RegisterAutomapper();
        }

        [TearDown]
        public void TearDown()
        {
            kernel = NinjectConfig.CreateKernel();

            var dbContext = kernel.Get<TeachersDiaryDbContext>();

            dbContext.Schools.Attach(school);
            dbContext.Schools.Remove(school);

            dbContext.SaveChanges();
        }

        [Test]
        public async Task GetClassWithStudentsByClassIdAsync_WithValidClassId_ReturnClass()
        {
            // Arrange
            var teachersDiaryContext = new TeachersDiaryDbContext();

            var entityframeworkRepository = new EntityFrameworkGenericRepository<ClassEntity>(teachersDiaryContext);
            var querySettings = new QuerySettings<ClassEntity>();
            var mappingService = new MappingService();
            var encryptingService = new EncryptingService();

            var claaService = new ClassService(
                entityframeworkRepository,
                querySettings,
                teachersDiaryContext,
                mappingService,
                encryptingService);

            var encoderId =  encryptingService.EncodeId(classes[0].Id);

            // Act
            var @class = await claaService.GetClassWithStudentsByClassIdAsync(encoderId);

            Assert.IsInstanceOf<ClassDomain>(@class);
            Assert.AreEqual("1a", @class.Name);
            Assert.AreEqual("FirstName1", @class.Students[0].FirstName);
            Assert.AreEqual("LastName1", @class.Students[0].LastName);
            Assert.AreEqual(1, @class.Students[0].Absences[0].Excused);
            Assert.AreEqual(2, @class.Students[0].Absences[0].NotExcused);
        }

        [Test]
        public async Task GetClassWithStudentsByClassIdAsync_WithInValidClassId_ReturnNull()
        {
            // Arrange
            var teachersDiaryContext = new TeachersDiaryDbContext();

            var entityframeworkRepository = new EntityFrameworkGenericRepository<ClassEntity>(teachersDiaryContext);
            var querySettings = new QuerySettings<ClassEntity>();
            var mappingService = new MappingService();
            var encryptingService = new EncryptingService();

            var claaService = new ClassService(
                entityframeworkRepository,
                querySettings,
                teachersDiaryContext,
                mappingService,
                encryptingService);

            var encoderId = encryptingService.EncodeId(0);

            // Act
            var @class = await claaService.GetClassWithStudentsByClassIdAsync(encoderId);

            Assert.IsInstanceOf<ClassDomain>(@class);
            Assert.IsNull(@class.Name);
            Assert.IsNull(@class.EncodedId);
        }
    }
}
