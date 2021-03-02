/********************************************************************************
** 作者： 梅靖
** 创始时间：2014-3-22
** 描述：
** IFeatureLayer的扩展方法
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ESRI.ArcGIS.Carto
{
    public static class IFeatureLayerExtension
    {
        /// <summary>
        /// 根据IFeatureI的ObjectID或FID获取IFeature对象
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="intObjectID">IFeature的ID</param>
        /// <returns>IFeature对象</returns>
        public static IFeature GetIFeature(this IFeatureLayer myIFeatureLayer, int intObjectID)
        {
            if (myIFeatureLayer == null)
            {
                return null;
            }
            IFeature myIFeature = myIFeatureLayer.FeatureClass.GetFeature(intObjectID);
            if (myIFeature == null)
            {
                return null;
            }
            return myIFeature;
        }

        /// <summary>
        /// 得到指定图层所有的IFeature
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="blReSetLayerDefinition">是否重置LayerDefinition进行查询</param>
        /// <returns>IFeature列表</returns>
        public static List<IFeature> GetAllLstIFeature(this IFeatureLayer myIFeatureLayer, bool blReSetLayerDefinition)
        {
            if (myIFeatureLayer == null)
            {
                return new List<IFeature>();
            }
            IFeatureLayerDefinition myIFeatureLayerDefinition = myIFeatureLayer as IFeatureLayerDefinition;
            string strOldIFeatureLayerDefinition = myIFeatureLayerDefinition.DefinitionExpression;
            if (blReSetLayerDefinition)
            {
                myIFeatureLayerDefinition.DefinitionExpression = "";
            }
            IFeatureCursor myIFeatureCursor = myIFeatureLayer.Search(null, false);
            List<IFeature> lstIFeature = new List<IFeature>();
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); eachIFeature != null; eachIFeature = myIFeatureCursor.NextFeature())
            {
                lstIFeature.Add(eachIFeature);
            }
            myIFeatureLayerDefinition.DefinitionExpression = strOldIFeatureLayerDefinition;
            return lstIFeature;
        }

        /// <summary>
        /// 根据指定的查询条件查询获取IFeature
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="strWhereClause">查询条件</param>
        /// <param name="blResetLayerDefinition">是否重置LayerDefinition进行查询</param>
        /// <returns>IFeature列表</returns>
        public static List<IFeature> GetLstIFeature(this IFeatureLayer myIFeatureLayer, string strWhereClause, bool blResetLayerDefinition)
        {
            if (myIFeatureLayer == null)
            {
                return new List<IFeature>();
            }
            IFeatureLayerDefinition myIFeatureLayerDefinition = myIFeatureLayer as IFeatureLayerDefinition;
            string strOldIFeatureLayerDefinition = myIFeatureLayerDefinition.DefinitionExpression;
            if (blResetLayerDefinition)
            {
                myIFeatureLayerDefinition.DefinitionExpression = "";
            }
            IQueryFilter myIQueryFilter = new QueryFilterClass();
            myIQueryFilter.WhereClause = strWhereClause;
            List<IFeature> lstIFeature = new List<IFeature>();
            IFeatureCursor myIFeatureCursor = myIFeatureLayer.Search(myIQueryFilter, false);
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); null != eachIFeature; eachIFeature = myIFeatureCursor.NextFeature())
            {
                lstIFeature.Add(eachIFeature);
            }
            if (blResetLayerDefinition)
            {
                myIFeatureLayerDefinition.DefinitionExpression = strOldIFeatureLayerDefinition;
            }
            return lstIFeature;
        }

        /// <summary>
        /// 根据制定的查询条件获取IFeature
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="myIGeometry">查询地理要素</param>
        /// <param name="strWhereClause">查询条件</param>
        /// <param name="_EsriSpatialRelEnum">查询的图元和查询地理要素的空间关系</param>
        /// <returns>IFeature列表</returns>
        public static List<IFeature> GetLstIFeature(this IFeatureLayer myIFeatureLayer, IGeometry myIGeometry, string strWhereClause, esriSpatialRelEnum myEsriSpatialRelEnum)
        {
            if (myIFeatureLayer == null)
            {
                return new List<IFeature>();
            }
            if (myEsriSpatialRelEnum == esriSpatialRelEnum.esriSpatialRelUndefined)
            {
                switch (myIFeatureLayer.FeatureClass.ShapeType)
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
            IFeatureCursor myIFeatureCursor = myIFeatureLayer.Search(myISpatialFilter, false);


            //IFeatureCursor myIFeatureCursor = myIFeatureLayer.Search(null, false);


            List<IFeature> lstIFeature = new List<IFeature>();
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); null != eachIFeature; eachIFeature = myIFeatureCursor.NextFeature())
            {
                lstIFeature.Add(eachIFeature);
            }
            return lstIFeature;
        }
        
        /// <summary>
        /// 根据制定的查询条件获取IFeature
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="myIGeometry">查询地理要素</param>
        /// <param name="strWhereClause">查询条件</param>
        /// <param name="_EsriSpatialRelEnum">查询的图元和查询地理要素的空间关系</param>
        /// <param name="blReSetLayerDefinition">是否重置LayerDefinition进行查询</param>
        /// <returns>IFeature列表</returns>
        public static List<IFeature> GetLstIFeature(this IFeatureLayer myIFeatureLayer, IGeometry myIGeometry, string strWhereClause, esriSpatialRelEnum myEsriSpatialRelEnum, bool blReSetLayerDefinition)
        {
            if (myIFeatureLayer == null)
            {
                return new List<IFeature>();
            }
            if (myEsriSpatialRelEnum == esriSpatialRelEnum.esriSpatialRelUndefined)
            {
                switch (myIFeatureLayer.FeatureClass.ShapeType)
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
            myISpatialFilter.GeometryField = myIFeatureLayer.FeatureClass.ShapeFieldName;
            IFeatureLayerDefinition myIFeatureLayerDefinition = myIFeatureLayer as IFeatureLayerDefinition;
            string strOldIFeatureLayerDefinition = myIFeatureLayerDefinition.DefinitionExpression;
            if (blReSetLayerDefinition)
            {
                myIFeatureLayerDefinition.DefinitionExpression = "";
            }
            IFeatureCursor myIFeatureCursor = myIFeatureLayer.Search(myISpatialFilter, false);
            List<IFeature> lstIFeature = new List<IFeature>();
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); null != eachIFeature; eachIFeature = myIFeatureCursor.NextFeature())
            {
                lstIFeature.Add(eachIFeature);
            }
            if (blReSetLayerDefinition)
            {
                myIFeatureLayerDefinition.DefinitionExpression = strOldIFeatureLayerDefinition;
            }
            return lstIFeature;
        }

        /// <summary>
        /// 高亮单个IFeature
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="myIFeature">IFeature对象</param>
        /// <param name="blClearSelection">是否清除原有高亮图元</param>
        public static void HightLightIFeature(this IFeatureLayer myIFeatureLayer, IFeature myIFeature, bool blClearSelection)
        {
            List<IFeature> lstIFeature = new List<IFeature>();
            lstIFeature.Add(myIFeature);
            myIFeatureLayer.HightLightIFeatures(lstIFeature, blClearSelection);
        }

        /// <summary>
        /// 高亮IFeature列表
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="lstIFeature">IFeature列表</param>
        /// <param name="blClearSelection">是否清除原有高亮图元</param>
        public static void HightLightIFeatures(this IFeatureLayer myIFeatureLayer, List<IFeature> lstIFeature, bool blClearSelection)
        {
            IFeatureSelection myIFeatureSelection = myIFeatureLayer as IFeatureSelection;
            if (blClearSelection)
            {
                myIFeatureSelection.Clear();
            }
            foreach (IFeature eachIFeature in lstIFeature)
            {
                myIFeatureSelection.Add(eachIFeature);
            }
        }

        /// <summary>
        /// 清除图层IFeature选择集
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="myIFeatureLayer">指定图层</param>
        public static void ClearSelection(this IFeatureLayer myIFeatureLayer)
        {
            IFeatureSelection myIFeatureSelection = myIFeatureLayer as IFeatureSelection;
            myIFeatureSelection.Clear();
        }

        /// <summary>
        /// 获取图层指定字段的UniqueValues
        /// </summary>
        /// <param name="myIFeatureLayer">IFeatureLayer扩展对象</param>
        /// <param name="strFieldName">字段名称</param>
        /// <returns></returns>
        public static List<string> GetFieldUniqueValues(this IFeatureLayer myIFeatureLayer,string strFieldName)
        {
            List<string> lstStrFieldUniqueValue = new List<string>();
            IFeatureCursor myIFeatureCursor = myIFeatureLayer.Search(null, false);
            for (IFeature eachIFeature = myIFeatureCursor.NextFeature(); eachIFeature != null; eachIFeature = myIFeatureCursor.NextFeature())
            {
                bool blRepeat = false ;
                string strFieldValue = eachIFeature.GetStringValue(strFieldName);
                for (int i=0; i < lstStrFieldUniqueValue.Count; i++)
                {
                    if (strFieldValue == lstStrFieldUniqueValue[i])
                    {
                        blRepeat = true;
                        break;
                    }
                }
                if (blRepeat == false)
                {
                    lstStrFieldUniqueValue.Add(strFieldValue);
                }
            }
            return lstStrFieldUniqueValue;
        }
    }
}
