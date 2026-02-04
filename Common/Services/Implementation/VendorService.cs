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
    
    public class VendorService : IVendorService
    {
        private IVendor _vendor;
        public VendorService()
        {
            _vendor = new VendorRepository();
        }

        public async Task<int> Create(VendorModel model)
        {
            TS_Vendors vendor = new TS_Vendors()
            {
                VendorID = model.VendorID,
               VName = model.VendorName,
                VPhone = model.PhoneNumber,
                VEmail = model.Email,
                VGSTIN = model.GSTIN,
                VFax = model.Fax,
                VAddress = model.Address,
                IsActive = true,
                CreatedBy = model.UserID,
                CreatedOn = DateTime.Now
            };
            var result = await _vendor.create(vendor);
            return result;
        }

        public async Task<int> Delete(int VendorID)
        {
            var result = await _vendor.delete(VendorID);
            return result;
        }

        public async Task<List<VendorModel>> getAllVendors()
        {
            return await _vendor.GetAllVendors();
        }

        public async Task<VendorModel> GetVendorbyID(int VendorID)
        {
            VendorModel vendor = new VendorModel();
            var vendors = await _vendor.getVendorbyID(VendorID);
            vendor = new VendorModel
            {
                VendorID = vendors.VendorID,
                VendorName = vendors.VName,
                Address = vendors.VAddress,
                Email = vendors.VEmail,
                PhoneNumber = vendors.VPhone,
                Fax = vendors.VFax,
                GSTIN = vendors.VGSTIN,
                CreatedOn = vendors.CreatedOn 
            };
            return vendor;
        }

        public async Task<int> Update(VendorModel model)
        {
            var result = await _vendor.update(model);
            return result;
        }
    }
}
