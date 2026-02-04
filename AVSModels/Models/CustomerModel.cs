using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
    public  class CustomerModel
    {
        public int  CustomerID { get; set; }
        public  string  CustomerName { get; set; }
        public  string  Address { get; set; }
        public string GSTNO { get; set; }
        public  string  PhoneNumber { get; set; }
        public  string   Email { get; set; }
        public string StateCode { get; set; }
        public string State { get; set; }
        public string SCustomerName { get; set; }
        public string SAddress { get; set; }
        public string SGSTNO { get; set; }
        public string SPhoneNumber { get; set; }
        public string SEmail { get; set; }
        public string SStateCode { get; set; }
        public string SState { get; set; }
        public  bool IsActive { get; set; }
        public bool IsSameAs  { get; set; }
        public   string CreatedOn { get; set; }
        public  int CreatedBy { get; set; }
        public  DateTime UpdateOn { get; set; }
        public  int UpdatedBy { get; set; }
        public int userid { get; set; }
        public CustomerProductModel Product { get; set; }
         
    }
    
}
