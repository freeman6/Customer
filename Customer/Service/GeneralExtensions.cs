using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Customer.Service
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// 轉換 List 型別物件為 DataTable 型別物件
        /// </summary>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // 欄位名稱
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;

            try
            {
                foreach (T rec in varlist)
                {
                    //透過反射來取得屬性名稱，並且建立DataTable；只有第一次時執行，其它次數都依循
                    if (oProps == null)
                    {
                        oProps = ((Type)rec.GetType()).GetProperties();
                        foreach (PropertyInfo pi in oProps)
                        {
                            Type colType = pi.PropertyType;

                            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            {
                                colType = colType.GetGenericArguments()[0];
                            }

                            string colName = pi.Name;
                            var colAttribute = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                            try
                            {
                                colName = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                            }
                            catch
                            {
                                colName = pi.Name;
                            }

                            dtReturn.Columns.Add(new DataColumn(colName, colType));
                        }
                    }

                    DataRow dr = dtReturn.NewRow();

                    foreach (PropertyInfo pi in oProps)
                    {
                        string colName = pi.Name;
                        var colAttribute = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);

                        try
                        {
                            colName = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                        }
                        catch
                        {
                            colName = pi.Name;
                        }

                        dr[colName] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                    }

                    dtReturn.Rows.Add(dr);
                }
            }
            catch
            {
                dtReturn = new DataTable();
            }
            return dtReturn;
        }

    }
}