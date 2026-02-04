using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
   public  class CustomerProductTransactionModel
    {
        public int CPTransactionID { get; set; }
        public int CProductID { get; set; }
        public  decimal NewPayment { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal  Totaltax { get; set; }

        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public int PaymentTypeId { get; set; }
        public  bool IsActive { get; set; }
        public  DateTime CreatedOn { get; set; }
        public  int CreatedBy { get; set; }
        public  DateTime UpdateOn { get; set; }
        public  int UpdatedBy { get; set; }
        public int UserID { get; set; }

    }
}
