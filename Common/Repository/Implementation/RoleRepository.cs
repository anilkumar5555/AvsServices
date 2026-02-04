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
    public class RoleRepository : IRoles
    {
        private AVSTechnoEntities _connection;
        public RoleRepository()
        {
            _connection = new AVSTechnoEntities();
        }
        public async Task<bool> CreateRole(TS_Roles role)
        {
            try
            {
                _connection.TS_Roles.Add(role);
                await _connection.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRole(int roleId)
        {
            try
            {
                var rol = await _connection.TS_Roles.Where(r => r.RoleID == roleId && r.IsActive.Value).FirstOrDefaultAsync();
                if (rol != null)
                {
                    rol.IsActive = false;
                    await _connection.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<TS_Roles> GetRoleById(int roleID)
        {
            try
            {
                return await _connection.TS_Roles.Where(r => r.RoleID == roleID && r.IsActive == true).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TS_Roles>> getroles()
        {
            try
            {
                return await _connection.TS_Roles.Where(a => a.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRole(TS_Roles role)
        {
            try
            {
                var rol = await _connection.TS_Roles.Where(r => r.RoleID == role.RoleID && r.IsActive.Value).FirstOrDefaultAsync();
                if (rol != null)
                {
                    rol.RoleName = role.RoleName;
                    rol.RoleDescription = role.RoleDescription;
                    _connection.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
