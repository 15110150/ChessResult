using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using ECOM.Models;
using ECOM.Infrastructure.Utilities;

using ECOM.Core.Interfaces;
using ECOM.Services;
using System.Linq;
using ECOM.Entities.Models;
using ECOM.Infrastructure.ECOMException;

using System.Configuration;

namespace ECOM.Web.Utilities.App
{
    public class GridViewUtils
    {
        #region Grid View Export

        public enum GridViewExportFormat { None, Pdf, Xls, Xlsx, Rtf, Csv }
        public delegate ActionResult GridViewExportMethod(GridViewSettings settings, object dataObject);

        static Dictionary<GridViewExportFormat, GridViewExportMethod> exportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> ExportFormatsInfo
        {
            get
            {
                if (exportFormatsInfo == null)
                    exportFormatsInfo = CreateExportFormatsInfo();
                return exportFormatsInfo;
            }
        }

        public static List<ExportFormatConfig> ExportFormatsDefault
        {
            get {
                return new List<ExportFormatConfig>() { 
                    new ExportFormatConfig(){Id = GridViewExportFormat.Pdf.ToString(), Name = "Pdf File"},
                    new ExportFormatConfig(){Id = GridViewExportFormat.Xls.ToString(), Name = "Xls File"},
                    new ExportFormatConfig(){Id = GridViewExportFormat.Xlsx.ToString(), Name = "Xlsx File"},
                    new ExportFormatConfig(){Id = GridViewExportFormat.Rtf.ToString(), Name = "Rtf File"},
                    new ExportFormatConfig(){Id = GridViewExportFormat.Csv.ToString(), Name = "Csv File"},
                };
            }
        }


