using AVSModels.Models;
using Common.DBContext;
using Common.Helper;
using Common.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Implementation
{
    public class VendorRepository : IVendor
    {
        private AVSTechnoEntities _connection;
        public VendorRepository()
        {
            _connection = new AVSTechnoEntities();
        }

        public async Task<int> create(TS_Vendors model)
        {
            _connection.TS_Vendors.Add(model);
            _connection.SaveChanges();
            var vendorId = await _connection.TS_Vendors.OrderByDescending(v => v.VendorID).Select(a=>a.VendorID).FirstOrDefaultAsync();
            return vendorId;
        }

        public async Task<int> delete(int VendorID)
        {
            var result = 0;
            var vendor = await _connection.TS_Vendors.Where(v => v.VendorID == VendorID).FirstOrDefaultAsync();
            if(vendor != null)
            {
                vendor.IsActive = false;
                vendor.UpdateOn = DateTime.Now;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }

        public  async Task<List<VendorModel>> GetAllVendors()
        {
            return await _connection.TS_Vendors.Where(v => v.IsActive == true).Select(v=> new VendorModel 
            {
                VendorName = v.VName,
                Address = v.VAddress,
                PhoneNumber = v.VPhone,
                Email = v.VEmail,
                VendorID = v.VendorID,
                GSTIN = v.VGSTIN,
                Fax = v.VFax
            }).ToListAsync();
        }

        public  async Task<TS_Vendors> getVendorbyID(int VendorID)
        {
            return await _connection.TS_Vendors.Where(v => v.VendorID == VendorID && v.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<int> update(VendorModel model)
        {
            var result = 0;
            var vendor = await _connection.TS_Vendors.Where(v => v.VendorID == model.VendorID).FirstOrDefaultAsync();
            if(vendor != null)
            {
                vendor.VendorID = model.VendorID;
                vendor.VName = model.VendorName;
                vendor.VPhone = model.PhoneNumber;
                vendor.VGSTIN = model.GSTIN;
                vendor.VFax = model.Fax;
                vendor.VAddress = model.Address;
                vendor.VEmail = model.Email;
                vendor.UpdateOn = DateTime.Now; 
                vendor.UpdatedBy = model.UserID;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }
    }
}
