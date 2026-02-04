using Common.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AVSModels.Models;
using Common.Repository.Interface;

namespace Common.Repository.Implementation
{
	public class ReportRepository: IReport
	{

		public List<ReportModel> GetTotalStockReport(DateTime fromDate, DateTime toDate)
		{
			List<ReportModel> report = new List<ReportModel>();
			using (AVSTechnoEntities _conn = new AVSTechnoEntities())
			{
				var _report =  _conn.usp_ProductSalePurChaseReport(fromDate, toDate).ToList();
				if (_report.Count > 0)
				{
					report = _report.Select(a => new ReportModel
					{
						ModelId = a.ModelId??0,
						ModelName = a.ModelName,
						ProductTypeId = a.ProductTypeId,
						ProductTypeName = a.ProductTypeName,
						TotalProductTransaction = a.TotalProducts ?? 0,
						TotalPurchaseQty = a.TotalPurchaseQty ?? 0,
						TotalSalesTransaction = a.TotalSalesProducts ?? 0,
						TotalSalesQty = a.TotalSalesQty ?? 0
					}).ToList();
				}
			}
			return report;
		}

		public List<ReportModel> GetAllSalesReports()
		{
			var products = GetSalesReport();

			//DatailReport
			var _dailyProducts = products.Where(a => a.CreatedOn.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString())).ToList();
			List<ReportModel> _reportModels = _dailyProducts.Select(a => new ReportModel 
			{
				 CustomerId=a.CustomerID,
				 CustomerTransactionId = a.CProductID,
				 ModelId = a.ModelID,
				 ModelName = a.ModelName,
				 ProductTypeId = a.ProductTypeId,
				 ProductTypeName = a.ProductTypeName,
				 ReportId = 1,
				 ReportName = "Daily Report",
				 ReportTitle = "Today Report",
				 ReportType = "pie",
			}).ToList();
			return _reportModels;
		}

		private List<CustomerProductModel> GetSalesReport()
		{
			using (AVSTechnoEntities _conn = new AVSTechnoEntities())
			{
				var product = (from cp in _conn.TS_CustomerProducts
							   join m in _conn.TS_Models on cp.ProductModelId equals m.ProductModelId
							   join pt in _conn.TS_ProductTypes on m.ProductTypeId equals pt.ProductTypeId
							   where cp.IsActive.Value && m.IsActive.Value && pt.IsActive.Value
							   select new CustomerProductModel
							   {
								   ProductTypeId = pt.ProductTypeId,
								   ProductTypeName = pt.ProductTypeName,
								   ModelID = m.ProductModelId,
								   ModelName = m.ModelName,
								   CProductID = cp.CProductID,
								   CustomerID = cp.CustomerID ?? 0,
								   CreatedOn = cp.CreatedOn,
								   Qty = cp.Qty??0
							   }).ToList();
				return product;
			}
		}


	}
}
