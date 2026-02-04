using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
	public class ReportModel
	{
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int CustomerTransactionId { get; set; }
        public int CustomerId { get; set; }
        public int TotalProductTransaction { get; set; }
        public int TotalPurchaseQty { get; set; }
        public int TotalSalesTransaction { get; set; }
        public int TotalSalesQty { get; set; }
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportTitle { get; set; }
        public string ReportType { get; set; }
    }
}
