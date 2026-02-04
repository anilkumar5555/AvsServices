using AVSModels.Models;
using Common.DBContext;
using Common.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Implementation
{
    public class UserRepository : IUser
    {
        private AVSTechnoEntities _connection;
        private UserModel _userModel;
        public UserRepository()
        {
            _connection = new AVSTechnoEntities();
        }

        public async Task<int> create(TS_Users user)
        {
              _connection.TS_Users.Add(user);
            return await _connection. SaveChangesAsync(); 
        }

        public async Task<int> Delete(int userid)
        {
            var user = await _connection.TS_Users.Where(a => a.UserID == userid).FirstOrDefaultAsync();
            if (user != null)
            {
                user.IsActive = false; 
            }
            var result = await _connection.SaveChangesAsync();
            return result;
 
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            try
            {
                var user = await (from u in _connection.TS_Users
                                  join r in _connection.TS_Roles on u.RoleID equals r.RoleID
                                  where u. IsActive.Value&&r.IsActive.Value
                                  select new UserModel
                                  {
                                      UserID = u.UserID,
                                      Address = u.Address,
                                      Email = u.Email,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Password = u.Password,
                                      PhoneNumber = u.PhoneNumber,
                                      RoleID = u.RoleID,
                                      RoleName = r.RoleName,
                                      UserName = u.UserName
                                  }).ToListAsync();

                return user;
            }

            catch (Exception ex)
            {
                throw;

            }

        }

        public async  Task<UserModel> GetUserById(int userid)
        {
            try
            {
                var user =await(from u in  _connection. TS_Users 
                            join r in _connection. TS_Roles on u.RoleID equals r.RoleID
                            where u.UserID ==  userid
                            select new UserModel
                            {
                                UserID = u.UserID, 
                                Address = u.Address,
                                Email = u.Email,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Password = u.Password,
                                PhoneNumber = u.PhoneNumber,
                                RoleID = u.RoleID,
                                RoleName = r.RoleName,
                                UserName = u.UserName
                            }). FirstOrDefaultAsync();

                return user;
            }
            
            catch (Exception ex)
            {
                throw;

            } 
        }

        public async Task<UserModel> GetUserByPassword(string userName, string Password)
        {
            return await _connection.TS_Users
                .Where(a => a.UserName.ToLower().Equals(userName.ToLower()) && a.Password.Equals(Password) && a.IsActive.Value)
                .Select(b => new UserModel
                {
                   UserName = b.UserName,
                   Address = b.Address,
                   Email = b.Email,
                   FirstName = b.FirstName,
                   LastName =b.LastName,
                   PhoneNumber = b.PhoneNumber,
                   RoleID = b.RoleID,
                   RoleName = b.TS_Roles.RoleName,
                   UserID = b.UserID,
                   Password = b.Password
                }).FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserByPhoneNumber(string PhoneNumber)
        {
            return await _connection.TS_Users
                .Where(a => a.PhoneNumber.ToLower().Equals(PhoneNumber.ToLower()) && a.IsActive.Value)
                .Select(b => new UserModel
                {
                    UserName=b.UserName,
                    Address=b.Address,
                    Email=b.Email,
                   FirstName=b.FirstName,
                   LastName=b.LastName,
                  Password=b.Password,
                  PhoneNumber=b.PhoneNumber,
                  RoleID=b.RoleID,
                  RoleName=b.TS_Roles.RoleName,
                  UserID=b.UserID 
                }).FirstOrDefaultAsync(); 
        }

        public async Task<int> update(TS_Users user)
        {
            var result = await _connection.TS_Users.Where(u => u.UserID == user.UserID).FirstOrDefaultAsync();
            if (result != null)
            {
                result.UserID = user.UserID;
                result.UserName = user.UserName;
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.Password = user.Password;
                result.PhoneNumber = user.PhoneNumber;
                result.RoleID = user. RoleID;
                result.Email = user.Email;
                result.UpdatedBy= user. UserID;
                result.UpdateOn = DateTime.Now; 
            }
            var res = _connection.SaveChanges();
            return res; 
        }

        public async Task<int> updatepassword(TS_Users  model)
        {
            var user =   new  TS_Users();
            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                user  =await _connection.TS_Users.Where(u => u.PhoneNumber.Equals(model.PhoneNumber)).FirstOrDefaultAsync();
            }
            else
            {
                user = await _connection.TS_Users.Where(u => u. UserID == model. UserID).FirstOrDefaultAsync();
            }

            if (user != null)
            {
                user. Password = model.Password;
            }
            var result = _connection.SaveChanges();
            return result;

        }

        
    }
}
