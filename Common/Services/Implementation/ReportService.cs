using AVSModels.Models;
using Common.Repository.Implementation;
using Common.Repository.Interface;
using Common.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Implementation
{
	public class ReportService : IReportService
	{

		private IReport _report;
		
		public ReportService()
		{
			_report = new ReportRepository();
			
		}
		public List<ReportModel> GetTotalStockReport(DateTime fromDate, DateTime toDate)
		{
			return _report.GetTotalStockReport(fromDate,toDate);
		}
	}
}
