using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductTypes>> GetProductTypes();
        Task<int> CreateMaterial(MaterialModel model);
        Task<MaterialModel> GetMaterialById(int materialId);
        Task<List<MaterialModel>> GetMaterials();
        Task<List<ProductModel>> GetMaterialProducts(int materialId);
        Task<ProductModel> GetMaterialProductById(int productModelId);
        Task<int> UpdateProduct(ProductModel model);
        Task<int> CreateProduct(ProductModel model);
        Task<int> deleteProduct(int productmodelId, int userId);
        Task<List<ProductModel>> GetmodelByProductType(int productTypeId);
        Task<List<ProductModel>> GetProductModels(int productTypeId);
        Task<ProductModel> GetProductModel(int modelId);
        Task<ProductModel> GetModelSerialNumbers(int modelId);

        Task<int> Createmodel(ProductModel model);
        Task<int> Updatematerial(MaterialModel model);
    }
}
