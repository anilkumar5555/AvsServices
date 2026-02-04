using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
    public interface ICustomerProductTransaction
    {

        Task<List<TS_CustomerProductTransaction>> GetAllCustomerProductTransactions();

        Task<int> update(CustomerProductTransactionModel model);

        Task<int> delete(int CPTransactionID);

        Task<TS_CustomerProductTransaction> getCustomerProductTransactionById(int CPTransactionID);
        Task<CustomerModel> getCustomerwithProductByCustomerId(int customerId);
    }
}
