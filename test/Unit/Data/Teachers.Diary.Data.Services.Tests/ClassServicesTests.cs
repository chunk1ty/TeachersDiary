//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Moq;
//using NUnit.Framework;
//using TeachersDiary.Data.Ef.Contracts;
//using TeachersDiary.Data.Entities;
//using TeachersDiary.Data.Services;
//using TeachersDiary.Domain;
//using TeachersDiary.Services.Contracts;
//using TeachersDiary.Services.Contracts.Mapping;

//namespace Teachers.Diary.Data.Services.Tests
//{
//    [TestFixture()]
//    public class ClassServicesTests
//    {
//        private Mock<IEntityFrameworkGenericRepository<ClassEntity>> _mockedEntityFrameworkRepository;
//        private Mock<IQuerySettings<ClassEntity>> _mockedQuerySettings;
//        private Mock<IUnitOfWork> _mockedUnitOfWorkd;
//        private Mock<IMappingService> _mockedMapperService;
//        private Mock<IEncryptingService> _mockedEncryptingService;

//        [SetUp]
//        public void Setup()
//        {
//            _mockedEntityFrameworkRepository = new Mock<IEntityFrameworkGenericRepository<ClassEntity>>();
//            _mockedQuerySettings = new Mock<IQuerySettings<ClassEntity>>();
//            _mockedUnitOfWorkd = new Mock<IUnitOfWork>();
//            _mockedMapperService = new Mock<IMappingService>();
//            _mockedEncryptingService = new Mock<IEncryptingService>();
//        }

//        [Test]
//        public void Constructor_WhenEntityFrameworkRepositoryIsNull_ThrowsArgumentNullException()
//        {
//            Assert.That(() => new ClassService(
//                null, 
//                _mockedQuerySettings.Object, 
//                _mockedUnitOfWorkd.Object, 
//                _mockedMapperService.Object, 
//                _mockedEncryptingService.Object),
//                Throws.TypeOf<ArgumentNullException>()
//                    .With.Message.Contain("entityFrameworkGenericRepository"));
//        }

//        [Test]
//        public void Constructor_WhenQuerySettingsIsNull_ThrowsArgumentNullException()
//        {
//            Assert.That(() => new ClassService(
//                    _mockedEntityFrameworkRepository.Object,
//                    null,
//                    _mockedUnitOfWorkd.Object,
//                    _mockedMapperService.Object,
//                    _mockedEncryptingService.Object),
//                Throws.TypeOf<ArgumentNullException>()
//                    .With.Message.Contain("querySettings"));
//        }

//        [Test]
//        public void Constructor_WhenUnitOfWorkIsNull_ThrowsArgumentNullException()
//        {
//            Assert.That(() => new ClassService(
//                    _mockedEntityFrameworkRepository.Object,
//                    _mockedQuerySettings.Object,
//                    null,
//                    _mockedMapperService.Object,
//                    _mockedEncryptingService.Object),
//                Throws.TypeOf<ArgumentNullException>()
//                    .With.Message.Contain("unitOfWork"));
//        }

//        [Test]
//        public void Constructor_WhenMapperServiceIsNull_ThrowsArgumentNullException()
//        {
//            Assert.That(() => new ClassService(
//                    _mockedEntityFrameworkRepository.Object,
//                    _mockedQuerySettings.Object,
//                    _mockedUnitOfWorkd.Object,
//                    null,
//                    _mockedEncryptingService.Object),
//                Throws.TypeOf<ArgumentNullException>()
//                    .With.Message.Contain("mappingService"));
//        }

//        [Test]
//        public void Constructor_WhenEncryptingServiceIsNull_ThrowsArgumentNullException()
//        {
//            Assert.That(() => new ClassService(
//                    _mockedEntityFrameworkRepository.Object,
//                    _mockedQuerySettings.Object,
//                    _mockedUnitOfWorkd.Object,
//                    _mockedMapperService.Object,
//                    null),
//                Throws.TypeOf<ArgumentNullException>()
//                    .With.Message.Contain("encryptingService"));
//        }

//        [Test]
//        public void GetClassWithStudentsByClassIdAsync_WhenClassIdIsNull_ThrowsArgumentNullException()
//        {
//            var classService = new ClassService(
//                _mockedEntityFrameworkRepository.Object,
//                _mockedQuerySettings.Object,
//                _mockedUnitOfWorkd.Object,
//                _mockedMapperService.Object,
//                _mockedEncryptingService.Object);
            

//            Assert.That(async () => await classService.GetClassByClassIdAsync(null),
//                Throws.TypeOf<ArgumentNullException>()
//                    .With.Message.Contain("classId"));
//        }

//        [Test]
//        public async Task GetClassWithStudentsByClassIdAsync_WhenClassIdIsNotFound_ThrowsClassNotFoundException()
//        {
//            _mockedEncryptingService.Setup(x => x.DecodeId(It.IsAny<string>()))
//                .Returns(1);

//            _mockedEntityFrameworkRepository.Setup(x => x.GetAllAsync(It.IsAny<IQuerySettings<ClassEntity>>()))
//                .ReturnsAsync(new List<ClassEntity>());

//            var classService = new ClassService(
//                _mockedEntityFrameworkRepository.Object,
//                _mockedQuerySettings.Object,
//                _mockedUnitOfWorkd.Object,
//                _mockedMapperService.Object,
//                _mockedEncryptingService.Object);

//            var result =  await classService.GetClassByClassIdAsync("1");

//            Assert.IsInstanceOf<ClassDomain>(result);
//            Assert.IsNull(result.Name);
//            Assert.IsNull(result.Id);
//        }

//        //[Test]
//        //public void GetAllAvailableClassesForUserAsync_WhenUserIdIsNull_ThrowsArgumentNullException()
//        //{
//        //    var classService = new ClassService(
//        //        _mockedEntityFrameworkRepository.Object,
//        //        _mockedQuerySettings.Object,
//        //        _mockedUnitOfWorkd.Object,
//        //        _mockedMapperService.Object,
//        //        _mockedEncryptingService.Object);


//        //    Assert.That(async () => await classService.GetClassesBySchoolIdAsync(1),
//        //        Throws.TypeOf<ArgumentNullException>()
//        //            .With.Message.Contain("userId"));
//        //}

//        [Test]
//        public void AddRange_WhenCollectionIsNull_ThrowsArgumentNullException()
//        {
//            var classService = new ClassService(
//                _mockedEntityFrameworkRepository.Object,
//                _mockedQuerySettings.Object,
//                _mockedUnitOfWorkd.Object,
//                _mockedMapperService.Object,
//                _mockedEncryptingService.Object);


//            Assert.That(() => classService.AddRange(null), Throws.TypeOf<ArgumentNullException>()
//                    .With.Message.Contain("classDomains"));
//        }
//    }
//}
