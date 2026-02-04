using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
    public  class CustomerProductModel
    {
        public int CProductID { get; set; }
        public int CustomerID { get; set; }
        public int ModelID { get; set; }
        public int SerialID { get; set; }
        public string SerialNumber { get; set; }
        public int UserID { get; set; }
        public int Qty { get; set; }
        public  decimal UnitPrice { get; set; }
        public decimal Totaltax { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal SellPrice { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal GrossTotal { get; set; }
        public  string   Capacity { get; set; }
        public   string   HSNCode { get; set; }
        public string ACSerialNumber { get; set; }

        public int PaymentTypeId { get; set; }
        public int ProductTypeId { get; set; }
        public int PaymentModeId { get; set; }
        public    bool IsActive { get; set; }
        public  DateTime? CreatedOn { get; set; }
        public  int CreatedBy { get; set; }
        public  DateTime UpdateOn { get; set; }
        public  int UpdatedBy { get; set; }
        public string ModelName { get; set; }
        public string ProductTypeName { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public CustomerProductTransactionModel Transaction { get; set; }

    }
}
