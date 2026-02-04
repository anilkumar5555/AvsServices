using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
   public class PaymentModes
    {
        public int PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime UpdateOn { get; set; }
        public int UpdatedBy { get; set; }
    }
}
