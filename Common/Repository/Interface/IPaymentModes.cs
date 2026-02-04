using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
     public interface IPaymentModes
    {
        Task<List<PaymentModes>> GetAllPaymentModes();
        Task<int> createPaymentModes(TS_PaymentModes model);
        Task<int> updatePaymentModes(PaymentModes model);

        Task<int> deletePaymentModes(int PaymentModeId);

        Task<TS_PaymentModes> getPaymentModebyID(int PaymentModeId);
    }
}
