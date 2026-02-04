using AVSModels.Models;
using Common.DBContext;
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
    public class Customerservice : ICustomerservice
    {
        private ICustomer _customer;
        private TimeZoneInfo INDIAN_ZONE;

        public Customerservice()
        {
            _customer = new CustomerRepository();
            this.INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        }
        public async Task<int> createcustomer(CustomerModel model)
        {

            TS_Customers Customers = new TS_Customers()
            {
                CId = model.CustomerID,
                CName = model.CustomerName,
                CAddress = model.Address,
                CEmail = model.Email,
                CPhoneNumber = model.PhoneNumber,
                CGSTIN = model.GSTNO,
                CState = model.State,
                CStateCode = model.StateCode,
                SName = model.SCustomerName,
                SPhoneNumber = model.SPhoneNumber,
                SAddress = model.SAddress,
                SEmail = model.SEmail,
                IsSameAs = model.IsSameAs,
                SGSTIN = model.SGSTNO,
                SState = model.SState,
                SStateCode = model.SStateCode,
                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE),
                CreatedBy=model.userid,
                IsActive = true,
                TS_CustomerProducts = new List<TS_CustomerProducts>
                {
                    new TS_CustomerProducts
                    { 
                        CGST = model.Product.CGST,
                        CProductID = model.Product.CProductID,
                        Discount = model.Product.Discount,
                        CustomerID = model.Product.CustomerID,
                        GrossTotal = model.Product.GrossTotal,
                        IGST = model.Product.IGST,
                        ProductModelId = model.Product.ModelID,
                        IsActive = true,
                        CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE), 
                        NetTotal = model.Product.NetTotal,
                        PaymentModeId = model.Product.PaymentModeId,
                        CreatedBy = model.userid,
                        PaymentTypeId = model.Product.PaymentTypeId,
                        SerialNo=model.Product.SerialNumber, 
                        ACSerialNo=model.Product.ACSerialNumber, 
                        Qty = model.Product.Qty,
                        SellPrice = model.Product.SellPrice,
                        SGST = model.Product.SGST,
                        UnitPrice = model.Product.UnitPrice,
                        PendingAmount = model.Product.Transaction.RemainingAmount,
                        PaidAmount = model.Product.Transaction.PaidAmount,
                        TS_CustomerProductTransaction = new List<TS_CustomerProductTransaction>
                        {
                            new TS_CustomerProductTransaction
                            {
                                CProductID = model.Product.Transaction.CProductID,
                                NewPayment = model.Product.Transaction.NewPayment,
                                PaidAmount = model.Product.Transaction.PaidAmount,
                               
                                PaymentTypeId = model.Product.PaymentTypeId,
                                RemainingAmount = model.Product.Transaction.RemainingAmount,
                                TotalAmount = model.Product.Transaction.TotalAmount,
                                IsActive = true,
                                CreatedBy = model.userid,
                                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE),
                            }
                        }
                    }
                }
            };
            return await _customer.create(Customers);
        }

        public async Task<int> deletecustomer(int CustomerID)
        {
            var result = await _customer.delete(CustomerID);
            return result;
        }

        public List<CustomerModel> GetallCustomers()
        {

            List<CustomerModel> customers = new List<CustomerModel>();
            customers = _customer.getallcustomers().Select(a => new CustomerModel
            {
                Address = a.CAddress,
                CustomerID = a.CId,
                CustomerName = a.CName,
                Email = a.CEmail,
                PhoneNumber = a.CPhoneNumber,
                CreatedOn =a.CreatedOn.Value.ToShortDateString(),
            }).ToList();
            return customers;
        }

        public async Task<CustomerModel> GetCustomerById(int customerid)
        {
           return await _customer.getcustomerByid(customerid);
        }
        public async Task<int> updatecustomer(CustomerModel model)
        {
            var result = await _customer.update(model);
            return result;

        }
        public async Task<List<PaymentModes>> PaymentModes()
        {

            return await _customer.PaymentModes();
         }
        public async Task<List<PaymentTypes>> PaymentTypes()
        {
            return await _customer.PaymentTypes();
        }

        public async Task<List<CustomerProductModel>> GetProductsByCustomerId(int customerId)
        {
            return await _customer.GetProductsByCustomerId(customerId);
        }

        public async Task<int> createProduct(CustomerProductModel model)
        {
            TS_CustomerProducts customerProduct = new TS_CustomerProducts()
            {
                
                CProductID = model.CProductID,
                CustomerID = model.CustomerID,
                ProductModelId = model.ModelID,
                PaymentModeId = model.PaymentModeId,
                PaymentTypeId = model.PaymentTypeId,
                
                CGST = model.CGST,
                SGST = model.SGST,
                IGST = model.IGST,
                Discount = model.Discount,
                NetTotal = model.NetTotal,
                Qty = model.Qty,
                SellPrice = model.SellPrice,
                UnitPrice = model.UnitPrice,
                PendingAmount = model.Transaction.RemainingAmount,
                PaidAmount = model.Transaction.PaidAmount,
                GrossTotal = model.GrossTotal,

                SerialNo = model.SerialNumber,
                ACSerialNo = model.ACSerialNumber,

                IsActive = true,
                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE),
                CreatedBy = model.UserID,

                TS_CustomerProductTransaction = new List<TS_CustomerProductTransaction>
                        {
                            new TS_CustomerProductTransaction
                            {
                                NewPayment = model.Transaction.NewPayment,
                                PaidAmount = model.Transaction.PaidAmount,
                                PaymentTypeId = model.PaymentTypeId,
                                RemainingAmount = model.Transaction.RemainingAmount,
                                TotalAmount = model.Transaction.TotalAmount,
                                IsActive = true,
                                CreatedBy = model.UserID,
                                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE),
                            }
                        }
            };
            return await _customer.CreateProduct(customerProduct);
        }

        public async Task<int> updateProduct(CustomerProductModel model)
        {
            var result=  await _customer.UpdateProduct(model);
            return result;

        }

        public async Task<int> deleteCustomerProduct(int CProductId)
        {
            var result = await _customer.DeleteCustomerProduct(CProductId);
            return result;
        }
    }
}

