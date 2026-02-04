using AVSModels.Models;
using Common.Repository.Implementation;
using Common.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
     public interface ICustomerProductservice
    {
        Task<List<CustomerProductModel>> getAllCustomerProducts();

        Task<int> Update(CustomerProductModel model);

        Task<int> Delete(int CProductID);
        Task<CustomerProductModel> GetCustomerProductbyId(int cProductId);
    }
}
