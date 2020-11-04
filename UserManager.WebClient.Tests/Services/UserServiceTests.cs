using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.DAL;
using UserManager.DAL.Enum;
using UserManager.DAL.Models;
using UserManager.WebClient.Services;
using Xunit;

namespace UserManager.WebClient.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserManagerContext _context;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            //var myConfiguration = new Dictionary<string, string>
            //{
            //    {"SchemaName", "TestSchema"}
            //};

            //var configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(myConfiguration)
            //    .Build();

            _context = new UserManagerContext(new DbContextOptionsBuilder<UserManagerContext>()
                .UseSqlite("Filename=:memory:")
                .UseSnakeCaseNamingConvention()
                .Options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            _userService = new UserService(_context);
        }


        [Fact]
        public async Task GetUserInfo_WhenGot_ListOfAllUsersExpected()
        {
            // Arrange

            // Act
            var result = await _userService.GetUsersInfo();

            // Assert
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task CreateUSer_WhenDone_AddedUserExpected()
        {
            // Arrange
            var initUser = InitUser();

            // Act
            var result = await _userService.CreateUser(initUser);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateUser_WhenIdAlreadyExist_NullExpected()
        {
            // Arrange
            var initUser = InitUser();
            await _context.Users.AddAsync(initUser);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userService.CreateUser(initUser);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RemoveUser_WhenIdNotExist_FalseExpected()
        {
            // Arrange
            var id = 5;

            // Act
            var result = await _userService.RemoveUser(id);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveUser_WhenDone_TrueExpected()
        {
            // Arrange
            var initUser = InitUser();
            await _context.Users.AddAsync(initUser);
            var id =await _context.SaveChangesAsync();

            // Act
            var result = await _userService.RemoveUser(id);
            var userInDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.True(result);
            Assert.Null(userInDb);
        }

        [Fact]
        public async Task SetStatus_WhenNewStatusAssign_UpdatedUserExpected()
        {
            // Arrange
            var initUser = InitUser();
            await _context.Users.AddAsync(initUser);
            var id = await _context.SaveChangesAsync();

            // Act
            var result = await _userService.SetStatus(id, UserStatus.Blocked.ToString());
            var userInDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.NotNull(result);
            Assert.True(userInDb.UserStatus == UserStatus.Blocked);
        }

        [Fact]
        public async Task SetStatus_WhenNewStatusNotFromEnum_UserExpected()
        {
            // Arrange
            var initUser = InitUser();
            await _context.Users.AddAsync(initUser);
            var id = await _context.SaveChangesAsync();
            var userBefore = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            // Act
            var result = await _userService.SetStatus(id, "Unknown");
            var userInDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.Null(result);
            Assert.NotNull(userBefore);
            Assert.Equal(userBefore.UserStatus, userInDb.UserStatus);
        }

        private User InitUser()
        {
           return new User()
           {
               UserName = "test",
               UserStatus = UserStatus.Active
           };
        }
    }
}
