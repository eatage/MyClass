using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass
{
    class HashtableHelper
    {
        /// <summary>
        /// 将DataTable类型转换为Hashtable类型
        /// </summary>
        /// <param name="as_dt">DataTable</param>
        /// <returns>Hashtable</returns>
        public static Hashtable DataTableToHashtable(DataTable as_dt)
        {
            Hashtable hash = new Hashtable();
            try
            {
                for (int i = 0; i < as_dt.Rows.Count; i++)
                {
                    hash.Add(as_dt.Rows[i][0], as_dt.Rows[i][1]);
                }
                return hash;
            }
            catch
            {
                return hash;
            }
        }

        /// <summary>
        /// 将Hashtable类型转换为DataTable类型,可指定列名
        /// </summary>
        /// <param name="as_Hash">Hashtable</param>
        /// <param name="as_Row1">列名1，Hashtable.Keys</param>
        /// <param name="as_Row2">列名2，Hashtable.Values</param>
        /// <returns>DataTable</returns>
        public static DataTable HashtableToDataTable(Hashtable as_Hash, string as_Row1 = "", string as_Row2 = "")
        {
            try
            {
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(as_Row1) && !string.IsNullOrEmpty(as_Row2))
                {
                    DataColumn dc1 = dt.Columns.Add(as_Row1, typeof(object));
                    DataColumn dc2 = dt.Columns.Add(as_Row2, typeof(object));

                    foreach (DictionaryEntry element in as_Hash)
                    {
                        DataRow dr = dt.NewRow();
                        dr[as_Row1] = (object)element.Key;//防止类型转换出错 这里使用object类型
                        dr[as_Row2] = (object)element.Value;

                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    DataColumn keys = dt.Columns.Add("keys", typeof(object));
                    DataColumn values = dt.Columns.Add("values", typeof(object));

                    foreach (DictionaryEntry element in as_Hash)
                    {
                        DataRow dr = dt.NewRow();
                        dr["keys"] = (object)element.Key;//防止类型转换出错 这里使用object类型
                        dr["values"] = (object)element.Value;

                        dt.Rows.Add(dr);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将Hashtable类型转换为Json类型,可指定列名
        /// </summary>
        /// <param name="as_Hash">Hashtable</param>
        /// <param name="as_Row1">列名1，Hashtable.Keys</param>
        /// <param name="as_Row2">列名2，Hashtable.Values</param>
        /// <returns>DataTable</returns>
        public static string HashtableToJson(Hashtable as_Hash, string as_Row1 = "", string as_Row2 = "")
        {
            if (as_Hash.Count == 0)
            {
                return "";
            }
            DataTable ldt = new DataTable();
            if (!string.IsNullOrEmpty(as_Row1) && !string.IsNullOrEmpty(as_Row2))
            {
                ldt = HashtableToDataTable(as_Hash, as_Row1, as_Row2);
                string json = "";
                json = JsonHelper.DatatableToJson(ldt);
                return json;
            }
            else
            {
                ldt = HashtableToDataTable(as_Hash);
                string json = "";
                json = JsonHelper.DatatableToJson(ldt);
                return json;
            }
        }

        /// <summary>
        /// 将Json字符串转换为Hashtable类型
        /// </summary>
        /// <param name="as_json"></param>
        /// <returns></returns>
        public static Hashtable JsonToHashtable(string as_json)
        {
            Hashtable Hash = new Hashtable();
            if (string.IsNullOrEmpty(as_json))
            {
                return Hash;
            }
            DataTable ldt = new DataTable();
            ldt = JsonHelper.JsonToDataTable(as_json);

            Hash = DataTableToHashtable(ldt);
            return Hash;
        }
    }
}
