using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
	public interface IReport
	{
		List<ReportModel> GetTotalStockReport(DateTime fromDate, DateTime toDate);
	}
}
