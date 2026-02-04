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
    public class PaymentTypeService : IPaymentTypeService
    {
        private IPaymentTypes _paymentType;
        public PaymentTypeService()
        {
            _paymentType = new PaymentTypeRepository();
        }
        public async Task<int> CreatePaymentTypes(PaymentTypes model)
        {
            TS_PaymentTypes paymenttype = new TS_PaymentTypes()
            {
                PaymentTypeId = model.PaymentTypeId,
                PaymentTypeName = model.PaymentTypeName,
                IsActive = true,
                CreatedBy = model.UserID,
                CreatedOn = DateTime.Now,
            };
            var result= await _paymentType.createPaymentTypes(paymenttype);
            return result;
        }

        public async Task<int> DeletePaymentTypes(int PaymentTypeID)
        {
            var result = await _paymentType.deletePaymentTypes(PaymentTypeID);
            return result;
        }

        public async Task<List<PaymentTypes>> getAllPaymentTypes()
        {
            return await _paymentType.GetAllPaymentTypes();
        }

        public async Task<PaymentTypes> GetPaymentTypebyID(int PaymentTypeID)
        {
            PaymentTypes payment = new PaymentTypes();
            var paymenttype = await _paymentType.getPaymentTypebyID(PaymentTypeID);
            payment = new PaymentTypes
            {
                PaymentTypeId = paymenttype.PaymentTypeId,
                PaymentTypeName = paymenttype.PaymentTypeName,
                CreatedOn = paymenttype.CreatedOn ?? Utility.GetCurrentDate()
            };
            return payment;
        }

        public async Task<int> UpdatePaymentTypes(PaymentTypes model)
        {
            var result = await _paymentType.updatePaymentTypes(model);
            return result;
        }
    }
}
