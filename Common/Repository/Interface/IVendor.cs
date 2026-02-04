using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
    public interface IVendor
    {
        Task<List<VendorModel>> GetAllVendors();
        Task<int> create(TS_Vendors model);
        Task<int> update(VendorModel model);

        Task<int> delete(int VendorID);

        Task<TS_Vendors> getVendorbyID(int VendorID);
    }
}
