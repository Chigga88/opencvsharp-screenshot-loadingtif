/********************************************************************************
** 作者： 梅靖
** 描述：
** 常用方法集合
*********************************************************************************/
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System;
using System.Data.OleDb;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace DomHelpPS
{
    public static class ComprehensiveTool
    {
        /// <summary>
        /// 把DataRows里的数据复制到一个新的DataTable里
        /// </summary>
        /// <param name="arrDataRow">DataRow数组</param>
        /// <returns>DataTable对现</returns>
        public static DataTable CreateDataTableByArrDataRow(DataRow[] arrDataRow)
        {
            if (arrDataRow == null || arrDataRow.Length <= 0)
            {
                return null;
            }
            DataTable mDataTable = arrDataRow[0].Table.Clone();
            foreach (DataRow eachDataRow in arrDataRow)
            {
                mDataTable.ImportRow(eachDataRow);
            }
            return mDataTable;
        }

        /// <summary>
        /// 根据名称关闭系统进程
        /// </summary>
        /// <param name="strProcessName">系统进程名称</param>
        public static void KillProcess(string strProcessName)
        {
            bool blExists = false;
            System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(strProcessName);
            if (arrProcess.Length > 0)
            {
                blExists = true;
            }
            if (blExists)
            {
                foreach (System.Diagnostics.Process eachProcess in arrProcess)
                {
                    eachProcess.Kill();
                    break;
                }
            }
        }

        /// <summary>
        /// 根据名称检测系统进程是否存在
        /// </summary>
        /// <param name="strProcessName">系统进程名称</param>
        /// <returns>true或者false</returns>
        public static bool ProcessExists(string strProcessName)
        {
            System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(strProcessName);
            if (arrProcess.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据名称获取同名进程数量
        /// </summary>
        /// <param name="strProcessName">系统进程名称</param>
        /// <returns>系统进程个数</returns>
        public static int GetProcessCount(string strProcessName)
        {
            System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(strProcessName);
            return arrProcess.Length;
        }

        /// <summary>
        /// DataTable转化为EXCEL
        /// </summary>
        /// <param name="mDataTable">DataTable对象</param>
        /// <param name="strFileName">EXCEL文件保存路径</param>
        public static void ExportToSvc(DataTable mDataTable, string strFileName)
        {
            string strPath = strFileName;
            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            } //如果文件已经存在就删除掉
            //先打印标头
            StringBuilder strColumnStringBuilder = new StringBuilder();
            StringBuilder strValueStringBuilder = new StringBuilder();
            StreamWriter mStreamWriter = new StreamWriter(new System.IO.FileStream(strPath, FileMode.CreateNew), Encoding.GetEncoding("GB2312"));
            for (int i = 0; i <= mDataTable.Columns.Count - 1; i++)
            {
                strColumnStringBuilder.Append(mDataTable.Columns[i].ColumnName);
                strColumnStringBuilder.Append(",");  //csv的分隔符是","
            }
            strColumnStringBuilder.Remove(strColumnStringBuilder.Length - 1, 1);//移出掉最后一个,字符
            mStreamWriter.WriteLine(strColumnStringBuilder);//这样就把字段名写入了文件
            for (int j = 0; j < mDataTable.Rows.Count; j++)
            {
                DataRow mDataRow = mDataTable.Rows[j];
                strValueStringBuilder.Remove(0, strValueStringBuilder.Length);//清空字符串
                for (int i = 0; i <= mDataTable.Columns.Count - 1; i++)
                {
                    strValueStringBuilder.Append(mDataRow[i].ToString());
                    strValueStringBuilder.Append(",");
                }
                strValueStringBuilder.Remove(strValueStringBuilder.Length - 1, 1);//移出掉最后一个,字符
                mStreamWriter.WriteLine(strValueStringBuilder);
            }
            mStreamWriter.Close();
            MessageBox.Show("导出完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}