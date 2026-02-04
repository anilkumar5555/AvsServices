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
    public class PaymentModeRepository : IPaymentModes
    {
        private AVSTechnoEntities _connection;
        public PaymentModeRepository()
        {
            _connection = new AVSTechnoEntities();
        }
        public async Task<int> createPaymentModes(TS_PaymentModes model)
        {
            _connection.TS_PaymentModes.Add(model);
            _connection.SaveChanges();
            var paymentmode = await _connection.TS_PaymentModes.OrderByDescending(a => a.PaymentModeId).Select(a => a.PaymentModeId).FirstOrDefaultAsync();
            return paymentmode;
        }

        public async Task<int> deletePaymentModes(int PaymentModeId)
        {
            var result = 0;
            var payment = await _connection.TS_PaymentModes.Where(a => a.PaymentModeId == PaymentModeId).FirstOrDefaultAsync();
            if (payment != null)
            {
                payment.IsActive = false;
                payment.UpdateOn = DateTime.Now;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<PaymentModes>> GetAllPaymentModes()
        {
            return await _connection.TS_PaymentModes.Where(a => a.IsActive == true).Select(a => new PaymentModes
            {
                PaymentModeId = a.PaymentModeId,
                PaymentModeName = a.PaymentModeName,
                //CreatedOn = Utility.GetCurrentDate()
            }).ToListAsync();
        }

        public async Task<TS_PaymentModes> getPaymentModebyID(int PaymentModeId)
        {
            return await _connection.TS_PaymentModes.Where(a => a.PaymentModeId == PaymentModeId && a.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<int> updatePaymentModes(PaymentModes model)
        {
            var result = 0;
            var payment = await _connection.TS_PaymentModes.Where(a => a.PaymentModeId == model.PaymentModeId).FirstOrDefaultAsync();
            if (payment != null)
            {
                payment.PaymentModeName = model.PaymentModeName;
                payment.UpdatedBy = model.UserID;
                result = await _connection.SaveChangesAsync();
            }
            return result;
        }
    }
}
