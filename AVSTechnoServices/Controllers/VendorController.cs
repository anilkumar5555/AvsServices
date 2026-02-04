using AVSModels.Models;
using AVSTechnoServices.Security;
using Common.Services.Implementation;
using Common.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AVSTechnoServices.Controllers
{
    [Authorize]
    public class VendorController : BaseController
    {
        private IVendorService _vendorservice;
        public VendorController()
        {
            _vendorservice = new VendorService();
        }

        public async Task<ActionResult> Index()
        {
            var result = await _vendorservice.getAllVendors();
            return PartialView(result); 
        }
        public ActionResult Details(int vendorId)
        {
            var result = _vendorservice.GetVendorbyID(vendorId);
            return PartialView(result);
        }
        [HttpGet]
        public  async Task<ActionResult> AddVendor(int VendorID = 0)
        {
            VendorModel model = new VendorModel();
            if (VendorID != 0)
            {
                var vendor = await _vendorservice.GetVendorbyID(VendorID);
                return PartialView(vendor);
            }
            return PartialView(model);
        }
        [HttpPost]
        public async Task<JsonResult> Addvendor(VendorModel vendor)
        {
            int result = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    vendor.UserID = User.UserID;
                    if (vendor.VendorID == 0)
                    {
                        result = await _vendorservice.Create(vendor);
                    }
                    else
                    {
                        result = await _vendorservice.Update(vendor);
                    }
                }
                return Json(new { status = true, vendorId = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
                {
                return Json(new { status = false, vendorId = result }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<ActionResult> DeleteVendor(int VendorID)
        {
            var result = await _vendorservice.Delete(VendorID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}