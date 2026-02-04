using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interface
{
	public interface IReportService
	{
		List<ReportModel> GetTotalStockReport(DateTime fromDate, DateTime toDate);
	}
}
