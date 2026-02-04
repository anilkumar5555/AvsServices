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
    public class CustomerRepository : ICustomer
    {
        private AVSTechnoEntities _connection;
        private TimeZoneInfo INDIAN_ZONE;
        public CustomerRepository()
        {
            _connection = new AVSTechnoEntities();  
            this.INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        }
        public async Task<int> create(TS_Customers model)
        {
            try
            {
                _connection.TS_Customers.Add(model);
                var result = _connection.SaveChanges();
                var cid = await _connection.TS_Customers.OrderByDescending(a => a.CId).Select(s=>s.CId).FirstOrDefaultAsync();
                return cid;
            }
            catch (Exception ex)
            {
                throw;
            
            }
        }

        public async Task<CustomerModel> getcustomerByid(int customerid)
        {
            return await _connection.TS_Customers.Where(a => a.CId == customerid && a.IsActive == true).Select(c => new CustomerModel
            {
                CustomerID = c.CId,
                CustomerName = c.CName,
                Address = c.CAddress,
                Email = c.CEmail,
                PhoneNumber = c.CPhoneNumber,
                GSTNO = c.CGSTIN,
                State = c.CState,
                StateCode = c.CStateCode,
                SCustomerName = c.SName,
                SPhoneNumber = c.SPhoneNumber,
                SAddress = c.SAddress,
                SGSTNO = c.SGSTIN,
                SState = c.SState,
                SStateCode = c.SStateCode,
                SEmail = c.SEmail,
                IsSameAs = c.IsSameAs??false,

            }).FirstOrDefaultAsync();
        }

        public async Task<int> delete(int customerid)
        {
            var customer =await _connection.TS_Customers.Where(a => a.CId == customerid).FirstOrDefaultAsync();
            if (customer != null)
            {
                customer.IsActive = false;
                customer.UpdatedBy = 1;
                customer.UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE);
            }
            var customerProducts = _connection.TS_CustomerProducts.Where(d => d.CustomerID == customerid).ToList();
            if (customerProducts != null)
            {
                customerProducts.ForEach(a => a.IsActive = false);
                customerProducts.ForEach(a => a.UpdatedBy = 1);
                customerProducts.ForEach(a => a.UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE)); 
            
            }
            var result = _connection.SaveChanges();
            return result;

        } 
        public async Task <int> update(CustomerModel model)
        {
            var customer = await _connection.TS_Customers.Where(c => c.CId == model.CustomerID).FirstOrDefaultAsync();
            if (customer != null)
            {
                customer.CName = model. CustomerName;
                customer.CPhoneNumber = model. PhoneNumber;
                customer.CEmail = model. Email;
                customer.CAddress = model. Address; 
                customer.UpdatedBy = model. userid;
                customer. UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE); 
            }
            var result = _connection.SaveChanges();
            return result;
        }

        public List<TS_Customers> getallcustomers()
        {
            return _connection.TS_Customers.Where(a => a.IsActive == true).ToList();
        }

        public async Task<List<PaymentTypes>> PaymentTypes()
        {
            return await _connection.TS_PaymentTypes
                .Where(a => a.IsActive.Value)
                .Select(b => new PaymentTypes {
                PaymentTypeId=b.PaymentTypeId,
                PaymentTypeName=b.PaymentTypeName
                }).ToListAsync(); 
        }

        public async Task<List<CustomerProductModel>> GetProductsByCustomerId(int customerId)
        {
            var _cusProducts = await (from cp in _connection.TS_CustomerProducts
                                   join m in _connection.TS_Models on cp.ProductModelId equals m.ProductModelId
                                   join p in _connection.TS_ProductTypes on m.ProductTypeId equals p.ProductTypeId
                                   where cp.CustomerID == customerId && cp.IsActive.Value
                                   select new CustomerProductModel
                                   {
                                       ACSerialNumber = cp.ACSerialNo,
                                       Capacity = m.Capacity,
                                       CGST = cp.CGST ?? 0,
                                       CProductID = cp.CProductID,
                                       CustomerID = cp.CustomerID ?? 0,
                                       CreatedOn = cp.CreatedOn,
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
                                       ModelName = m.ModelName,
                                       ProductTypeName = p.ProductTypeName
                                   }).ToListAsync();
            return _cusProducts;
        }

        public async Task<List<PaymentModes>>  PaymentModes()
        {
            return await _connection.TS_PaymentModes
                .Where(a => a.IsActive.Value)
                .Select(b => new PaymentModes
            {
                PaymentModeId=b.PaymentModeId,
                PaymentModeName=b.PaymentModeName 
            }).ToListAsync(); 
        }

        public async Task<int> CreateProduct(TS_CustomerProducts model)
        {
            _connection.TS_CustomerProducts.Add(model);
            var result = _connection.SaveChanges();
            var cproductid = await _connection.TS_CustomerProducts.OrderByDescending(a => a.CProductID).Select(a => a.CProductID).FirstOrDefaultAsync();
            return cproductid;
        }

        public async Task<int> UpdateProduct(CustomerProductModel model)
        {
            var customerproduct = await _connection.TS_CustomerProducts.Where(a => a.CProductID == model.CProductID).FirstOrDefaultAsync();
            if(customerproduct != null)
            {
                customerproduct.ACSerialNo = model.ACSerialNumber;
                customerproduct.SerialNo = model.SerialNumber;

                customerproduct.Qty = model.Qty;
                customerproduct.SellPrice = model.SellPrice;

                customerproduct.CGST = model.CGST;
                customerproduct.SGST = model.SGST;
                customerproduct.IGST = model.IGST;

                customerproduct.Discount = model.Discount;
                customerproduct.PaidAmount = model.PaidAmount;
                customerproduct.PendingAmount = model.RemainingAmount;
                customerproduct.NetTotal = model.NetTotal;
                customerproduct.GrossTotal = model.GrossTotal;

                customerproduct.PaymentModeId = model.PaymentModeId;
                customerproduct.PaymentTypeId = model.PaymentTypeId;
                customerproduct.ProductModelId = model.ModelID;
                
                
                customerproduct.UnitPrice = model.UnitPrice;
                customerproduct.UpdatedBy = model.UserID;
            }
            var result = await _connection.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteCustomerProduct(int CProductId)
        {
            var customerProducts = await _connection.TS_CustomerProducts.Where(d => d.CProductID == CProductId).ToListAsync();
            if (customerProducts != null)
            {
                customerProducts.ForEach(a => a.IsActive = false);
                customerProducts.ForEach(a => a.UpdatedBy = 1);
                customerProducts.ForEach(a => a.UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE));

            }
            var result = await _connection.SaveChangesAsync();
            return result;
        }
    }
}
