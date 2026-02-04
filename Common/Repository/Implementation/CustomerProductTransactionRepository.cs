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
    public class CustomerProductTransactionRepository : ICustomerProductTransaction
    {
        private AVSTechnoEntities _connection;
        private TimeZoneInfo INDIAN_ZONE;
        public CustomerProductTransactionRepository()
        {
            _connection = new AVSTechnoEntities();
            this.INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        }
        public async Task<int> delete(int CPTransactionID)
        {
            var customerProductTransactions = await _connection.TS_CustomerProductTransaction.Where(a => a.CPTransactionID == CPTransactionID).ToListAsync();
                if (customerProductTransactions != null)
            {
                customerProductTransactions.ForEach(a => a.IsActive = false);
                customerProductTransactions.ForEach(a => a.UpdatedBy = 1);
                customerProductTransactions.ForEach(a => a.UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE));

            }
            var result = await _connection.SaveChangesAsync();
            return result;

        }

        public async Task<List<TS_CustomerProductTransaction>> GetAllCustomerProductTransactions()
        {
            return await _connection.TS_CustomerProductTransaction.Where(a => a.IsActive == true).ToListAsync();

        }

        public  async Task<TS_CustomerProductTransaction> getCustomerProductTransactionById(int CPTransactionID)
        {
            return await _connection.TS_CustomerProductTransaction.Where(a => a.CPTransactionID == CPTransactionID && a.IsActive == true).FirstOrDefaultAsync();

        }

        public async Task<CustomerModel> getCustomerwithProductByCustomerId(int customerId)
        {
            return await (from cust in _connection.TS_Customers
                          join cusProduct in _connection.TS_CustomerProducts
                          on cust.CId equals cusProduct.CustomerID
                          join model in _connection.TS_Models
                          on cusProduct.ProductModelId equals model.ProductModelId
                          join productType in _connection.TS_ProductTypes
                          on cusProduct.PaymentTypeId equals productType.ProductTypeId
                          join cusProdTransaction in _connection.TS_CustomerProductTransaction
                          on cusProduct.CProductID equals cusProdTransaction.CProductID
                          where cust.IsActive.Value && cust.CId == customerId
                          select new CustomerModel
                          {
                              CustomerID = cust.CId,
                              CustomerName = cust.CName,
                              PhoneNumber = cust.CPhoneNumber,
                              Address = cust.CAddress,
                              Email = cust.CEmail,
                              SGSTNO=cust.SGSTIN,
                              SPhoneNumber=cust.SPhoneNumber,
                              SAddress=cust.SAddress,
                              SEmail=cust.SEmail,
                              SState=cust.SState,
                              SCustomerName=cust.SName,
                              SStateCode=cust.SStateCode,
                              GSTNO=cust.CGSTIN,
                              State=cust.CState,
                              StateCode=cust.CStateCode, 
                             
                              Product = new CustomerProductModel
                              {
                                  CGST = cusProduct.CGST ?? 0,
                                  IGST = cusProduct.IGST ?? 0,
                                  SGST = cusProduct.SGST ?? 0,
                                  GrossTotal = cusProduct.GrossTotal ?? 0,                                 
                                  Qty=cusProduct.Qty??0,
                                  Discount=cusProduct.Discount??0,
                                  NetTotal = cusProduct.NetTotal ?? 0,
                                  PaymentModeId = cusProduct.PaymentModeId ?? 0,
                                  PaymentTypeId = cusProduct.PaymentTypeId ?? 0,
                                  UnitPrice = cusProduct.UnitPrice ?? 0,
                                  ModelID = cusProduct.ProductModelId ?? 0,
                                  SellPrice = cusProduct.SellPrice ?? 0,
                                  ModelName = model.ModelName,
                                  ProductTypeName = productType.ProductTypeName,
                                  Transaction = new CustomerProductTransactionModel
                                  {
                                      NewPayment = cusProdTransaction.NewPayment ?? 0,
                                      PaymentTypeId = cusProduct.PaymentTypeId ?? 0,
                                      CPTransactionID = cusProdTransaction.CPTransactionID,                                      
                                      PaidAmount = cusProdTransaction.PaidAmount ?? 0,
                                      RemainingAmount = cusProdTransaction.RemainingAmount ?? 0,
                                      Totaltax = cusProdTransaction.RemainingAmount ?? 0,
                                      TotalAmount = cusProdTransaction.TotalAmount ?? 0
                                  }
                              }
                          }).FirstOrDefaultAsync();
        }

        public async Task<int> update(CustomerProductTransactionModel model)
        {
            var productTransaction = await _connection.TS_CustomerProductTransaction.Where(a => a.CPTransactionID == model.CPTransactionID).FirstOrDefaultAsync();
            if (productTransaction != null)
            {
                productTransaction.NewPayment = model.NewPayment;
                productTransaction.PaidAmount  = model.PaidAmount;
                productTransaction.RemainingAmount = model.RemainingAmount;
                productTransaction.TotalAmount = model.TotalAmount; 
                productTransaction.UpdatedBy = model.UserID;
                productTransaction.UpdateOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE);
            }
            var result = await _connection.SaveChangesAsync();
            return result;

        }
    }
}
