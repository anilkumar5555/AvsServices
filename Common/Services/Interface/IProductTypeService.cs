using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
   public interface IProductTypeService
    {
        Task<List<ProductTypes>> getAllProductTypes();
        Task<int> CreateProductTypes(ProductTypes model);
        Task<int> UpdateProductTypes(ProductTypes model);

        Task<int> DeleteProductTypes(int ProductTypeId);
        Task<ProductTypes> GetProductTypesbyID(int ProductTypeId);
    }
}
