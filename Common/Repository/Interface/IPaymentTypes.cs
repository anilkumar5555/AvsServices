using AVSModels.Models;
using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
    public interface IPaymentTypes
    {
        Task<List<PaymentTypes>> GetAllPaymentTypes();
        Task<int> createPaymentTypes(TS_PaymentTypes model);
        Task<int> updatePaymentTypes(PaymentTypes model);

        Task<int> deletePaymentTypes(int PaymentTypeId);

        Task<TS_PaymentTypes> getPaymentTypebyID(int PaymentTypeId);
    }
}
