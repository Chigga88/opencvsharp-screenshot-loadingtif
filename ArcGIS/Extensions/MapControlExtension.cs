/********************************************************************************
** 作者： 梅靖
** 创始时间：2014-3-22
** 描述：
** ArcEngine MapControl的扩展方法
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace ESRI.ArcGIS.Controls
{
    public static class MapControlExtension
    {
        /// <summary>
        /// 根据图层名找到图层 
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="strLayerName">图层名称</param>
        /// <returns>图层(ILayer)</returns>
        public static ILayer GetILayer(this ESRI.ArcGIS.Controls.AxMapControl myAxMapControl, string strLayerName)
        {
            IEnumLayer myIEnumLayer;
            myIEnumLayer = (myAxMapControl.Map as IMap).get_Layers(null, true);
            myIEnumLayer.Reset();
            ILayer myILayer = myIEnumLayer.Next();
            while (myILayer != null)
            {
                if (myILayer.Name.Equals(strLayerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return myILayer;
                }
                myILayer = myIEnumLayer.Next();
            }
            return null;
        }

        /// <summary>
        /// 缩放到指定IGeometry对象列表组成的范围
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="lstIGeometry">IGeometry对象列表</param>
        public static void ZoomToIGeometries(this AxMapControl myAxMapControl, List<IGeometry> lstIGeometry)
        {
            IEnvelope myIEnvelope = null;
            foreach (IGeometry eachIGeometry in lstIGeometry)
            {
                if (myIEnvelope == null)
                {
                    myIEnvelope = eachIGeometry.Envelope;
                }
                else
                {
                    myIEnvelope.Union(eachIGeometry.Envelope);
                }
            }
            if (myIEnvelope == null)
            {
                return;
            }
            myIEnvelope.Width = myIEnvelope.Width * 1.1;
            myIEnvelope.Height = myIEnvelope.Height * 1.1;
            myAxMapControl.Extent = myIEnvelope;
            myAxMapControl.Refresh(esriViewDrawPhase.esriViewBackground, null, myIEnvelope);
        }

        /// <summary>
        /// 缩放到指定IFeature列表组成的范围
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="LstIFeature">IFeature列表</param>
        public static void ZoomToIFeatures(this AxMapControl myAxMapControl, List<IFeature> LstIFeature)
        {
            IEnvelope myIEnvelope = null;
            foreach (IFeature eachIFeature in LstIFeature)
            {
                if (myIEnvelope == null)
                {
                    myIEnvelope = eachIFeature.Shape.Envelope;
                }
                else
                {
                    myIEnvelope.Union(eachIFeature.Shape.Envelope);
                }
            }
            if (myIEnvelope == null)
            {
                return;
            }
            myIEnvelope.Width = myIEnvelope.Width * 1.1;
            myIEnvelope.Height = myIEnvelope.Height * 1.1;
            myAxMapControl.Extent = myIEnvelope;
            myAxMapControl.Refresh(esriViewDrawPhase.esriViewBackground, null, myIEnvelope);
        }

        /// <summary>
        /// 根据XY座标定位
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="douX">X座标</param>
        /// <param name="douY">Y座标</param>
        public static void LocateToXY(this AxMapControl myAxMapControl, double douX, double douY)
        {
            IPoint myIPoint = new PointClass();
            myIPoint.X = douX;
            myIPoint.Y = douY;
            myAxMapControl.LocateToIGeomery(myIPoint);
        }

        /// <summary>
        /// 定位到指定IGeometry
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="myIGeometry">IGeometryd对象</param>
        public static void LocateToIGeomery(this AxMapControl myAxMapControl, IGeometry myIGeometry)
        {
            IEnvelope myIEnvelope = myIGeometry.Envelope;
            if (myIEnvelope.XMax == myIEnvelope.XMin || myIEnvelope.YMax == myIEnvelope.YMin)
            {
                double douX = myIEnvelope.XMin;
                double douY = myIEnvelope.YMin;
                myIEnvelope.XMin = douX - 0.001;
                myIEnvelope.XMax = douX + 0.001;
                myIEnvelope.YMin = douY - 0.001;
                myIEnvelope.YMax = douY + 0.001;
                myIEnvelope.Expand(4, 4, true);
                myAxMapControl.ActiveView.Extent = myIEnvelope;
            }
            myAxMapControl.Refresh();
        }

        /// <summary>
        /// 定位到指定IFeature
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="myIFeature">IFeature对象</param>
        public static void LocateToIFeature(this AxMapControl myAxMapControl, IFeature myIFeature)
        {
            myAxMapControl.LocateToIGeomery(myIFeature.Extent);
        }

        /// <summary>
        /// 得到所有的IFeatureLayer
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <returns>IFeatureLayer对象列表</returns>
        public static List<IFeatureLayer> GetAllIFeatureLayers(this AxMapControl myAxMapControl)
        {
            IEnumLayer myIEnumLayer = myAxMapControl.Map.get_Layers(null, true);
            myIEnumLayer.Reset();
            List<IFeatureLayer> lstIFeatureLayer = new List<IFeatureLayer>();
            for (ILayer myILayer = myIEnumLayer.Next(); myILayer != null; myILayer = myIEnumLayer.Next())
            {
               GetChildIFeatureLayer(myILayer, lstIFeatureLayer);
            }
            return lstIFeatureLayer;
        }

        /// <summary>
        /// 判断一个ILayer是不是IFeatureLayer，是则加入IFeatureLayer对象列表
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="lstIFeatureLayer">IFeatureLayer对象列表</param>
        private static void GetChildIFeatureLayer(ILayer myILayer, List<IFeatureLayer> lstIFeatureLayer)
        {
            IFeatureLayer myIFeatureLayer = myILayer as IFeatureLayer;
            if (null != myIFeatureLayer)
            {
                lstIFeatureLayer.Add(myIFeatureLayer);
                return;
            }
        }

        /// <summary>
        /// 得到N个像素的宽度Double值
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="intPixelUnits">像素个数</param>
        /// <returns>N个像素的宽度Double值</returns>
        public static double PixelsToMapUnits(this AxMapControl myAxMapControl, int intPixelUnits)
        {
            IActiveView myIActiveView = myAxMapControl.ActiveView;
            tagRECT mytagRECT = myIActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
            int intPixelExtent = mytagRECT.right - mytagRECT.left;
            double douRealWorldDisplayExtent = myIActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            double douSizeOfOnePixel = douRealWorldDisplayExtent / intPixelExtent;
            return intPixelUnits * douSizeOfOnePixel;
        }

        /// <summary>
        /// 创建内存IFeatureLayer，该方法为创建内存临时图层的便捷方法，创建的图层带有id,和name两个属性字段。
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="strIFeatureClassName">IFeatureClass名称</param>
        /// <param name="myEsriGeometryType">数据类型</param>
        /// <returns>IFeatureLayer对象</returns>
        public static IFeatureLayer CreateMemoryFeatureLayer(this AxMapControl myAxMapControl, String strIFeatureClassName, esriGeometryType myEsriGeometryType)
        {
            IWorkspaceFactory myIWorkspaceFactory = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName myIWorkspaceName = myIWorkspaceFactory.Create("", strIFeatureClassName, null, 0);
            IName myIName = (IName)myIWorkspaceName;
            IWorkspace myIWorkspace = (IWorkspace)myIName.Open();
            IField myIField = null;
            IFields myIFields = new FieldsClass();
            IFieldsEdit myIFieldsEdit = myIFields as IFieldsEdit;
            IFieldEdit myIFieldEdit = null;
            IFeatureClass myIFeatureClass = null;
            IFeatureLayer myIFeatureLayer = null;
            try
            {
                //主键id
                myIField = new FieldClass();
                myIFieldEdit = myIField as IFieldEdit;
                myIFieldEdit.Name_2 = "id";
                myIFieldEdit.IsNullable_2 = true;
                myIFieldEdit.Length_2 = 50;
                myIFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                myIFieldsEdit.AddField(myIField);
                //名称name
                myIField = new FieldClass();
                myIFieldEdit = myIField as IFieldEdit;
                myIFieldEdit.Name_2 = "name";
                myIFieldEdit.IsNullable_2 = true;
                myIFieldEdit.Length_2 = 50;
                myIFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                myIFieldsEdit.AddField(myIField);
                //IGeometryI字段
                IGeometryDef myIGeometryDef = new GeometryDefClass();
                IGeometryDefEdit myIGeometryDefEdit = (IGeometryDefEdit)myIGeometryDef;
                myIGeometryDefEdit.AvgNumPoints_2 = 5;
                myIGeometryDefEdit.GeometryType_2 = myEsriGeometryType;
                myIGeometryDefEdit.GridCount_2 = 1;
                myIGeometryDefEdit.HasM_2 = false;
                myIGeometryDefEdit.HasZ_2 = false;
                myIGeometryDefEdit.SpatialReference_2 = myAxMapControl.SpatialReference;
                myIField = new FieldClass();
                myIFieldEdit = myIField as IFieldEdit;
                myIFieldEdit.Name_2 = "SHAPE";
                myIFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                myIFieldEdit.GeometryDef_2 = myIGeometryDef;
                myIFieldEdit.IsNullable_2 = true;
                myIFieldEdit.Required_2 = true;
                myIFieldsEdit.AddField(myIField);
                //
                myIFeatureClass = (myIWorkspace as IFeatureWorkspace).CreateFeatureClass(strIFeatureClassName,myIFields, null, null,  esriFeatureType.esriFTSimple,  "SHAPE", "");
                (myIFeatureClass as IDataset).BrowseName = strIFeatureClassName;
                myIFeatureLayer = new FeatureLayerClass();
                myIFeatureLayer.Name = strIFeatureClassName;
                myIFeatureLayer.FeatureClass = myIFeatureClass;
            }
            catch
            {

            }
            return myIFeatureLayer;
        }

        /// <summary>
        /// 获取所有ILayer对象
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <returns>ILayer对象列表</returns>
        public static List<ILayer> GetAllILayers(this ESRI.ArcGIS.Controls.AxMapControl myAxMapControl)
        {
            List<ILayer> lstILayer = new List<ILayer>();
            IEnumLayer myIEnumLayer = (myAxMapControl.Map as IMap).get_Layers(null, true);
            myIEnumLayer.Reset();
            ILayer myILayer = myIEnumLayer.Next();
            while (myILayer != null)
            {
                lstILayer.Add(myILayer);
                myILayer = myIEnumLayer.Next();
            }
            return lstILayer;
        }

        /// <summary>
        /// 高亮IGeometry
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="myIGeometry">需要高亮的IGeometry</param>
        public static void FlashIGeometry(this ESRI.ArcGIS.Controls.AxMapControl myAxMapControl, IGeometry myIGeometry)
        {
            ISimpleMarkerSymbol myISimpleMarkerSymbol = new SimpleMarkerSymbolClass();
            myISimpleMarkerSymbol.Size = 18;
            IRgbColor myIRgbColor = new RgbColorClass();
            myIRgbColor.Green = 255;
            myISimpleMarkerSymbol.Color = myIRgbColor;
            myISimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
            myAxMapControl.FlashIGeometry(myIGeometry, myISimpleMarkerSymbol as ISymbol);
        }

        /// <summary>
        /// 高亮IGeometry
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="myIGeometry">需要高亮的IGeometry</param>
        /// <param name="myISymbol">高亮样式</param>
        public static void FlashIGeometry(this ESRI.ArcGIS.Controls.AxMapControl myAxMapControl, IGeometry myIGeometry, ISymbol myISymbol)
        {
            Timer myTimer = new Timer();
            myTimer.Interval = 2000;
            myTimer.Start();
            myTimer.Tick += (s, e) =>
            {
                myTimer.Stop();
                myAxMapControl.FlashShape(myIGeometry, 5, 100, myISymbol);
            };
        }

        /// <summary>
        /// 通过IFeature获取IFeatureLayer
        /// </summary>
        /// <param name="myAxMapControl">AxMapControl扩展对象</param>
        /// <param name="myIFeature">IFeature对象</param>
        /// <returns>IFeatureLayer对象</returns>
        public static IFeatureLayer GetFeatureLayer(this ESRI.ArcGIS.Controls.AxMapControl myAxMapControl, IFeature myIFeature)
        {
            List<IFeatureLayer> lstIFeatureLayer = myAxMapControl.GetAllIFeatureLayers();
            for (int i = 0; i < lstIFeatureLayer.Count; i++)
            {
                IFeatureClass myIFeatureClass = lstIFeatureLayer[i].FeatureClass;
                if (myIFeatureClass == myIFeature.Class)
                {
                    return lstIFeatureLayer[i];
                }
            }
            return null;
        }
    }
}