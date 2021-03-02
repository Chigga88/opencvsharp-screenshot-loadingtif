/********************************************************************************
** 作者： 梅靖
** 创始时间：2014-3-22
** 描述：
** IFeature的扩展方法
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESRI.ArcGIS.Geodatabase
{
    public static class IFeatureExtension
    {
        /// <summary>
        /// 获取IFeature对象中的某个字段值
        /// </summary>
        /// <param name="myIFeature">IFeature扩展对象</param>
        /// <param name="strFieldName">字段名称</param>
        /// <returns>指定字段值</returns>
        public static object GetValue(this IFeature myIFeature, String strFieldName)
        {
            int intFieldIndex = myIFeature.Fields.FindField(strFieldName);
            if (intFieldIndex == -1)
            {
                return "";
            }
            else
            {
                return myIFeature.get_Value(intFieldIndex);
            }
        }

        /// <summary>
        /// 获取IFeature对象中的某个字段值并转化为字符串
        /// </summary>
        /// <param name="myIFeature">IFeature扩展对象</param>
        /// <param name="strFieldName">字段名称</param>
        /// <returns>指定字段的字符串值</returns>
        public static String GetStringValue(this IFeature myIFeature, String strFieldName)
        {
            return myIFeature.GetValue(strFieldName).ToString();
        }

        /// <summary>
        /// 为IFeature对象中的某个字段赋值
        /// </summary>
        /// <param name="myIFeature">IFeature扩展对象</param>
        /// <param name="strFieldName">字段名称</param>
        /// <param name="objValue">字段赋值</param>
        public static void SetValue(this IFeature myIFeature, String strFieldName, object objValue)
        {
            int intFieldIndex = myIFeature.Fields.FindField(strFieldName);
            if (intFieldIndex == -1)
            {
                return;
            }
            myIFeature.set_Value(intFieldIndex, objValue);
        }
    }
}
