using Common.Services.Implementation;
using Common.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVSTechnoServices.Controllers
{
    public class ReportController : Controller
    {
        private IReportService _reportService;
        public ReportController()
        {
            _reportService = new ReportService();
        }

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult StockReport()
        {
            var report = _reportService.GetTotalStockReport(DateTime.Now, DateTime.Now);
            return View(report);
        }

        public ActionResult ProductModelReport(int productId)
        {
            var report = _reportService.GetTotalStockReport(DateTime.Now, DateTime.Now).Where(a=>a.ProductTypeId == productId);
            return PartialView(report);
        }
    }
}