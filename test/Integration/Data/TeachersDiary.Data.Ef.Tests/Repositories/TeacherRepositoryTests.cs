using System.Linq;
using NUnit.Framework;
using TeachersDiary.Data.Ef.Repositories;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Tests.Repositories
{
    //[TestFixture]
    //public class TeacherRepositoryTests
    //{
    //    //  [TestInitialize] in MSTest 
    //    [SetUp]
    //    public void SetUp()
    //    {

    //    }

    //    //  [TestCleanup] in MSTest
    //    [TearDown]
    //    public void TearDown()
    //    {
    //        var teachersDiaryContext = new TeachersDiaryDbContext();

    //        var teacher = teachersDiaryContext.Teachers.FirstOrDefault(x => x.UserId == "1234");

    //        var school = teachersDiaryContext.Schools.FirstOrDefault(x => x.Name == "School");

    //        teachersDiaryContext.Teachers.Remove(teacher);
    //        teachersDiaryContext.Schools.Remove(school);

    //        teachersDiaryContext.SaveChanges();
    //    }

    //    [Test]
    //    public void Add_WithValidTeacher_ShouldAddTeacherToDb()
    //    {
    //        // Arrange
    //        var teachersDiaryContext = new TeachersDiaryDbContext();

    //        var teacherRepository = new TeacherRepository(teachersDiaryContext);

    //        var teacher = new TeacherEntity()
    //        {
    //            School = new SchoolEntity()
    //            {
    //                Name = "School"
    //            },
    //            FirstName = "FirstName",
    //            LastName = "LastName",
    //            UserId = "1234"
    //        };

    //        // Act
    //        teacherRepository.Add(teacher);

    //        teachersDiaryContext.SaveChanges();

    //        // Assert
    //        var teacherDbEntity = teachersDiaryContext.Teachers.Where(x => x.UserId == "1234");
            
    //        Assert.AreEqual(1, teacherDbEntity.ToList().Count);
    //        Assert.AreEqual(teacher.SchoolId, teacherDbEntity.ToList()[0].SchoolId);
    //        Assert.AreEqual(teacher.FirstName, teacherDbEntity.ToList()[0].FirstName);
    //        Assert.AreEqual(teacher.LastName, teacherDbEntity.ToList()[0].LastName);
    //        Assert.AreEqual(teacher.UserId, teacherDbEntity.ToList()[0].UserId);

    //    }
    //}
}
