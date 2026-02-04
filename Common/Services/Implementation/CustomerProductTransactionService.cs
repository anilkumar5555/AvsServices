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
    public class CustomerProductTransactionService : ICustomerProductTransactionService
    {
        private ICustomerProductTransaction _customerProductTransaction;
        private TimeZoneInfo INDIAN_ZONE;
        public CustomerProductTransactionService()
        {
            _customerProductTransaction = new CustomerProductTransactionRepository();
            this.INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        }
        public async Task<int> Delete(int CPTransactionID)
        {
            var result = await _customerProductTransaction.delete(CPTransactionID);
            return result;
        }

        public async Task<List<CustomerProductTransactionModel>> getAllCustomerProductTransactions()
        {
            List<CustomerProductTransactionModel> customerProductTransactions = new List<CustomerProductTransactionModel>();
            var ProductTransactions = await _customerProductTransaction.GetAllCustomerProductTransactions();
            customerProductTransactions = ProductTransactions.Select(a => new CustomerProductTransactionModel
            {
                CProductID = a.CProductID ?? 0,
                CPTransactionID = a.CPTransactionID,
                NewPayment = a.NewPayment ?? 0,
                PaidAmount = a.PaidAmount ?? 0,
                RemainingAmount = a.RemainingAmount ?? 0,
                TotalAmount = a.TotalAmount ?? 0,
                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE)

            }).ToList();
            return customerProductTransactions;
        }

        public async Task<CustomerProductTransactionModel> GetCustomerProductTransactionbyId(int CPTransactionID)
        {
            CustomerProductTransactionModel customerProductTransaction = new CustomerProductTransactionModel();
            var ProductTransaction = await _customerProductTransaction.getCustomerProductTransactionById(CPTransactionID);
            customerProductTransaction = new CustomerProductTransactionModel
            {
                CPTransactionID = ProductTransaction.CPTransactionID,
                NewPayment = ProductTransaction.NewPayment ?? 0,
                RemainingAmount = ProductTransaction.RemainingAmount ?? 0,
                PaidAmount = ProductTransaction.PaidAmount ?? 0,
                TotalAmount = ProductTransaction.TotalAmount ?? 0,
                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE)
            };
            return customerProductTransaction;

        }

        public async Task<int> Update(CustomerProductTransactionModel model)
        {
            var result = await _customerProductTransaction.update(model);
            return result;
        }

        public async Task<CustomerModel> getCustomerwithProductByCustomerId(int customerId)
        {
            return await _customerProductTransaction.getCustomerwithProductByCustomerId(customerId);
        }
    }
}
    