using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
    public interface IRoleServices
    {
        Task<List<RoleModel>> GetRoles();
        Task<RoleModel> GetRoleById(int roleId);
        Task<bool> CreateRole(RoleModel model);
        Task<bool> UpdateRole(RoleModel model);
        Task<bool> DeletRole(int roleId);
    }
}
