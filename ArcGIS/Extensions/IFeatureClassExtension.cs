/********************************************************************************
** 作者： 梅靖
** 创始时间：2014-3-22
** 描述：
** IFeatureClass扩展
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace ESRI.ArcGIS.Geodatabase
{
    public static class IFeatureClassExtension
    {
        /// <summary>
        /// 得到指定IFeatureClass所有的IFeature
        /// </summary>
        /// <param name="myIFeatureClass">IFeatureClass扩展对象</param>
        /// <returns>IFeature列表</returns>
        public static List<IFeature> GetAllLstIFeature(this IFeatureClass myIFeatureClass)
        {
            if (myIFeatureClass == null)
            {
                return new List<IFeature>();
            }
            IFeatureCursor myIFeatureCursor = myIFeatureClass.Search(null, false);
            List<IFeature> lstIFeature = new List<IFeature>();
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); eachIFeature != null; eachIFeature = myIFeatureCursor.NextFeature())
            {
                lstIFeature.Add(eachIFeature);
            }
            return lstIFeature;
        }

        /// <summary>
        /// 根据指定的查询条件查询获取IFeature
        /// </summary>
        /// <param name="myIFeatureClass">IFeatureClass扩展对象</param>
        /// <param name="strWhereClause">查询条件</param>
        /// <returns>IFeature列表</returns>
        public static List<IFeature> GetLstIFeature(this IFeatureClass myIFeatureClass, String strWhereClause)
        {
            if (myIFeatureClass == null)
            {
                return new List<IFeature>();
            }
            IQueryFilter myIQueryFilter = new QueryFilterClass();
            myIQueryFilter.WhereClause = strWhereClause;
            List<IFeature> lstIFeature = new List<IFeature>();
            IFeatureCursor myIFeatureCursor = myIFeatureClass.Search(myIQueryFilter, false);
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); null != eachIFeature; eachIFeature = myIFeatureCursor.NextFeature())
            {
                lstIFeature.Add(eachIFeature);
            }
            return lstIFeature;
        }

        /// <summary>
        /// 根据制定的查询条件获取IFeature
        /// </summary>
        /// <param name="myIFeatureClass">IFeatureClass扩展对象</param>
        /// <param name="myIGeometry">查询地理要素</param>
        /// <param name="strWhereClause">查询条件</param>
        /// <param name="_EsriSpatialRelEnum">查询的图元和查询地理要素的空间关系</param>
        /// <returns>IFeature列表</returns>
        public static List<IFeature> GetLstIFeature(this IFeatureClass myIFeatureClass, IGeometry myIGeometry, String strWhereClause, esriSpatialRelEnum myEsriSpatialRelEnum)
        {
            if (myIFeatureClass == null)
            {
                return new List<IFeature>();
            }
            if (myEsriSpatialRelEnum == esriSpatialRelEnum.esriSpatialRelUndefined)
            {
                switch (myIFeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        myEsriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelContains;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        myEsriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelCrosses;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        myEsriSpatialRelEnum = esriSpatialRelEnum.esriSpatialRelIntersects;
                        break;
                }
            }
            ISpatialFilter myISpatialFilter = new SpatialFilterClass();
            if (!string.IsNullOrEmpty(strWhereClause))
            {
                myISpatialFilter.WhereClause = strWhereClause;
            }
            myISpatialFilter.Geometry = myIGeometry;
            myISpatialFilter.SpatialRel = myEsriSpatialRelEnum;
            IFeatureCursor myIFeatureCursor = myIFeatureClass.Search(myISpatialFilter, false);
            List<IFeature> lstIFeature = new List<IFeature>();
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); null != eachIFeature; eachIFeature = myIFeatureCursor.NextFeature())
            {
                lstIFeature.Add(eachIFeature);
            }
            return lstIFeature;
        }
    }
}
