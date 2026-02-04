using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
    public class MaterialProductModel
    {
        public int ModelID { get; set; }
        public Nullable<int> ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public string MName { get; set; }
        public string MDescription { get; set; }
        public string Mcapacity { get; set; }
        public Nullable<int> MQty { get; set; }
        public Nullable<decimal> MUnitPrice { get; set; }
        public Nullable<decimal> MCGST { get; set; }
        public Nullable<decimal> MSGST { get; set; }
        public Nullable<decimal> MIGST { get; set; }
        public Nullable<decimal> MTotalPrice { get; set; }
        public Nullable<decimal> MTaxTotal { get; set; }
        public Nullable<decimal> MGrossAmount { get; set; }
        public string MSrCode { get; set; }
        public int UserId { get; set; }
    }
}
