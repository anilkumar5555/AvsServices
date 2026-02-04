using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
    public interface IProduct
    {
        Task<int> createMaterial(TS_Materials model);
        Task<List<ProductTypes>> GetProductTypes();
        Task<List<MaterialProductModel>> GetProducts();
        Task<List<ProductModel>> GetMaterialProducts(int materialId);
        Task<ProductModel> GetMaterialProductById(int materilaProductId);
        Task<List<MaterialModel>> GetMaterials();
        Task<MaterialModel> GetMaterialById(int materialId);
        Task<int> updateProduct(ProductModel model);
        Task<int> createProduct(TS_ProductModels model);
        Task<int> deleteProduct(int productmodelId, int userId);
        Task<List<ProductModel>> GetmodelByProductType(int productTypeId);
        Task<List<ProductModel>> GetProductModels(int productTypeId);
        Task<ProductModel> GetProductModel(int modelId);
        Task<ProductModel> GetModelSerialNumbers(int modelId);

        Task<int> createmodel(TS_Models model);
        Task<int> updatematerial(MaterialModel model);
    }
}