using ECOM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ECOM.Web.Utilities.App
{
    public static class AppUtils
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            //Create the columns in the DataTable
            foreach (PropertyInfo pi in pia)
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            //Populate the table
            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                foreach (PropertyInfo pi in pia)
                {
                    dr[pi.Name] = pi.GetValue(item, null);
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        static public DataTable BindToDT<T>(RPT_Report infoRpt, List<T> results, string columnsName)
        {
            try
            {
                string strtmp = string.Empty;
                DataTable dt = CreateDTExp(infoRpt, columnsName);
                foreach (T info in results)
                {
                    DataRow dr = dt.NewRow();
                    //System.Reflection.PropertyInfo[] pros = typeof(T).GetProperties();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        try
                        {
                            System.Reflection.PropertyInfo infoPro = info.GetType().GetProperty(dc.ColumnName);
                            if (infoPro == null) //if cannot find this column in tmp table
                            {
                                // loggerSum.Info(string.Format("- There is no column '{0}' in the temporary table '{1}'", dc.ColumnName, infoRpt.ReportCode));
                            }
                            else
                            {
                                if (dc.ColumnName == "ETA_date") strtmp = "beak here";
                                object obj = info.GetType().GetProperty(dc.ColumnName).GetValue(info, null);
                                if (dc.DataType == typeof(DateTime))
                                {
                                    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                                        obj = System.DBNull.Value;
                                }
                                dr[dc] = obj;
                            }
                        }
                        catch (Exception ex1)
                        {

                            throw ex1;
                        }
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public DataTable BindToDT<T>(RPT_Report infoRpt, List<T> results)
        {
            try
            {
                string strtmp = string.Empty;
                DataTable dt = CreateDTExp(infoRpt);
                foreach (T info in results)
                {
                    DataRow dr = dt.NewRow();
                    //System.Reflection.PropertyInfo[] pros = typeof(T).GetProperties();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        try
                        {
                            System.Reflection.PropertyInfo infoPro = info.GetType().GetProperty(dc.ColumnName);
                            if (infoPro == null) //if cannot find this column in tmp table
                            {
                               // loggerSum.Info(string.Format("- There is no column '{0}' in the temporary table '{1}'", dc.ColumnName, infoRpt.ReportCode));
                            }
                            else
                            {
                                if (dc.ColumnName == "ETA_date") strtmp = "beak here";
                                object obj = info.GetType().GetProperty(dc.ColumnName).GetValue(info, null);
                                if (dc.DataType == typeof(DateTime))
                                {
                                    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                                        obj = System.DBNull.Value;
                                }
                                dr[dc] = obj;
                            }
                        }
                        catch (Exception ex1)
                        {
                           
                            throw ex1;
                        }
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static private DataTable CreateDTExp(RPT_Report infoRpt)
        {
            try
            {
                DataTable dt = new DataTable();
                var result = infoRpt.RPT_ReportColumn.Where(x => x.Enable == true).OrderBy(x => x.OrderBy);
                foreach (RPT_ReportColumn infoRptCl in result)
                {
                    try
                    {
                        DataColumn dc = new DataColumn(infoRptCl.ColumnName, System.Type.GetType(infoRptCl.ColumnType));
                        dc.Caption = infoRptCl.ColumnFormat; //Namespace
                        dt.Columns.Add(dc);
                    }
                    catch (Exception ex1)
                    {
                       
                        throw ex1;
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static private DataTable CreateDTExp(RPT_Report infoRpt, string columnsName)
        {
            try
            {
                DataTable dt = new DataTable();
                string[] columns = columnsName.Split(',');
                foreach (string item in columns)
                {
                    //var objReportColumn = infoRpt.RPT_ReportColumn.Where(x => x.ColumnName.ToUpper() == item.ToUpper()).FirstOrDefault();
                    var objReportColumn = infoRpt.RPT_ReportColumn.Where(x => x.ColumnName== item).FirstOrDefault();
                    if (objReportColumn != null)
                    {
                        DataColumn dc = new DataColumn(objReportColumn.ColumnName, System.Type.GetType(objReportColumn.ColumnType));
                        dc.Caption = objReportColumn.ColumnFormat; //Namespace
                        dt.Columns.Add(dc);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}