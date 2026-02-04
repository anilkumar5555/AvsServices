using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
   public  interface ICustomer
    {
        List<TS_Customers> getallcustomers();
        Task<int> create(TS_Customers model);
        Task<int> update(CustomerModel model);
        Task<int> delete(int customerid);
        Task<CustomerModel> getcustomerByid(int customerid);
        Task<List<PaymentModes>> PaymentModes();
        Task<List<PaymentTypes>> PaymentTypes();
        Task<List<CustomerProductModel>> GetProductsByCustomerId(int customerId);

        Task<int> CreateProduct(TS_CustomerProducts model);
        Task<int> UpdateProduct(CustomerProductModel model);
        Task<int> DeleteCustomerProduct(int CProductId);
    }
}
