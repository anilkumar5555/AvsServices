using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
    public class MaterialModel
    {
        public int MaterialID { get; set; }
        public Nullable<int> VendorId { get; set; }
        public string VendorName { get; set; }
        public string MaterialNo { get; set; }
        public string MaterialName { get; set; }
        public string InvoiceNo { get; set; }
        public string ShippingDate { get; set; }
        public string ReceivedDate { get; set; }
        public DateTime? SDate { get; set; }
        public DateTime? RDate { get; set; }
        public string MaterialDescription { get; set; }
        public ProductModel product { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class ProductModel
    {
        public int MaterialId { get; set; }
        public int ProductModelId { get; set; } 
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductType { get; set; }
        public string ModelName { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> TotalTaxPercent { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> TotalTax { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> TotalBalance { get; set; }
        public string SerialNumber { get; set; }
        public string ACSerialNumber { get; set; }
        public string HSNCode { get; set; }
        public string Capacity { get; set; }
        public Nullable<int> Weight { get; set; }
        public string Color { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int SerialNoId { get; set; }
        public int ModelId { get; set; }
        public decimal? taxPercent { get; set; }
    }
}
