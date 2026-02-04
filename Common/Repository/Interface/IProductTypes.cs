using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
     public interface IProductTypes
    {
        Task<List<ProductTypes>> GetAllProductTypes();
        Task<int> createProductTypes(TS_ProductTypes model);
        Task<int> updateProductTypes(ProductTypes model);

        Task<int> deleteProductTypes(int ProductTypeId);

        Task<TS_ProductTypes> getProductTypebyID(int ProductTypeId);
    }
}
