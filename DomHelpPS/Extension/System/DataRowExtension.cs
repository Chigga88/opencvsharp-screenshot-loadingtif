/********************************************************************************
** 作者： 梅靖
** 描述：
** DataRow扩展方法
*********************************************************************************/
using System.Drawing;
using System.IO;

namespace System.Data
{
    public static class DataRowExtension
    {
        /// <summary>
        /// 根据字段名称获取字段字符串值
        /// </summary>
        /// <param name="mDataRow">DataRow扩展对象</param>
        /// <param name="strColumnName">字段名称</param>
        /// <returns>字段字符串值</returns>
        public static string GetStringValueByColumnName(this DataRow mDataRow, string strColumnName)
        {
            return mDataRow[strColumnName] == DBNull.Value ? "" : mDataRow[strColumnName].ToString();
        }

        /// <summary>
        /// 根据字段索引获取字段字符串值
        /// </summary>
        /// <param name="mDataRow">DataRow扩展对象</param>
        /// <param name="intColumnIndex">字段索引</param>
        /// <returns>字段字符串值</returns>
        public static string GetStringValueByColumnIndex(this DataRow mDataRow, int intColumnIndex)
        {
            return mDataRow[intColumnIndex] == DBNull.Value ? "" : mDataRow[intColumnIndex].ToString();
        }

        /// <summary>
        /// 读取图像数据
        /// </summary>
        /// <param name="mDataRow">DataRow扩展对象</param>
        /// <param name="strColumnName">图像字段名称</param>
        /// <returns>Bitmap对象</returns>
        public static Bitmap GetBitmapValue(this DataRow mDataRow, string strColumnName)
        {
            MemoryStream mMemoryStream = new MemoryStream(mDataRow[strColumnName] as byte[]);
            Bitmap mBitmap = new Bitmap(mMemoryStream);
            return mBitmap;
        }
    }
}
