using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
     public interface ICustomerProductTransactionService
    {
        Task<List<CustomerProductTransactionModel>> getAllCustomerProductTransactions();

        Task<int> Update(CustomerProductTransactionModel model);

        Task<int> Delete(int CPTransactionID);
        Task<CustomerProductTransactionModel> GetCustomerProductTransactionbyId(int CPTransactionID);
        Task<CustomerModel> getCustomerwithProductByCustomerId(int customerId);
    }
}
