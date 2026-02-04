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
    public class CustomerController : BaseController
    {
        private ICustomerservice _customerservice;
        private ICustomerProductservice _customerproductservice;
        private IProductService _productService;
        private ICustomerProductTransactionService _customerProductTransactionService;  

        public CustomerController()
        {
            _customerservice = new Customerservice();
            _customerproductservice = new CustomerProductservice();
            _productService = new ProductService();
            _customerProductTransactionService = new CustomerProductTransactionService();

        }
        // GET: Customer
        public ActionResult Index()
        {
            var result = _customerservice.GetallCustomers();
            return PartialView(result);
        }

        [HttpGet]
        public async Task<ActionResult> CreateCustomer(int customerID = 0)
        {
            CustomerModel customer = new CustomerModel();
            if (customerID != 0)
            {
                customer = await _customerservice.GetCustomerById(customerID);
            }
            return PartialView(customer);
        }

        public ActionResult Customer(int customerID = 0)
        {
            CustomerModel customer = new CustomerModel();
            customer.CustomerID = customerID;
            return View(customer);
        }


        [HttpPost]
        public async Task<JsonResult> CreateCustomer(CustomerModel model)
        {
            int result = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.userid = User.UserID;
                      
                    if (model.CustomerID == 0)
                    {
                        result = await _customerservice.createcustomer(model);
                    }
                    else
                    {
                        result = await _customerservice.updatecustomer(model);
                    }
                }
                return Json(new { status = true, customerid = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, customerid = result }, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public async Task <ActionResult> CreateCustomerProduct(int customerId =0, int cProductId = 0)
        {
            try
            {
                var productTypes = await _productService.GetProductTypes();
                ViewBag.productTypes = productTypes.Select(a => new { value = a.ProductTypeId, text = a.ProductType });

                var PaymentTypes = await _customerservice.PaymentTypes();
                ViewBag.PaymentTypes = PaymentTypes.Select(a => new { value = a.PaymentTypeId, text = a.PaymentTypeName });

                var PaymentModes = await _customerservice.PaymentModes();
                ViewBag.PaymentModes = PaymentModes.Select(a => new { value = a.PaymentModeId, text = a.PaymentModeName });

                ViewBag.modelTypes = Enumerable.Empty<SelectListItem>();

                CustomerProductModel model = new CustomerProductModel();
                if (cProductId != 0)
                {
                    model = await _customerproductservice.GetCustomerProductbyId(cProductId);
                    if (model.ProductTypeId != 0)
                    {
                        var modelTypes = await _productService.GetProductModels(model.ProductTypeId);
                        ViewBag.modelTypes = modelTypes.Select(a => new { value = a.ModelId, text = a.ModelName });
                    }
                }
                else
                {
                    model.CustomerID = customerId;
                }
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
        
        [HttpPost]

        public async Task<ActionResult> CreateCustomerProduct(CustomerProductModel model)
        {
            int result = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserID = User.UserID;

                    if (model.CProductID == 0)
                    {
                        result = await _customerservice.createProduct(model);
                    }
                    else
                    {
                        result = await _customerservice.updateProduct(model);
                    }
                }
                return Json(new { status = true, customerid = model.CustomerID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, customerid = model.CustomerID }, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public ActionResult CustomerProductTransaction(int  cpTransactionid = 0, int cProductId = 0)
        {
            CustomerProductTransactionModel  model = new CustomerProductTransactionModel();
            if (cpTransactionid != 0)
            {
                 model = _customerProductTransactionService.GetCustomerProductTransactionbyId(cpTransactionid).Result;
            }
            else {
                 model.CProductID = cProductId;
            }
            return PartialView(model); 
        }

        [HttpPost]
        public async Task<ActionResult> SaveCustomerProductTransaction(CustomerModel model)
        {
            try
            {
                model.userid = User.UserID;
                var result = await _customerservice.createcustomer(model);
                return Json(new {customerId = result,status = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { customerId = 0, status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> CustomerProducts(int customerId)
        {
            List<CustomerProductModel> _products = new List<CustomerProductModel>();
            _products = await _customerservice.GetProductsByCustomerId(customerId);
            ViewBag.customerId = customerId;
            return PartialView(_products);
        }



        [HttpGet]
        public async Task<ActionResult>  CustomerBill(int customerId = 0)
        {
            var result = await _customerProductTransactionService.getCustomerwithProductByCustomerId(customerId);
            return View(result); 
        }
        public async Task<ActionResult> GetmodelsByProductType(int productTypeId)
        {
            var models = await _productService.GetmodelByProductType(productTypeId);
            return Json(models, JsonRequestBehavior.AllowGet); 
        }
        public async Task<ActionResult> GetModelDetailsById(int  modelId)
        {
            var model = await _productService.GetMaterialProductById(modelId);
            return Json(model, JsonRequestBehavior.AllowGet); 
        }

        public async Task<ActionResult>  DeleteCustomer(int customerId)
        {
            var result = await _customerservice.deletecustomer(customerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerInvoice(int customerId = 0)
        {
            return View();
        }

        public async Task<ActionResult> DeleteCustomerProduct(int CProductId)
        {
            try
            {
                var result = Convert.ToBoolean(await _customerservice.deleteCustomerProduct(CProductId));
                return Json(new { status = result, message = "Product deleted sucessfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public async Task<ActionResult> updateCustomer(CustomerModel model)
        {
            try
            {
                model.userid = User.UserID;
                var result = Convert.ToBoolean(await _customerservice.updatecustomer(model));
                return Json(new { status = result, message = "CustomerInformation updated sucessfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}