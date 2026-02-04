using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
     public interface ICustomerProduct
    {
        Task<List<TS_CustomerProducts>> GetAllCustomerProducts();
        Task<int> update(CustomerProductModel model);
        Task<int> delete(int CProductID);
        Task<CustomerProductModel> GetCustomerProductById(int cProductId);
    }
}
