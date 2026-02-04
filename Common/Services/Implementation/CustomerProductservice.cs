using AVSModels.Models;
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
    public class CustomerProductservice : ICustomerProductservice
    {
        private ICustomerProduct _customerProduct;
        private TimeZoneInfo INDIAN_ZONE;
        public CustomerProductservice()
        {
            _customerProduct = new CustomerProductRepository();
            this.INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        }

        public async Task<int> Delete(int CProductID)
        {
            var result = await _customerProduct.delete(CProductID);
            return result;
        }

        public async Task<List<CustomerProductModel>> getAllCustomerProducts()
        {
            List<CustomerProductModel> customerProducts = new List<CustomerProductModel>();
           var Products = await _customerProduct.GetAllCustomerProducts();
            customerProducts = Products.Select(a => new CustomerProductModel
            {
                CProductID = a.CProductID,
                CGST = a.CGST??0,
                IGST = a.IGST??0,
                SGST = a.SGST??0,
                Discount = a.Discount??0,
                GrossTotal = a.GrossTotal??0,
                NetTotal = a.NetTotal??0,
               Qty = a.Qty??0,
               SellPrice = a.SellPrice??0,
               UnitPrice = a.UnitPrice??0,
                CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.INDIAN_ZONE)

            }).ToList();
            return customerProducts;
        }

        public async Task<CustomerProductModel> GetCustomerProductbyId(int cProductId)
        {
            return await _customerProduct.GetCustomerProductById(cProductId);
        }

        public async Task<int> Update(CustomerProductModel model)
        {
            var result = await _customerProduct.update(model);
            return result;
        }
    }
}
