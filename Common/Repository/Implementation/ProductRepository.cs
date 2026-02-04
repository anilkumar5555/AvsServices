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
    public class ProductRepository : IProduct
    {
        private AVSTechnoEntities _connection;
        public ProductRepository()
        {
            _connection = new AVSTechnoEntities();
        }

        public async Task<List<ProductTypes>> GetProductTypes()
        {
            return await _connection.TS_ProductTypes.Where(a => a.IsActive.Value).Select(a => new ProductTypes 
                {
                    ProductType = a.ProductTypeName,
                    ProductTypeId = a.ProductTypeId
                }).ToListAsync();
        }

        public async Task<List<MaterialProductModel>> GetProducts()
        {
            return await _connection.TS_ProductModels.Where(a => a.IsActive.Value).Select(a => new MaterialProductModel
            {
                ProductTypeName = a.TS_ProductTypes.ProductTypeName,
                ProductTypeId = a.ProductTypeId,
                MaterialID = a.MaterialID,
                Mcapacity = a.Mcapacity,
                MCGST = a.MCGST,
                MDescription = a.MDescription,
                MGrossAmount = a.MGrossAmount,
                MIGST = a.MIGST,
                MName = a.MName,
                ModelID = a.ModelID,
                MQty = a.MQty,
                MSGST = a.MSGST,
                MSrCode = a.MSrCode,
                MTaxTotal = a.MTaxTotal,
                MTotalPrice = a.MTotalPrice,
                MUnitPrice = a.MUnitPrice,
            }).ToListAsync();
        }



        public async Task<int> createMaterial(TS_Materials model)
        {

            _connection.TS_Materials.Add(model);
            _connection.SaveChanges();
            return await _connection.TS_Materials.OrderByDescending(a => a.MaterialID).Select(s => s.MaterialID).FirstOrDefaultAsync();
        }

        public async Task<List<ProductModel>> GetMaterialProducts(int materialId)
        {
            var Products =  await (from pm in _connection.TS_ProductModels
                                  join m in _connection.TS_Models on pm.ProductModelId equals m.ProductModelId
                                  join p in _connection.TS_ProductTypes on pm.ProductTypeId equals p.ProductTypeId
                                  where pm.MaterialID == materialId && pm.IsActive.Value 
                                     select new ProductModel
                                        {
                                            ProductModelId = pm.ModelID,
                                            MaterialId = pm.MaterialID ??0,
                                            ModelName = m.ModelName,
                                            ProductTypeName = m.TS_ProductTypes.ProductTypeName,
                                            UnitPrice = pm.MUnitPrice,
                                            Qty = pm.MQty,
                                            CGST = m.CGST,
                                            IGST = m.IGST,
                                            SGST = m.SGST,
                                            TotalTax = m.TotalTax,
                                            TotalBalance = pm.MGrossAmount,
                                            CreatedOn = pm.CreatedOn
                                        }).ToListAsync();
            return Products;
        }

        public async Task<ProductModel> GetMaterialProductById(int materilaProductId)
        {
            var product = await (from pm in _connection.TS_ProductModels
                                 join m in _connection.TS_Models on pm.ProductModelId equals m.ProductModelId
                                 join sr in _connection.TS_ModelSerialNos on pm.ModelID equals sr.ModelId
                                 where pm.ModelID == materilaProductId && pm.IsActive.Value
                                 select new ProductModel
                                 {
                                     MaterialId = pm.MaterialID ?? 0,
                                     ProductModelId = pm.ModelID,
                                     ProductTypeId = m.ProductTypeId ?? 0,
                                     ModelId = m.ProductModelId,
                                     ModelName = m.ModelName,
                                     UnitPrice = m.UnitPrice,
                                     CGST = m.CGST,
                                     IGST = m.IGST,
                                     SGST = m.SGST,
                                     TotalTax = m.TotalTax,
                                     TotalTaxPercent = m.TaxPercent,
                                     Capacity = m.Capacity,
                                     HSNCode = m.HSNCode,
                                     taxPercent = (m.TaxPercent ?? 0) != 0 ? m.TaxPercent / 2 : 0,
                                     TotalBalance = pm.MGrossAmount,
                                     Qty = pm.MQty,
                                     CreatedOn = pm.CreatedOn,
                                     SerialNumber = sr.SerialNubmers,
                                     ACSerialNumber = sr.ACSerialNumbers,
                                     SerialNoId = sr.SerialID
                                 }).FirstOrDefaultAsync();
            return product;
        }


        public async Task<MaterialModel> GetMaterialById(int materialId)
        {
            var material = await _connection.TS_Materials.FirstOrDefaultAsync(a => a.MaterialID == materialId && a.IsActive.Value);
            return new MaterialModel
            {
                InvoiceNo = material.InvoiceNo,
                MaterialDescription = material.MaterialDescription,
                MaterialID = material.MaterialID,
                MaterialName = material.MaterialName,
                MaterialNo = material.MaterialNo,
                ReceivedDate = material.ReceivedDate.Value.ToString("yyyy-MM-dd"),
                ShippingDate = material.ShippingDate.Value.ToString("yyyy-MM-dd"),
                VendorId = material.VendorId,
                CreatedOn = material.CreatedOn
            };
        }

        public async Task<List<MaterialModel>> GetMaterials()
        {
            return await _connection.TS_Materials.Where(a => a.IsActive.Value).Select(a => new MaterialModel
            {
                InvoiceNo = a.InvoiceNo,
                MaterialID = a.MaterialID,
                MaterialNo = a.MaterialNo,
                RDate = a.ReceivedDate,
                SDate = a.ShippingDate,
                VendorId = a.VendorId,
                VendorName = a.TS_Vendors.VName,
                CreatedOn = a.CreatedOn
            }).ToListAsync();
        }
        public async Task<List<ProductModel>> GetmodelByProductType(int productTypeId)
        {
            return await _connection.TS_ProductModels.Where(a => a.ProductTypeId == productTypeId && a.IsActive.Value).Select(a => new ProductModel
            {
                ProductModelId=a.ModelID,
                ModelName=a.MName

            }).ToListAsync(); 
        }

        public  async Task<int> updateProduct(ProductModel model)
        {
            
            var product = await _connection.TS_ProductModels.Where(a => a.ModelID == model.ProductModelId).FirstOrDefaultAsync();
            if(product != null)
            {
                product.ProductTypeId = model.ProductTypeId;
                product.ProductModelId = model.ModelId;
                product.MName = model.ModelName;

                product.Mcapacity = model.Capacity;

                product.MQty = model.Qty;
                product.MUnitPrice = model.UnitPrice;
                product.MCGST = model.CGST;
                product.MIGST = model.IGST;
                product.MCGST = model.CGST;
                product.MTaxTotal = model.TotalTax;
                product.MTotalPrice = model.TotalTaxPercent;
                product.MGrossAmount = model.TotalBalance;

                product.UpdatedBy = model.UserId;
                product.UpdateOn = DateTime.Now;
                _connection.Entry(product).State = EntityState.Modified;
            }
            var serialNos = await _connection.TS_ModelSerialNos.FirstOrDefaultAsync(a => a.SerialID == model.SerialNoId && a.IsActive.Value);
            if (serialNos != null)
            {
                serialNos.SerialNubmers = model.SerialNumber;
                serialNos.ACSerialNumbers = model.ACSerialNumber;
                serialNos.Capacity = model.Capacity;
                serialNos.HSNCode = model.HSNCode;
                serialNos.UpdateOn = DateTime.Now;
                serialNos.UpdatedBy = model.UserId;
                _connection.Entry(serialNos).State = EntityState.Modified;
            }
            var result = await _connection.SaveChangesAsync();
            return result;
        }

        public async Task<int> createProduct(TS_ProductModels model)
        {
        
                _connection.TS_ProductModels.Add(model);
                return  await _connection.SaveChangesAsync();
           
        }

        public async Task<int> deleteProduct(int productmodelId, int userId)
        {
            var result = 0;
            var product = await _connection.TS_ProductModels.FirstOrDefaultAsync(v => v.ModelID == productmodelId);
            if (product != null)
            {
                product.IsActive = false;
                product.UpdateOn = DateTime.Now;
                product.UpdatedBy = userId;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<ProductModel>> GetProductModels(int productTypeId)
        {
            return await _connection.TS_Models
                .Where(a => a.ProductTypeId == productTypeId && a.IsActive.Value)
                .Select(a => new ProductModel
                {
                    ModelId = a.ProductModelId,
                    ModelName = a.ModelName
                }).ToListAsync();
        }

        public async Task<ProductModel> GetProductModel(int modelId)
        {
            return await _connection.TS_Models
                .Where(a => a.ProductModelId == modelId && a.IsActive.Value)
                .Select(a => new ProductModel
                {
                    ModelId = a.ProductModelId,
                    ModelName = a.ModelName,
                    ProductModelId = a.ProductModelId,
                    ProductTypeName = a.TS_ProductTypes.ProductTypeName,
                    ProductTypeId = a.ProductTypeId ?? 0,
                    CGST = a.CGST,
                    SGST = a.SGST,
                    IGST = a.IGST,
                    ACSerialNumber= a.ACSerialNo,
                    SerialNumber = a.SerialNo,
                    TotalTaxPercent = a.TaxPercent,
                    HSNCode = a.HSNCode,
                    Capacity = a.Capacity,
                    Qty = a.TotalUnits,
                    UnitPrice = a.UnitPrice,
                    CreatedOn = a.CreatedOn,
                    TotalBalance = a.TotalCost,
                    TotalTax = a.TotalTax,
                    taxPercent = (a.TaxPercent??0) != 0 ? (a.TaxPercent / 2) : 0
                }).FirstOrDefaultAsync();
        }

        public async Task<ProductModel> GetModelSerialNumbers(int modelId)
        {
            var _productModel = await _connection.TS_Models.Where(a => a.ProductModelId == modelId && a.IsActive.Value).FirstOrDefaultAsync();
            var productModel =  new ProductModel
                    {
                        ACSerialNumber = string.Join(",", _productModel.ACSerialNo.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray()),
                        SerialNumber = string.Join(",", _productModel.SerialNo.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray()),
                    };
            return productModel;
        }

        public async Task<int> createmodel(TS_Models model)
        {
            _connection.TS_Models.Add(model);
            var result =  await _connection.SaveChangesAsync();
            var modelid = await _connection.TS_Models.OrderByDescending(a => a.ProductModelId).Select(a => a.ProductModelId).FirstOrDefaultAsync();
            return modelid;
        }

        public async Task<int> UpdateModel(ProductModel model)
        {
            var result = 0;
            var _model = await _connection.TS_Models.Where(a => a.ProductModelId == model.ModelId).FirstOrDefaultAsync();
            if (_model != null)
            {
                _model.ProductTypeId = model.ProductTypeId;
                //_model.ModelName = model.ModelName;
                _model.Capacity = model.Capacity;
                _model.HSNCode = model.HSNCode;
                _model.UnitPrice = model.UnitPrice;
                _model.IGST = model.IGST;
                _model.CGST = model.CGST;
                _model.SGST = model.SGST;
                _model.TaxPercent = model.taxPercent;
                _model.TotalTax = model.TotalTax;
                _model.TotalCost = model.TotalBalance;
                _model.UpdatedBy = model.UserId;
                _model.UpdateOn = DateTime.Now;
                _connection.Entry(_model).State = EntityState.Modified;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> updatematerial(MaterialModel model)
        {
            var result = 0;
            var material = await _connection.TS_Materials.Where(a => a.MaterialID == model.MaterialID).FirstOrDefaultAsync();
            if(material != null)
            {
                material.MaterialName = model.MaterialName;
                material.VendorId = model.VendorId;
                material.MaterialNo = model.MaterialNo;
                material.InvoiceNo = model.InvoiceNo;
                //material.ShippingDate = model.ShippingDate;
                //material.ReceivedDate = model.ReceivedDate;
                _connection.Entry(material).State = EntityState.Modified;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }
    }
}
