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
    public class ProductTypeService : IProductTypeService
    {
        private IProductTypes _productType;
        public ProductTypeService()
        {
            _productType = new ProductTypeRepository();
        }
        public async Task<int> CreateProductTypes(ProductTypes model)
        {
            TS_ProductTypes producttype = new TS_ProductTypes()
            {
                ProductTypeId = model.ProductTypeId,
                ProductDescription = model.ProductDescription,
                ProductTypeName = model.ProductTypeName,
                CreatedBy = model.UserID,
                IsActive = true,
                CreatedOn = DateTime.Now

            };
            var result = await _productType.createProductTypes(producttype);
            return result;



        }

        public async Task<int> DeleteProductTypes(int ProductTypeId)
        {
            var result = await _productType.deleteProductTypes(ProductTypeId);
            return result;
        }

        public async Task<List<ProductTypes>> getAllProductTypes()
        {
            return await _productType.GetAllProductTypes();
        }

        public async Task<ProductTypes> GetProductTypesbyID(int ProductTypeId)
        {
            ProductTypes product = new ProductTypes();
            var Producttype = await _productType.getProductTypebyID(ProductTypeId);
            product = new ProductTypes
            {
                ProductTypeId = Producttype.ProductTypeId,
                ProductDescription = Producttype.ProductDescription,
                ProductTypeName = Producttype.ProductTypeName,
                CreatedOn = Producttype.CreatedOn ?? Utility.GetCurrentDate()
            };
            return product;

        }

        public async Task<int> UpdateProductTypes(ProductTypes model)
        {
            var result = await _productType.updateProductTypes(model);
            return result;
        }
    }
}