        static IDictionary Context { get { return System.Web.HttpContext.Current.Items; } }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Pdf, GridViewExtension.ExportToPdf },
                {
                    GridViewExportFormat.Xls,
                    (settings, data) => GridViewExtension.ExportToXls(settings, data, new XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                },
                { 
                    GridViewExportFormat.Xlsx,
                    (settings, data) => GridViewExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                },
                { GridViewExportFormat.Rtf, GridViewExtension.ExportToRtf },
                {
                    GridViewExportFormat.Csv,
                    (settings, data) => GridViewExtension.ExportToCsv(settings, data, new CsvExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                }
            };
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> dataAwareExportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> DataAwareExportFormatsInfo
        {
            get
            {
                if (dataAwareExportFormatsInfo == null)
                    dataAwareExportFormatsInfo = CreateDataAwareExportFormatsInfo();
                return dataAwareExportFormatsInfo;
            }
        }
        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateDataAwareExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Xls, GridViewExtension.ExportToXls },
                { GridViewExportFormat.Xlsx, GridViewExtension.ExportToXlsx },
                { GridViewExportFormat.Csv, GridViewExtension.ExportToCsv }
            };
        }

        public static string GetExportButtonTitle(GridViewExportFormat format)
        {
            if (format == GridViewExportFormat.None)
                return string.Empty;
            return string.Format("Export to {0}", format.ToString().ToUpper());
        }
        #endregion

        #region Grid View Config
        public static GridViewConfig GetGridViewConfig(string controllerName,IReportService reportService = null)
        {
            string gridViewName = controllerName + "_GridView";

            GridViewConfig grdConfig = new GridViewConfig()
            {
                GridViewName = gridViewName,
                CallbackRouteValues = new { Controller = controllerName, Action = "GridView" },
                CustomActionRouteValues = new { Controller = controllerName, Action = "GetByDateRange" },
                Columns = GetColumnsListByController(gridViewName, reportService),
                ExportsFormat = ExportFormatsDefault,
                ImageFile = GetImageFile(controllerName, reportService)
            };

            return grdConfig;
        }
        public static GridViewConfig GetGridViewConfig(string controllerName, string gridViewName, IReportService reportService = null)
        {
            string actionName = GetActionName(gridViewName);

            GridViewConfig grdConfig = new GridViewConfig()
            {
                GridViewName = gridViewName,
                CallbackRouteValues = new { Controller = controllerName, Action = actionName },
                CustomActionRouteValues = new { Controller = controllerName, Action = "GetByDateRange" },
                Columns = GetColumnsListByController(controllerName, gridViewName, reportService),
                ExportsFormat = ExportFormatsDefault,
                ImageFile = GetImageFile(controllerName, reportService)
            };
            
            return grdConfig;
        }
        private static string GetImageFile(string controllerName, IReportService reportService)
        {

            return reportService==null?"": reportService.GetImageFile(GetReportCode(controllerName));
          
        }
        private static List<GridViewColumn> GetOfficeListColumns()
        {

            return new List<GridViewColumn>() {
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Code#"},
                new GridViewColumn(){ColumnName = "PartnerName", DisplayText = "Name"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "MobilePhone", DisplayText = "Cell Phone"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
            };
        }
        private static List<GridViewColumn> GetOfficeListColumnsAdmin()
        {

            return new List<GridViewColumn>() {
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Code#"},
                new GridViewColumn(){ColumnName = "PartnerName", DisplayText = "Name"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "MobilePhone", DisplayText = "Cell Phone"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
                new GridViewColumn(){ColumnName = "ActiveStr", DisplayText = "Active"},
            };
        }
        private static List<GridViewColumn> GetBranchListColumns()
        {

            return new List<GridViewColumn>() { 
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Code"},
                new GridViewColumn(){ColumnName = "PartnerName", DisplayText = "Branch Name"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "MobilePhone", DisplayText = "Cell Phone"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
            };
        }
        private static List<GridViewColumn> GetBranchListColumnsAdmin()
        {

            return new List<GridViewColumn>() {
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Code"},
                new GridViewColumn(){ColumnName = "PartnerName", DisplayText = "Branch Name"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "MobilePhone", DisplayText = "Cell Phone"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
                new GridViewColumn(){ColumnName = "ActiveStr", DisplayText = "Active"},
            };
        }
        private static List<GridViewColumn> GetAgentListColumns()
        {

            return new List<GridViewColumn>() {
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Code"},
                new GridViewColumn(){ColumnName = "PartnerName", DisplayText = "Agent Name"},
                new GridViewColumn(){ColumnName = "ParentCode", DisplayText = "Branch"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "MobilePhone", DisplayText = "Cell Phone"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
            };
        }
        private static List<GridViewColumn> GetAgentListColumnsAdmin()
        {

            return new List<GridViewColumn>() {
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Code"},
                new GridViewColumn(){ColumnName = "PartnerName", DisplayText = "Agent Name"},
                new GridViewColumn(){ColumnName = "ParentCode", DisplayText = "Branch"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "MobilePhone", DisplayText = "Cell Phone"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
                new GridViewColumn(){ColumnName = "ActiveStr", DisplayText = "Active"},
            };
        }
        private static List<GridViewColumn> GetSubAgentListColumns()
        {

            return new List<GridViewColumn>() {
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Sub-Agent Code"},
                new GridViewColumn(){ColumnName = "AgentCode", DisplayText = "Agent"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "SelectCode", DisplayText = "ID or Passport#"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
            };
        }       
        private static List<GridViewColumn> GetCustomerListColumns()
        {

            return new List<GridViewColumn>() {
                new GridViewColumn(){ColumnName = "PartnerCode", DisplayText = "Customer Code"},
                new GridViewColumn(){ColumnName = "AgentCode", DisplayText = "Agent"},
                new GridViewColumn(){ColumnName = "SubAgentCode", DisplayText = "Sub-Agent"},
                new GridViewColumn(){ColumnName = "ContactName", DisplayText = "Contact Name"},
                new GridViewColumn(){ColumnName = "SelectCode", DisplayText = "ID or Passport#"},
                new GridViewColumn(){ColumnName = "WorkPhone", DisplayText = "Work Phone"},
            };
        }
        
        private static List<GridViewColumn> GetUserColumns()
        {
            
            return new List<GridViewColumn>() { 
                new GridViewColumn(){ColumnName = "UserName", DisplayText = "User Name"},
                new GridViewColumn(){ColumnName = "FullName", DisplayText = "Full Name"},
                new GridViewColumn(){ColumnName = "Email", DisplayText = "Email"},
                new GridViewColumn(){ColumnName = "Phone", DisplayText = "Phone"},
                new GridViewColumn(){ColumnName = "DepartmentName", DisplayText = "Department"},
                                            
                new GridViewColumn(){ColumnName = "StrActive", DisplayText = "Status"},
            };
        }

        private static List<GridViewColumn> GetDestinationColumns()
        {
            return new List<GridViewColumn>() { 
                new GridViewColumn(){ColumnName = "ReportGroupName", DisplayText = "Report Group"},
                new GridViewColumn(){ColumnName = "ReportName", DisplayText = "Report Name"},
                new GridViewColumn(){ColumnName = "Path", DisplayText = "Path"},                
            };
        }

        private static List<GridViewColumn> GetColumnsListByController(string controllerName, IReportService reportService)
        {
            string reportCode = string.Empty;
            List<GridViewColumn> result = new List<GridViewColumn>();

            if (reportService != null)
            {

                result = reportService.GetColumnsInfo(GetReportCode(controllerName)).ToList();
            }
            else
            {
                switch (controllerName)
                {                    
                    default:
                        break;
                }
            }
            return result;
        }
        private static List<GridViewColumn> GetColumnsListByController(string controllerName,string  gridViewName,IReportService reportService)
        {
            ECOMUserModel_Login userlogin = HttpSession.GetFromSession<ECOMUserModel_Login>();
            string reportCode = string.Empty;
            List<GridViewColumn> result = new List<GridViewColumn>();

            if (reportService != null)
            {
                result = reportService.GetColumnsInfo(GetReportCode(controllerName)).ToList();
            }
            else
            {
                switch (gridViewName)
                {
                    case "grdOffice":
                        if (userlogin.RoleID == (int)EnumUtility.ROLE.admin ||
                            userlogin.RoleID == (int)EnumUtility.ROLE.supervisor)
                            result = GetOfficeListColumnsAdmin();
                        else
                            result = GetOfficeListColumns();
                        break;
                    case "grdBranch":
                        if (userlogin.RoleID == (int)EnumUtility.ROLE.admin ||
                            userlogin.RoleID == (int)EnumUtility.ROLE.supervisor)
                            result = GetBranchListColumnsAdmin();
                        else
                            result = GetBranchListColumns();
                        break;
                    case "grdAgent":
                        if (userlogin.RoleID == (int)EnumUtility.ROLE.admin ||
                           userlogin.RoleID == (int)EnumUtility.ROLE.supervisor)
                            result = GetAgentListColumnsAdmin();
                        else
                            result = GetAgentListColumns();
                        break;
                    case "grdSubAgent":
                        result = GetSubAgentListColumns();
                        break;
                    case "grdCustomer":
                        result = GetCustomerListColumns();
                        break;
                        
                    default:
                        break;
                }
            }
            return result;
        }
        private static string GetActionName(string gridName)
        {
            string actionName = "";
            switch (gridName)
            {
                case "grdOffice":
                    actionName = "GridViewOffice";
                    break;
                case "grdBranch":
                    actionName = "GridViewBranch";
                    break;
                case "grdAgent":
                    actionName = "GridViewAgent";
                    break;
                case "grdSubAgent":
                    actionName = "GridViewSubAgent";
                    break;
                case "grdCustomer":
                    actionName = "GridViewCustomer";
                    break;
                default:
                    break;
            }
            return actionName;
        }
        #endregion

        public static string GetReportCode(string controllerName)
        {
            string result = "";
            switch (controllerName)
            {   
                default:
                    break;
            }
            return result;
        }

        public static string GetFileFormat(string fileFormat, DateTime toDate, string dailyMonthly, string extMonthly, DateTime fromDate, string extMonthlyFormat)
        {
            try
            {
                string str = fileFormat;
                switch (dailyMonthly)
                {
                    case "All":
                        str = GetFileFormat(fileFormat, toDate);
                        break;
                    case "D":
                        str = GetFileFormat(fileFormat, fromDate);
                        break;
                    case "M":
                        str = GetFileFormatByMonthly(fileFormat, fromDate, extMonthly, extMonthlyFormat);
                        break;
                    default:
                        break;
                }
                return str;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string GetFileFormat(string fileFormat, DateTime dtNow)
        {
            string str = fileFormat;
            string[] arr = ConfigurationManager.AppSettings["DateFormat"].Split(';');
            foreach (string d in arr)
            {
                str = str.Replace(d, dtNow.ToString(d));
            }
            return str;
        }

        static private string GetFileFormatByMonthly(string fileFormat, DateTime fromDate, string extMonthly, string extMonthlyFormat)
        {
            try
            {
                string str = fileFormat;
                //if is Daily mode, update datetime to format as configuration ------------
                string[] arr = ConfigurationManager.AppSettings["DateFormat"].Split(';');
                DateTime dtCurr = fromDate;
                string d_new = string.Empty;
                if (!string.IsNullOrEmpty(extMonthly))
                {
                    //d_new = "yyyyMMdd"; //CR 11662 - Report Name: YYYYMMDD: the end date of previous month
                    d_new = extMonthlyFormat;
                    //find and change to last date with new format
                    //Get end of month from fromDate
                    dtCurr = new DateTime(fromDate.Year, fromDate.Month, DateTime.DaysInMonth(fromDate.Year,
                                                        fromDate.Month));
                   
                }
                foreach (string d in arr)
                {
                    str = str.Replace(d, dtCurr.ToString(d_new == string.Empty ? d : d_new));
                }
                if (!string.IsNullOrEmpty(extMonthly)) //MonthlyExtension has 4 value: empty, 'none', '.mgr', '.mgr;.mvj'
                {
                    //for report T_07_Inventory_v1_2, change name will be difference
                    if (extMonthly == "00002.tsv")
                    {
                        str = str.Replace("00001.tsv", extMonthly);
                    }
                    else
                    {
                        string extDaily = str.Substring(str.IndexOf('.'));
                        str = str.Replace(extDaily, extMonthly.Split(';')[0]); //get the first ext ONLY
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
    }
}