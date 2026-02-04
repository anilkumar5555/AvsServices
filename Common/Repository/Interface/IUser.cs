using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
    public interface IUser
    {
        Task<UserModel> GetUserByPassword(string userName, string Password);
        Task<UserModel> GetUserByPhoneNumber(string PhoneNumber);
        Task<int> update(TS_Users user);
        Task<int> updatepassword(TS_Users  model);
        Task<UserModel> GetUserById(int userid);
        Task<int> create(TS_Users  user);
        Task<List<UserModel>> GetAllUsers();
        Task<int> Delete(int userid);
        
    }
}
