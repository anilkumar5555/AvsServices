using AVSModels.Models;
using AVSTechnoServices.Security;
using Common.Services.Implementation;
using Common.Services.Interface;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AVSTechnoServices.Controllers
{
    public class PaymentController : BaseController
    {
        private IProductTypeService _producttype;
        private IPaymentModeService _paymentmode;
        private IPaymentTypeService _paymenttype;
        public PaymentController()
        {
            _producttype = new ProductTypeService();
            _paymenttype = new PaymentTypeService();
            _paymentmode = new PaymentModeService();
        }
        public async Task<ActionResult> ProducttypeIndex()
        {
            var result = await _producttype.getAllProductTypes();
            return PartialView(result);
        }
        public async Task<ActionResult> PaymnettypeIndex()
        {
            var result = await _paymenttype.getAllPaymentTypes();
            return PartialView(result);
        }
        public async Task<ActionResult> PaymentmodeIndex()
        {
            var result = await _paymentmode.getAllPaymentModes();
            return PartialView(result);
        }
        [HttpGet]
        public async Task<ActionResult> AddProducttypes(int ProducttypeID = 0)
        {
            ProductTypes model = new ProductTypes();
            if (ProducttypeID != 0)
            {
                var producttype = await _producttype.GetProductTypesbyID(ProducttypeID);
                return PartialView(producttype);
            }
            return PartialView(model);
        }
        [HttpPost]
        public async Task<JsonResult> AddProducttypes(ProductTypes producttypes)
        {
            int result = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    producttypes.UserID = User.UserID;
                    if (producttypes.ProductTypeId == 0)
                    {
                        result = await _producttype.CreateProductTypes(producttypes);
                    }
                    else
                    {
                        result = await _producttype.UpdateProductTypes(producttypes);
                    }
                }
                return Json(new { status = true, vendorId = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, vendorId = result }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public async Task<ActionResult> AddPaymenttypes(int PaymenttypeID = 0)
        {
            PaymentTypes model = new PaymentTypes();
            if (PaymenttypeID != 0)
            {
                var paymenttype = await _paymenttype.GetPaymentTypebyID(PaymenttypeID);
                return PartialView(paymenttype);
            }
            return PartialView(model);
        }
        [HttpPost]
        public async Task<JsonResult> AddPaymenttypes(PaymentTypes paymenttypes)
        {
            int result = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    paymenttypes.UserID = User.UserID;
                    if (paymenttypes.PaymentTypeId == 0)
                    {
                        result = await _paymenttype.CreatePaymentTypes(paymenttypes);
                    }
                    else
                    {
                        result = await _paymenttype.UpdatePaymentTypes(paymenttypes);
                    }
                }
                return Json(new { status = true, vendorId = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, vendorId = result }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public async Task<ActionResult> AddPaymentmodes(int PaymentModeID = 0)
        {
            PaymentModes model = new PaymentModes();
            if (PaymentModeID != 0)
            {
                var paymentmode = await _paymentmode.GetPaymentModebyID(PaymentModeID);
                return PartialView(paymentmode);
            }
            return PartialView(model);
        }
        [HttpPost]
        public async Task<JsonResult> AddPaymentmodes(PaymentModes paymentmodes)
        {
            int result = 0;
            try
            {

                if (ModelState.IsValid)
                {
                    paymentmodes.UserID = User.UserID;
                    if (paymentmodes.PaymentModeId == 0)
                    {
                        result = await _paymentmode.CreatePaymentModes(paymentmodes);
                    }
                    else
                    {
                        result = await _paymentmode.UpdatePaymentModes(paymentmodes);
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
        public async Task<ActionResult> DeleteProductType(int ProductTypeId)
        {
            var result = await _producttype.DeleteProductTypes(ProductTypeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> DeletePaymentType(int PaymentTypeId)
        {
            var result = await _paymenttype.DeletePaymentTypes(PaymentTypeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> DeletePaymentMode(int PaymentModeId)
        {
            var result = await _paymentmode.DeletePaymentModes(PaymentModeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}