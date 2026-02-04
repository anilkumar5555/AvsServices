using AVSModels.Models;
using Common.DBContext;
using Common.Helper;
using Common.Repository.Implementation;
using Common.Repository.Interface;
using Common.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUser _user;
        public UserService()
        {
            _user = new UserRepository();
        }
        public async Task<List<UserModel>> getallusers()
        {
            try
            {
                return await _user.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        public async Task<UserModel> GetUser(string username, string password)
        {
            return await _user.GetUserByPassword(username, Utility.Encrypt(password));
        }

        public async Task<UserModel> GetuserByPhoneNumber(string phonenumber)
        {
            var result = await _user.GetUserByPhoneNumber(phonenumber);
            return result;
        }

        public async Task<int> UpdateUser(UserModel model)
        {
            TS_Users user = new TS_Users
            {
                Address = model.Address,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                UserName=model.UserName,
                PhoneNumber = model.PhoneNumber,
                RoleID = model.RoleID,
                UpdatedBy = model.UserID,
                UpdateOn = DateTime.Now,
                UserID = model.UserID 
            };
            var result = await _user.update(user);
            return result;

             
        }
        public async Task<int> Updatepassword(UserModel model)
        {
            TS_Users user = new TS_Users
            {
                Password = Utility.Encrypt(model.Password),
                UserID = model.UserID,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _user.updatepassword(user);
            return result; 
        }

        public async Task<UserModel> GetUserbyID(int UserID)
        {
            try
            {
                UserModel users = new UserModel();
                var user = await _user.GetUserById(UserID);
                users = new UserModel
                {
                    Address=user.Address,
                    Email=user.Email,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    PhoneNumber=user.PhoneNumber,
                    RoleName=user.RoleName,
                    RoleID=user.RoleID,
                    Password=user.Password,
                    UserName=user.UserName,
                    UserID=user.UserID,
                    ConformPassword=user.ConformPassword 
                };
                return users;

            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task<int> CreateUser(UserModel model)
        {
            TS_Users users = new TS_Users
            {
                Address = model.Address,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName=model.UserName,
                Password = Utility.Encrypt(model.Password),
                CreatedOn = DateTime.Now,
                CreatedBy=model.CreatedBy,
                RoleID = model.RoleID,
                IsActive = true, 
            };
            var result = await _user.create(users);
            return result;
            
        }

        public async Task<int> deleteUser(int Userid)
        {
            var result = await _user.Delete(Userid);
            return result;

        }
    }
}
