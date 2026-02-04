using AVSModels.Models;
using Common.DBContext;
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
    
   public class RoleServices : IRoleServices
    {
        private IRoles _roles;
        public RoleServices()
        {
            _roles = new RoleRepository();
        }
        public async Task<bool> CreateRole(RoleModel model)
        {
            try
            {
                 TS_Roles role = new TS_Roles
                 {
                    CreatedOn = DateTime.Now,
                     RoleDescription = model.Description,
                    IsActive = true,
                    RoleName = model.RoleName
                };
                return await _roles.CreateRole(role);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeletRole(int roleId)
        {
            try
            {
                return await _roles.DeleteRole(roleId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleModel> GetRoleById(int roleId)
        {
            try
            {
                var r = await _roles.GetRoleById(roleId);
                RoleModel role = new RoleModel
                {
                     RoleID = r.RoleID,
                    RoleName = r.RoleName,
                    Description = r. RoleDescription
                };
                return role;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<RoleModel>> GetRoles()
        {
            try
            {
                var roles = await _roles.getroles();
               return  roles.Select(r => new RoleModel
                {
                    RoleID = r.RoleID,
                    RoleName = r.RoleName
                }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> UpdateRole(RoleModel model)
        {
            try
            {
                 TS_Roles role = new TS_Roles
                 {
                    RoleDescription = model.Description,
                    IsActive = true,
                    RoleName = model.RoleName,
                    RoleID = model. RoleID
                };
                return await _roles.UpdateRole(role);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
