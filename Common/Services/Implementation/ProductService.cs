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
    public class ProductService : IProductService
    {
        private IMaterial _material;
        private IProduct _product;
        public ProductService()
        {
            _material = new MaterialRepository();
            _product = new ProductRepository();
        }

        public async Task<List<ProductTypes>> GetProductTypes()
        {
            return await _product.GetProductTypes();
        }

        public async Task<int> CreateMaterial(MaterialModel model)
        {
            try
            {
                if (model.product.ModelId == 0)
                {
                    model.product.UserId = model.UserId;
                    model.product.ModelId = await Createmodel(model.product);
                }

                TS_Materials material = new TS_Materials
                {
                    InvoiceNo = model.InvoiceNo,
                    MaterialDescription = string.Empty,
                    MaterialName = string.Empty,
                    MaterialNo = model.MaterialNo,
                    ReceivedDate = Convert.ToDateTime(model.ReceivedDate),
                    ShippingDate = Convert.ToDateTime(model.ShippingDate),
                    VendorId = model.VendorId,
                    CreatedBy = model.UserId,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    TS_ProductModels = new List<TS_ProductModels>
                    { new TS_ProductModels
                        {
                            ProductTypeId = model.product.ProductTypeId,
                            MName = model.product.ModelName,
                            ProductModelId = model.product.ModelId,
                            Mcapacity = string.Empty,
                            MDescription = string.Empty,
                            MQty = model.product.Qty,
                            MUnitPrice = model.product.UnitPrice,
                            MCGST = model.product.CGST,
                            MSGST = model.product.SGST,
                            MIGST = model.product.IGST,
                            MTaxTotal = model.product.TotalTax,
                            MGrossAmount = model.product.TotalBalance,
                            MTotalPrice = model.product.Total,
                            MSrCode = string.Empty,
                            CreatedBy = model.UserId,
                            CreatedOn = DateTime.Now,
                            IsActive = true,
                           TS_ModelSerialNos = new List<TS_ModelSerialNos>
                           {
                               new TS_ModelSerialNos
                               {
                                    HSNCode = model.product.HSNCode,
                                    Capacity = model.product.Capacity,
                                    SerialNubmers = model.product.SerialNumber,
                                    ACSerialNumbers = model.product.ACSerialNumber,
                                    CreatedBy = model.UserId,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true
                               }
                           }
                        }
                    }
                };

                return await _product.createMaterial(material);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

       public async Task<int> CreateProduct(ProductModel model)
        {
            
            if(model.ModelId == 0)
            {
                model.ModelId = await Createmodel(model);
            }
            TS_ProductModels product = new TS_ProductModels()
            {
                MaterialID = model.MaterialId,
                ProductTypeId = model.ProductTypeId,
                ProductModelId = model.ModelId,
                MName = model.ModelName,

                Mcapacity = model.Capacity,

                MUnitPrice = model.UnitPrice,
                MQty = model.Qty,
                MIGST = model.IGST,
                MSGST = model.SGST,
                MCGST = model.CGST,
                MTotalPrice = model.TotalTaxPercent,
                MTaxTotal = model.TotalTax,
                MGrossAmount = model.TotalBalance,

                CreatedBy = model.UserId,
                CreatedOn = DateTime.Now,
                IsActive = true,
                TS_ModelSerialNos = new List<TS_ModelSerialNos>
                           {
                               new TS_ModelSerialNos
                               {
                                    HSNCode = model.HSNCode,
                                    Capacity = model.Capacity,
                                    SerialNubmers = model.SerialNumber,
                                    ACSerialNumbers = model.ACSerialNumber,
                                    IsActive = true,
                                    CreatedBy = model.UserId,
                                    CreatedOn = DateTime.Now
                               }
                           }
            };
            return await _product.createProduct(product);
        }

        public async Task<MaterialModel> GetMaterialById(int materialId)
        {
            return await _product.GetMaterialById(materialId);
        }

        public async Task<List<MaterialModel>> GetMaterials()
        {
            return await _product.GetMaterials();
        }

        public async Task<List<ProductModel>> GetMaterialProducts(int materialId)
        {
            return await _product.GetMaterialProducts(materialId);
        }

        public async Task<ProductModel> GetMaterialProductById(int productModelId)
        {
            return await _product.GetMaterialProductById(productModelId);
        }
        public async Task<int> UpdateProduct(ProductModel model)
        {
            if (model.ModelId == 0)
            {
                model.ModelId = await Createmodel(model);
            }
            return await _product.updateProduct(model);
        }

        public async Task<int> deleteProduct(int productmodelId, int userId)
        {
            return await _product.deleteProduct(productmodelId, userId);
        }
        public async Task<List<ProductModel>> GetmodelByProductType(int productTypeId)
        {
            return await _product.GetmodelByProductType(productTypeId); 
        }

        public async Task<List<ProductModel>> GetProductModels(int productTypeId)
        {
            return await _product.GetProductModels(productTypeId);
        }

        public async Task<ProductModel> GetProductModel(int modelId)
        {
            return await _product.GetProductModel(modelId);
        }

        public async Task<ProductModel> GetModelSerialNumbers(int modelId)
        {
            return await _product.GetModelSerialNumbers(modelId);
        }

        public async Task<int> Createmodel(ProductModel model)
        {
               var Productmodel = new TS_Models
                {
                    ProductTypeId = model.ProductTypeId,
                    ModelName = model.ModelName,
                    IGST = model.IGST,
                    CGST = model.CGST,
                    SGST = model.CGST,
                    TotalUnits = model.Qty,
                    TaxPercent = model.TotalTaxPercent,
                    TotalTax = model.TotalTax,
                    TotalCost = model.TotalBalance,
                    UnitPrice = model.UnitPrice,
                    SerialNo = model.SerialNumber,
                    ACSerialNo = model.ACSerialNumber,
                    HSNCode = model.HSNCode,
                    Capacity = model.Capacity,
                    IsActive = true,
                    CreatedBy = model.UserId,
                    CreatedOn = DateTime.Now
                };
            return await _product.createmodel(Productmodel);
        }
       public async Task<int> Updatematerial(MaterialModel model)
        {
            var result = await _product.updatematerial(model);
            return result;

        }
    }
}
