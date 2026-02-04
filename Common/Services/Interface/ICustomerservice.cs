using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
    public interface ICustomerservice
    {
        List<CustomerModel> GetallCustomers();
       Task<int> createcustomer(CustomerModel model);
        Task<int>  updatecustomer(CustomerModel model);
        Task <int> deletecustomer(int CustomerID);
       Task<CustomerModel> GetCustomerById(int customerid);
        Task<List<PaymentTypes>> PaymentTypes();

        Task<List<PaymentModes>> PaymentModes();
        Task<List<CustomerProductModel>> GetProductsByCustomerId(int customerId);

        Task<int> createProduct(CustomerProductModel model);
        Task<int> updateProduct(CustomerProductModel model);
        Task<int> deleteCustomerProduct(int CProductId);


    }
}
