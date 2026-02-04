using AVSModels.Models;
using Common.DBContext;
using Common.Helper;
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
    public class PaymentModeService : IPaymentModeService
    {
        private IPaymentModes _paymentmode;
        public PaymentModeService()
        {
            _paymentmode = new PaymentModeRepository();
        }
        public async Task<int> CreatePaymentModes(PaymentModes model)
        {
            TS_PaymentModes paymentmode = new TS_PaymentModes()
            {
                PaymentModeId = model.PaymentModeId,
                PaymentModeName = model.PaymentModeName,
                IsActive = true,
                CreatedBy = model.UserID,
                CreatedOn = DateTime.Now,
            };
            var result = await _paymentmode.createPaymentModes(paymentmode);
            return result;
        }

        public async Task<int> DeletePaymentModes(int PaymentModeID)
        {
            var result = await _paymentmode.deletePaymentModes(PaymentModeID);
            return result;
        }

        public async Task<List<PaymentModes>> getAllPaymentModes()
        {
            return await _paymentmode.GetAllPaymentModes();
        }

        public async Task<PaymentModes> GetPaymentModebyID(int PaymentModeID)
        {
            PaymentModes payment = new PaymentModes();
            var paymenttype = await _paymentmode.getPaymentModebyID(PaymentModeID);
            payment = new PaymentModes
            {
                PaymentModeId = paymenttype.PaymentModeId,
                PaymentModeName = paymenttype.PaymentModeName,
                CreatedOn = paymenttype.CreatedOn ?? Utility.GetCurrentDate()
            };
            return payment;
        }

        public async Task<int> UpdatePaymentModes(PaymentModes model)
        {
            var result = await _paymentmode.updatePaymentModes(model);
            return result;
        }
    }
}
