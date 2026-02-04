using AVSModels.Models;
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
    public class MaterialRepository : IMaterial
    {
        private AVSTechnoEntities _connection;
        public MaterialRepository()
        {
            _connection = new AVSTechnoEntities();
        }

        public async Task<List<MaterialModel>> GetMaterials()
        {
            return await _connection.TS_Materials.Where(a => a.IsActive.Value).Select(a => new MaterialModel
            {
                VendorId = a.VendorId,
                InvoiceNo = a.InvoiceNo,
                MaterialDescription = a.MaterialDescription,
                MaterialID = a.MaterialID,
                MaterialName = a.MaterialName,
                MaterialNo = a.MaterialNo,
                ReceivedDate = a.ReceivedDate.Value.ToString("yyyy-MM-dd"),
                ShippingDate = a.ShippingDate.Value.ToString("yyyy-MM-dd"),
            }).ToListAsync();
        }
    }
}
