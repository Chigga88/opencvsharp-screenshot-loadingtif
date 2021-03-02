/********************************************************************************
** 作者： 梅靖
** 创始时间：2014-3-22
** 描述：
** DEM计算
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SpatialAnalyst;

namespace ESRI
{
    public class RasterCalculate
    {
        /// <summary>
        /// 获取高程信息ITable
        /// </summary>
        /// <param name="strFolderPath">DEM文件夹</param>
        /// <param name="strFileName">DEM文件名称</param>
        /// <param name="myIFeatureClass">过滤面状数据集</param>
        /// <returns>高程信息ITable</returns>
        public ITable CalculateValueFromFile(string strFolderPath, string strFileName, IFeatureClass myIFeatureClass)
        {
            IWorkspaceFactory myIWorkspaceFactory = new RasterWorkspaceFactory();
            IWorkspace myIWorkspace = myIWorkspaceFactory.OpenFromFile(strFolderPath, 0);
            IRasterWorkspace myIRasterWorkspace = myIWorkspace as IRasterWorkspace;
            IRasterDataset myIRasterDataset = myIRasterWorkspace.OpenRasterDataset(strFileName);//创建影像数据集
            IGeoDataset rasterIGeoDataset = myIRasterDataset as IGeoDataset;
            IGeoDataset myIFeatureClassGeoDataset = myIFeatureClass as IGeoDataset;
            IZonalOp myIZonalOp = new RasterZonalOpClass();
            ITable myITable = myIZonalOp.ZonalStatisticsAsTable(myIFeatureClassGeoDataset, rasterIGeoDataset, true);
            return myITable;
        }
    }
}