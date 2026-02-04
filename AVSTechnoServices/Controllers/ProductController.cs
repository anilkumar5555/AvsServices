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
    public class ProductController : BaseController
    {
        private IVendorService _vendorservice;
        private IProductService _productService;
        public ProductController()
        {
            _vendorservice = new VendorService();
            _productService = new ProductService();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CreateMaterial(int materialId = 0)
        {
            try
            {   
                
                MaterialModel model = new MaterialModel();
                var vendorsList = await _vendorservice.getAllVendors();
                if (materialId != 0)
                {
                    model = await _productService.GetMaterialById(materialId);
                }
                ViewBag.vendors = vendorsList.Select(a => new { value = a.VendorID, text = a.VendorName });
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Material(int materialId = 0)
        {
            try
            {
                MaterialModel model = new MaterialModel();
                if (materialId != 0)
                {
                    model = await _productService.GetMaterialById(materialId);
                }
                else
                    model.MaterialID = materialId;

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> CreateProduct(int productId = 0, int materialId = 0)
        {
            try
            {
                ProductModel model = new ProductModel();
                model.MaterialId = materialId;
                var productTypes = await _productService.GetProductTypes();
                if (productId != 0)
                {
                    model = await _productService.GetMaterialProductById(productId);
                    if (model != null)
                    {
                        model.taxPercent = decimal.Round(model.taxPercent ?? 0, 0);
                    }
                    
                        var modelTypes = await _productService.GetProductModels(model.ProductTypeId);
                        ViewBag.modelTypes = modelTypes.Select(a => new { value = a.ModelId, text = a.ModelName });
                    
                    
                }
                ViewBag.productTypes = productTypes.Select(a => new { value = a.ProductTypeId, text = a.ProductType });
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<ActionResult> SaveMaterialProduct(MaterialModel model)
        {
            try
            {
                var result = 0;
                model.UserId = User.UserID;
                if(model.MaterialID == 0)
                {
                    result = await _productService.CreateMaterial(model);
                }
                
                return Json(new { materialId=result,status=true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { materialId = 0, status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Materials()
        {
            try
            {
                var materials = await _productService.GetMaterials();
                return View(materials);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetMaterialProducts(int materialId)
        {
            try
            {
                var materialProducts = await _productService.GetMaterialProducts(materialId);
                return PartialView("MaterialProducts", materialProducts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveProduct(ProductModel model)
        {
            try
            {
                model.UserId = User.UserID;
                var result = false;
                if (model.ProductModelId != 0)
                {
                    result = Convert.ToBoolean(await _productService.UpdateProduct(model));
                }
                else
                {
                    result = Convert.ToBoolean(await _productService.CreateProduct(model));
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> DeleteProduct(int productModelId)
        {
            try
            {
                var result = Convert.ToBoolean(await _productService.deleteProduct(productModelId, User.UserID));
                return Json(new { status = result,message = "Product deleted sucessfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetProductTypeModels(int productTypeId)
        {
            var result = await _productService.GetProductModels(productTypeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductModel(int modelId)
        {
            var productModel = await _productService.GetProductModel(modelId);
            var serialNumbers = await _productService.GetModelSerialNumbers(modelId);
            return Json( new { model = productModel, serialNumbers = serialNumbers }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> updatematerial(MaterialModel model)
        {
            try
            {
                model.UserId = User.UserID;
                var result = Convert.ToBoolean(await _productService.Updatematerial(model));
                return Json(new { status = result, message = "MaterialInformation updated sucessfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}