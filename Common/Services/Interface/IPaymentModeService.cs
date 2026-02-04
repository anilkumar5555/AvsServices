using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
   public interface IPaymentModeService
    {
        Task<List<PaymentModes>> getAllPaymentModes();
        Task<int> CreatePaymentModes(PaymentModes model);
        Task<int> UpdatePaymentModes(PaymentModes model);

        Task<int> DeletePaymentModes(int PaymentModeID);
        Task<PaymentModes> GetPaymentModebyID(int PaymentModeID);
    }
}
