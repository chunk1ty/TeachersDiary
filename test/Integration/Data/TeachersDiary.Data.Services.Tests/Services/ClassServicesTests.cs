using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;
using TeachersDiary.Clients.Mvc;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Extensions;
using TeachersDiary.Data.Ef.GenericRepository;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Encrypting;
using TeachersDiary.Services.Mapping;

namespace TeachersDiary.Data.Services.Tests.Services
{
    [TestFixture]
    public class ClassServicesTests
    {
        private static string User1Id = Guid.NewGuid().ToString();
        private static string User2Id = Guid.NewGuid().ToString();

        private static SchoolEntity school = new SchoolEntity()
        {
            Name = "School"
        };

        private static List<ClassEntity> classes = new List<ClassEntity>()
        {
            new ClassEntity()
            {
                Name = "1a",
                CreatedBy = User1Id
            },
            new ClassEntity()
            {
                Name = "2a",
                CreatedBy = User2Id
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

        [OneTimeSetUp]
        public void OnecSetUp()
        {
            AutoMapperConfig.RegisterAutomapper();
        }

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

        }

        [TearDown]
        public void TearDown()
        {
            //kernel = NinjectConfig.CreateKernel();

            // Entity Framework Optimistic Concurrency Patterns
            // https://msdn.microsoft.com/en-us/data/jj592904
            var dbContext = kernel.Get<TeachersDiaryDbContext>();

            dbContext.Schools.Attach(school);
            dbContext.Schools.Remove(school);

            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store 
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);

        }

        [Test]
        public async Task GetClassWithStudentsByClassIdAsync_WithValidClassId_ReturnClass()
        {
            // Arrange
            //var dbContext = new TeachersDiaryDbContext();
            //var entityframeworkRepository = new EntityFrameworkGenericRepository<ClassEntity>(dbContext);
            //var querySettings = new QuerySettings<ClassEntity>();
            //var mappingService = new MappingService();
            //var encryptingService = new EncryptingService();

            //var claaService = new ClassService(
            //    entityframeworkRepository,
            //    querySettings,
            //    dbContext,
            //    mappingService,
            //    encryptingService);

            var claaService = kernel.Get<IClassService>();

            var encoderId = kernel.Get<IEncryptingService>().EncodeId(classes[0].Id);

            // Act
            var @class = await claaService.GetClassWithStudentsByClassIdAsync(encoderId);

            // Assert
            Assert.IsInstanceOf<ClassDomain>(@class);
            Assert.AreEqual("1a", @class.Name);
            Assert.AreEqual("FirstName1", @class.Students[0].FirstName);
            Assert.AreEqual("LastName1", @class.Students[0].LastName);
            Assert.AreEqual(1, @class.Students[0].Absences[0].Excused);
            Assert.AreEqual(2, @class.Students[0].Absences[0].NotExcused);
        }

        [Test]
        public async Task GetAllAvailableClassesForUserAsync_WhenUserIdExist_ReturnAllClasses()
        {
            // Arrange
            //var dbContext = new TeachersDiaryDbContext();
            //var entityframeworkRepository = new EntityFrameworkGenericRepository<ClassEntity>(dbContext);
            //var querySettings = new QuerySettings<ClassEntity>();
            //var mappingService = new MappingService();
            //var encryptingService = new EncryptingService();

            //var claaService = new ClassService(
            //    entityframeworkRepository,
            //    querySettings,
            //    dbContext,
            //    mappingService,
            //    encryptingService);
            var claaService = kernel.Get<IClassService>();

            // Act
            var @class = await claaService.GetAllAvailableClassesForUserAsync(User2Id);
           
            Assert.IsInstanceOf<IEnumerable<ClassDomain>>(@class);
            Assert.AreEqual(1, @class.Count());
            Assert.AreEqual("2a", @class.ToList()[0].Name);
        }

        [Test]
        public void AddRange_WhenPassedCollectionIsValid_ShouldAddClasesToDb()
        {
            // Arrange
            var dbContext = kernel.Get<TeachersDiaryDbContext>();
            var entityframeworkRepository = new EntityFrameworkGenericRepository<ClassEntity>(dbContext);
            var querySettings = new QuerySettings<ClassEntity>();
            var mappingService = new MappingService();
            var encryptingService = new EncryptingService();

            var claaService = new ClassService(
                entityframeworkRepository,
                querySettings,
                dbContext,
                mappingService,
                encryptingService);

            //var claaService = kernel.Get<IClassService>();

            var classeCollection = new List<ClassDomain>()
            {
                new ClassDomain()
                {
                    Name = "2b",
                    SchoolId = school.Id
                },
                new ClassDomain()
                {
                    Name = "2v",
                    SchoolId = school.Id
                },
            };

            // Act
            claaService.AddRange(classeCollection);

            // Assert
            var classesInDb = dbContext.Classes.ToList();
           
            Assert.AreEqual(4, classesInDb.Count);
        }

        [Test]
        public async Task DeleteByIdAsync_WhenClassIdExist_ShouldRemoveClassFromDb()
        {
            // Arrange
            var dbContext = kernel.Get<TeachersDiaryDbContext>();

            var entityframeworkRepository = new EntityFrameworkGenericRepository<ClassEntity>(dbContext);
            var querySettings = new QuerySettings<ClassEntity>();
            var mappingService = new MappingService();
            var encryptingService = new EncryptingService();

            var claaService = new ClassService(
                entityframeworkRepository,
                querySettings,
                dbContext,
                mappingService,
                encryptingService);

            var decodedClassId = encryptingService.EncodeId(classes[0].Id);

            // Act
            await claaService.DeleteByIdAsync(decodedClassId);

            // Assert
            var classesInDb = dbContext.Classes.ToList();

            Assert.AreEqual(1, classesInDb.Count);
            Assert.AreEqual("2a", classesInDb[0].Name);
        }

        [Test]
        public async Task DeleteByIdAsync_WhenClassIdDoestExist_ShouldNotRemoveClassFromDb()
        {
            // Arrange
            var dbContext = new TeachersDiaryDbContext();
            var entityframeworkRepository = new EntityFrameworkGenericRepository<ClassEntity>(dbContext);
            var querySettings = new QuerySettings<ClassEntity>();
            var mappingService = new MappingService();
            var encryptingService = new EncryptingService();

            var claaService = new ClassService(
                entityframeworkRepository,
                querySettings,
                dbContext,
                mappingService,
                encryptingService);

            var decodedClassId = encryptingService.EncodeId(0);

            // Act
            await claaService.DeleteByIdAsync(decodedClassId);

            // Assert
            var classesInDb = dbContext.Classes.ToList();

            Assert.AreEqual(2, classesInDb.Count);
        }
    }
}
