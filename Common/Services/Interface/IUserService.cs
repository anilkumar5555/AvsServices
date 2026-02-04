using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetUser(string username, string password);
        Task<UserModel> GetuserByPhoneNumber(string phonenumber);
        Task<int> UpdateUser(UserModel model);
       Task<int> Updatepassword(UserModel model);
        Task<UserModel> GetUserbyID(int UserID);
        Task<int> CreateUser(UserModel model);
        Task<List<UserModel>> getallusers();
        Task<int> deleteUser(int Userid);
    }
}
