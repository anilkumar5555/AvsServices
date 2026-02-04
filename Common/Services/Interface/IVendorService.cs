using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
    public interface IVendorService
    {
        Task<List<VendorModel>> getAllVendors();
        Task<int> Create(VendorModel model);
        Task<int> Update(VendorModel model);

        Task<int> Delete(int VendorID);
       Task< VendorModel> GetVendorbyID(int VendorID);
    }
}
