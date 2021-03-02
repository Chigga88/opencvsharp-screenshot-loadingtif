/********************************************************************************
** 作者： 梅靖
** 创始时间：2014-3-22
** 描述：
** 各种方式打开ESRI数据
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.DataSourcesFile;

namespace ESRI
{
    public class GeoDataBaseConnection
    {
        /// <summary>
        /// 打开sde的workspace
        /// </summary>
        /// <param name="strServer">服务器名</param>
        /// <param name="strInstance">实例名</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPassword">密码</param>
        /// <param name="strDatabase">数据库名称</param>
        /// <param name="stVersion">版本</param>
        /// <returns>返回工作空间</returns>
        public static IWorkspace OpenSDEWorkspace(string strServer, string strInstance, string strUser, string strPassword, string strDatabase, string stVersion)
        {
            IWorkspace myIWorkspace = null;
            IPropertySet myIPropertySet = new PropertySetClass();
            IWorkspaceFactory myIWorkspaceFactory = new SdeWorkspaceFactoryClass();
            myIPropertySet.SetProperty("SERVER", strServer);
            myIPropertySet.SetProperty("INSTANCE", strInstance);
            myIPropertySet.SetProperty("DATABASE", strDatabase);
            myIPropertySet.SetProperty("USER", strUser);
            myIPropertySet.SetProperty("PASSWORD", strPassword);
            myIPropertySet.SetProperty("VERSION", stVersion);
            try
            {
                myIWorkspace = myIWorkspaceFactory.Open(myIPropertySet, 0);
            }
            catch 
            {

            }
            return myIWorkspace;
        }

        /// <summary>
        /// 直接打开ORACLE数据库中的工作空间
        /// </summary>
        /// <param name="strDataSource">数据源</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPassword">密码</param>
        /// <returns>返回工作空间</returns>
        private static IWorkspace OpenOracleWorkspace(string strDataSource, string strUser, string strPassword)
        {
            IWorkspaceFactory myIWorkspaceFactory = new OLEDBWorkspaceFactoryClass();
            IPropertySet myIPropertySet;
            myIPropertySet = new PropertySetClass();
            myIPropertySet.SetProperty("CONNECTSTRING", "Provide=oraoledb.oracle;Data Source=" + strDataSource + ";User Id=" + strUser + ";Password=" + strPassword);
            IWorkspace myIWorkspace = null;
            try
            {
                myIWorkspace = myIWorkspaceFactory.Open(myIPropertySet, 0);
            }
            catch
            {

            }
            return myIWorkspace;
        }

        /// <summary>
        /// 打开Personal GeoDatabae数据库
        /// </summary>
        /// <param name="strConnString">连接字符串如："D:\\Statescountles.mdb"</param>
        /// <returns>成功则返回该工作空间；否则返回null</returns>
        public static IWorkspace OpenAccessWorkspace(string strConnString)
        {
            IWorkspace myIWorkspace = null;
            IWorkspaceFactory myIWorkspaceFactory = new AccessWorkspaceFactoryClass();
            try
            {
                myIWorkspace = myIWorkspaceFactory.OpenFromFile(strConnString, 0);
            }
            catch
            {

            }
            return myIWorkspace;
        }

        /// <summary>
        /// 打开shape数据库
        /// </summary>
        /// <param name="strFilePath">连接字符串如："d:\\temp"</param>
        /// <returns>成功则返回该工作空间；否则返回null</returns>
        public static IWorkspace OpenShapfileWorkspace(string strFilePath)
        {
           
            IWorkspace myIWorkspace = null;
            IWorkspaceFactory myIWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            try
            {
                myIWorkspace = myIWorkspaceFactory.OpenFromFile(strFilePath, 0);
            }
            catch 
            {

            }
            return myIWorkspace;
        }

        /// <summary>
        /// 打开file geodatabase数据库
        /// </summary>
        /// <param name="strConnString">连接字符串如："D:\\GIS\\GuangYuan\\GuangYuan.gdb"</param>
        /// <returns>成功则返回该工作空间；否则返回null</returns>
        public static IWorkspace OpenFileGeodatabaseWorkspace(string strConnString)
        {
            IAoInitialize myIAoInitialize  = new AoInitializeClass();
            myIAoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);
            IWorkspaceFactory myIWorkspaceFactory = new FileGDBWorkspaceFactoryClass();
            IWorkspace myIWorkspace = null;
            try
            {
                myIWorkspace = myIWorkspaceFactory.OpenFromFile(strConnString, 0);
            }
            catch
            {

            }
            return myIWorkspace;
        }
    }
}
