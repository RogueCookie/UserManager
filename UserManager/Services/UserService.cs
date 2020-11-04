using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManager.DAL;
using UserManager.DAL.Enum;
using UserManager.DAL.Models;

namespace UserManager.WebClient.Services
{
    public class UserService
    {
        private readonly UserManagerContext _context;

        public UserService(UserManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Get all information about user
        /// </summary>
        /// <returns>List of exist users</returns>
        public Task<List<User>> GetUsersInfo()
        {
            return _context.Users.ToListAsync();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="model">New user model</param>
        /// <returns>User model which was added</returns>
        public async Task<User> CreateUser(User model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (existUser != null) return null; //TODO error model

            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();

            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
            return result;
        }

        /// <summary>
        /// Remove user by Id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Whether user removed or not</returns>
        public async Task<bool> RemoveUser(int userId)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (existUser == null) return false;

            _context.Remove(existUser);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Update user status
        /// </summary>
        /// <param name="userId">Id of user which need to update</param>
        /// <param name="status">New status</param>
        /// <returns>Updated user model by status</returns>
        public async Task<User> SetStatus(int userId, string status)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (existUser == null) return null; //TODO
            var currentStatus = existUser.UserStatus.ToString();
            
            var newStatusIsEnum = Enum.IsDefined(typeof(UserStatus), status);//TODO check
            if (!newStatusIsEnum) return null;

            if (currentStatus == status) return existUser;

            var newStatus = (UserStatus) Enum.Parse(typeof(UserStatus), status);
            existUser.UserStatus = newStatus;

            _context.Users.Update(existUser);
            await _context.SaveChangesAsync();

            var updatedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return updatedUser;
        }
    }
}