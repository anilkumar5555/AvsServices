using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
   public interface IPaymentTypeService
    {
        Task<List<PaymentTypes>> getAllPaymentTypes();
        Task<int> CreatePaymentTypes(PaymentTypes model);
        Task<int> UpdatePaymentTypes(PaymentTypes model);

        Task<int> DeletePaymentTypes(int PaymentTypeID);
        Task<PaymentTypes> GetPaymentTypebyID(int PaymentTypeID);
    }
}
