using AVSModels.Models;
using Common.DBContext;
using Common.Helper;
using Common.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Implementation
{
    public class PaymentTypeRepository : IPaymentTypes
    {
        private AVSTechnoEntities _connection;
        public PaymentTypeRepository()
        {
            _connection = new AVSTechnoEntities();
        }
        public async Task<int> createPaymentTypes(TS_PaymentTypes model)
        {
            _connection.TS_PaymentTypes.Add(model);
            _connection.SaveChanges();
            var paymenttype = await _connection.TS_PaymentTypes.OrderByDescending(a => a.PaymentTypeId).Select(a => a.PaymentTypeId).FirstOrDefaultAsync();
            return paymenttype;
        }

        public async Task<int> deletePaymentTypes(int PaymentTypeId)
        {
            var result = 0;
            var payment = await _connection.TS_PaymentTypes.Where(a => a.PaymentTypeId == PaymentTypeId).FirstOrDefaultAsync();
            if(payment != null)
            {
                payment.IsActive = false;
                payment.UpdateOn =DateTime.Now;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<PaymentTypes>> GetAllPaymentTypes()
        {
            return await _connection.TS_PaymentTypes.Where(a => a.IsActive == true).Select(a => new PaymentTypes
            {
                PaymentTypeId = a.PaymentTypeId,
                PaymentTypeName = a.PaymentTypeName,
                //CreatedOn = Utility.GetCurrentDate()
        }).ToListAsync();
        }

        public async Task<TS_PaymentTypes> getPaymentTypebyID(int PaymentTypeId)
        {
            return await _connection.TS_PaymentTypes.Where(a => a.PaymentTypeId == PaymentTypeId && a.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<int> updatePaymentTypes(PaymentTypes model)
        {
            var result = 0;
            var payment = await _connection.TS_PaymentTypes.Where(a => a.PaymentTypeId == model.PaymentTypeId).FirstOrDefaultAsync();
            if(payment != null)
            {
                payment.PaymentTypeName = model.PaymentTypeName;
                payment.UpdatedBy = model.UserID;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }
    }
}
