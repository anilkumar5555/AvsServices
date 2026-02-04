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
    public class CustomerProductRepository : ICustomerProduct
    {

        private AVSTechnoEntities _connection;
        private TimeZoneInfo INDIAN_ZONE;
        public CustomerProductRepository()
        {
            _connection = new AVSTechnoEntities();
            this.INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        }

        public  async Task<int> delete(int CProductID)
        {
            var customerProducts = await _connection.TS_CustomerProducts.Where(d => d.CProductID == CProductID).ToListAsync();
            if (customerProducts != null)
            {
                customerProducts.ForEach(a => a.IsActive = false);
                customerProducts.ForEach(a => a.UpdatedBy = 1);
                customerProducts.ForEach(a => a.UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE));

            }
            var result = await _connection.SaveChangesAsync();
            return result;
        }

        //public  async Task<int> create(TS_CustomerProducts model)
        //{
        //    try
        //    {
        //        _connection.TS_CustomerProducts.Add(model);
        //        var result = await _connection.SaveChangesAsync();
        //        var ProductId = await _connection.TS_CustomerProducts.OrderByDescending(a => a.CProductID).Select(a=>a.CProductID).FirstOrDefaultAsync();
        //        return ProductId;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;

        //    }
        //}

        public  async Task<List<TS_CustomerProducts>> GetAllCustomerProducts()
        {
            return await _connection.TS_CustomerProducts.Where(a => a.IsActive == true).ToListAsync();
        }

        public async Task<CustomerProductModel> GetCustomerProductById(int cProductId)
        {
            var _cusProduct = await (from cp in _connection.TS_CustomerProducts
                                      join m in _connection.TS_Models on cp.ProductModelId equals m.ProductModelId
                                      join p in _connection.TS_ProductTypes on m.ProductTypeId equals p.ProductTypeId
                                      where cp.CProductID == cProductId && cp.IsActive.Value
                                      select new CustomerProductModel
                                      {
                                          ACSerialNumber = cp.ACSerialNo,
                                          Capacity = m.Capacity,
                                          CGST = cp.CGST ?? 0,
                                          CProductID = cp.CProductID,
                                          CustomerID = cp.CustomerID ?? 0,
                                          Discount = cp.Discount ?? 0,
                                          GrossTotal = cp.GrossTotal ?? 0,
                                          HSNCode = m.HSNCode,
                                          IGST = cp.IGST ?? 0,
                                          ModelID = cp.ProductModelId ?? 0,
                                          NetTotal = cp.NetTotal ?? 0,
                                          PaymentTypeId = cp.PaymentTypeId ?? 0,
                                          ProductTypeId = m.ProductTypeId ?? 0,
                                          PaymentModeId = cp.PaymentModeId ?? 0,
                                          Qty = cp.Qty ?? 0,
                                          SellPrice = cp.SellPrice ?? 0,
                                          SGST = cp.SGST ?? 0,
                                          UnitPrice = cp.UnitPrice ?? 0,
                                          SerialNumber = cp.SerialNo,
                                          Totaltax = m.TotalTax??0,
                                          PaidAmount = cp.PaidAmount??0,
                                          RemainingAmount = cp.PendingAmount??0
                                          //ModelName = m.ModelName,
                                          //ProductTypeName = p.ProductTypeName
                                      }).FirstOrDefaultAsync();
            return _cusProduct;
        }

        public async Task<int> update(CustomerProductModel model)
        {
            var product = await _connection.TS_CustomerProducts.Where(a => a.CProductID == model.CProductID).FirstOrDefaultAsync();
            if(product != null)
            {
                product.CGST = model.CGST;
                product.Discount = model.Discount;
                product.GrossTotal = model.GrossTotal;
                product.IGST = model.IGST;
                product.NetTotal = model.NetTotal;
                product.Qty = model.Qty;
                product.SellPrice = model.SellPrice;
                product.SGST = model.SGST;
                product.UnitPrice = model.UnitPrice;
                product.UpdatedBy = model.UserID;
                product.UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE);
            }
            var result = await _connection.SaveChangesAsync();
            return result;
        }
    }
}
