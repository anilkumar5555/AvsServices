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
    public class ProductTypeRepository : IProductTypes
    {
        private AVSTechnoEntities _connection;
        public ProductTypeRepository()
        {
            _connection = new AVSTechnoEntities();
        }
        public async Task<int> createProductTypes(TS_ProductTypes model)
        {
            _connection.TS_ProductTypes.Add(model);
            _connection.SaveChanges();
            var Producttype = await _connection.TS_ProductTypes.OrderByDescending(a => a.ProductTypeId).Select(a => a.ProductTypeId).FirstOrDefaultAsync();
            return Producttype;
        }

        public async Task<int> deleteProductTypes(int ProductTypeId)
        {
            int result = 0;
            var producttype = await _connection.TS_ProductTypes.Where(p => p.ProductTypeId == ProductTypeId).FirstOrDefaultAsync();
            if (producttype != null)
            {
                producttype.IsActive = false;
                producttype.UpdateOn = DateTime.Now;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<ProductTypes>> GetAllProductTypes()
        {
            return await _connection.TS_ProductTypes.Where(a => a.IsActive == true).Select(p=>new ProductTypes
            {
                ProductTypeName = p.ProductTypeName,
                ProductDescription = p.ProductDescription,
                ProductTypeId = p.ProductTypeId,
              //CreatedOn = Utility.GetCurrentDate()
        }).ToListAsync();
        }

        public async Task<TS_ProductTypes> getProductTypebyID(int ProductTypeId)
        {
            return await _connection.TS_ProductTypes.Where(a => a.ProductTypeId == ProductTypeId && a.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<int> updateProductTypes(ProductTypes model)
        {
            var result = 0;
            var productype = await _connection.TS_ProductTypes.Where(a => a.ProductTypeId == model.ProductTypeId).FirstOrDefaultAsync();
            if(productype != null)
            {
                productype.ProductTypeName = model.ProductTypeName;
                productype.ProductDescription = model.ProductDescription;
                productype.UpdateOn = Utility.GetCurrentDate();
                productype.UpdatedBy = model.UserID;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }
    }
}
