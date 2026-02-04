using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{

    public interface IRoles
    {
        Task<List<TS_Roles>> getroles();
        Task<TS_Roles> GetRoleById(int roleID);
        Task<bool> CreateRole(TS_Roles role);
        Task<bool> UpdateRole(TS_Roles role);
        Task<bool> DeleteRole(int roleId);
    }
}

