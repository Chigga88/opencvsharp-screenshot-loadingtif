/********************************************************************************
** 作者： 梅靖
** 描述：
** List扩展方法
*********************************************************************************/
using System.Collections.Generic;
using System.Reflection;

namespace System.Data
{
    public static class ListExtension
    {
        /// <summary>
        /// 将一个列表转换成DataTable,如果列表为空将返回空的DataTable结构
        /// </summary>
        /// <typeparam name="T">要转换的数据类型</typeparam>
        /// <param name="lstT">实体对象列表</param> 
        public static DataTable ListToDataTable<T>(this List<T> lstT)
        {
            DataTable mDataTable = new DataTable();
            Type mType = typeof(T);
            PropertyInfo[] arrPropertyInfo = mType.GetProperties();//取类型T所有PropertyInfo
            Type columnType = null;
            foreach (PropertyInfo eachPropertyInfo in arrPropertyInfo)
            {
                if (eachPropertyInfo.PropertyType.IsGenericType)
                {
                    columnType = Nullable.GetUnderlyingType(eachPropertyInfo.PropertyType);
                }
                else
                {
                    columnType = eachPropertyInfo.PropertyType;
                }
                if (columnType.FullName.StartsWith("System"))
                {
                    mDataTable.Columns.Add(eachPropertyInfo.Name, columnType);
                }
            }
            if (lstT != null && lstT.Count > 0)
            {
                foreach (T eachT in lstT)
                {
                    DataRow mDataRow = mDataTable.NewRow();
                    foreach (PropertyInfo eachPropertyInfo in arrPropertyInfo)
                    {
                        if (mDataTable.Columns.Contains(eachPropertyInfo.Name))
                        {
                            object objValue = eachPropertyInfo.GetValue(eachT, null);
                            mDataRow[eachPropertyInfo.Name] = objValue == null ? DBNull.Value : objValue;
                        }
                    }
                    mDataTable.Rows.Add(mDataRow);
                }
            }
            return mDataTable;
        }
    }
}