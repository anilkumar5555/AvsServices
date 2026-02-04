using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
    public class VendorModel
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GSTIN { get; set; }
        public String Fax { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }
        public System.DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime UpdateOn { get; set; }
        public int UpdatedBy { get; set; }
    }
}
