/********************************************************************************
** 作者： 梅靖
** 描述：
** DataTable扩展方法
*********************************************************************************/
using System.Collections.Generic;
using System.Reflection;

namespace System.Data
{
    public static class DataTableExtension
    {
        /// <summary>
        /// 把制定的DataTable转换为T类型的List列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="mDataTable">DataTable扩展对象</param>
        /// <returns>实体列表</returns>
        public static List<T> ToList<T>(this DataTable mDataTable) where T : new()
        {
            List<T> lstT = new List<T>();
            PropertyInfo[] arrPropertyInfo = typeof(T).GetProperties();
            if (mDataTable == null)
            {
                return lstT;
            }
            foreach (DataRow eachDataRow in mDataTable.Rows)
            {
                T mT = new T();
                for (int i = 0; i < arrPropertyInfo.Length; ++i)
                {
                    PropertyInfo mPropertyInfo = arrPropertyInfo[i];
                    if (!mDataTable.Columns.Contains(mPropertyInfo.Name))
                    {
                        continue;
                    }
                    object objValue = eachDataRow[mPropertyInfo.Name];
                    if (objValue == DBNull.Value)
                    {
                        continue;
                    }
                    mPropertyInfo.SetValue(mT, GetValue(objValue, mPropertyInfo.PropertyType), null);
                }
                lstT.Add(mT);
            }
            return lstT;
        }

        /// <summary>
        /// 根据Object对象的类型，返回对应类型的值
        /// </summary>
        /// <param name="mObject">Object对象</param>
        /// <param name="mType">Object类型</param>
        /// <returns>Object对象对应类型的值（int、double、string或object）</returns>
        private static object GetValue(Object mObject, Type mType)
        {
            if (mType.IsAssignableFrom(typeof(int)))
            {
                return mObject.ToInt32();
            }
            else if (mType.IsAssignableFrom(typeof(double)))
            {
                return mObject.ToDouble();
            }
            else if (mType.IsAssignableFrom(typeof(string)))
            {
                return mObject.ToString();
            }
            return mObject;
        }
    }
}