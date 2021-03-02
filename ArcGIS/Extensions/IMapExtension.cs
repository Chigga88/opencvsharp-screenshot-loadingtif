/********************************************************************************
** 作者： 梅靖
** 创始时间：2014-3-22
** 描述：
** IMap的扩展方法
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESRI.ArcGIS.Carto
{
    public static class IMapExtension
    {
        /// <summary>
        /// 根据图层名找到图层 
        /// </summary>
        /// <param name="myIMap">IMap扩展对象</param>
        /// <param name="strILayerName">图层名称</param>
        /// <returns>ILayer对象</returns>
        public static ILayer GetLayer(this IMap myIMap, string strILayerName)
        {
            if (myIMap == null)
            {
                return null;
            }
            IEnumLayer myIEnumLayer;
            myIEnumLayer = myIMap.get_Layers(null, true);
            myIEnumLayer.Reset();
            ILayer myILayer = myIEnumLayer.Next();
            while (myILayer != null)
            {
                if (myILayer.Name.Equals(strILayerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return myILayer;
                }
                myILayer = myIEnumLayer.Next();
            }
            return null;
        }
    }
}
