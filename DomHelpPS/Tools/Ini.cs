using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

namespace DomHelpPS
{
    /// <summary>
    /// Ini文件读写类
    /// </summary>
    public class Ini
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string strSection, string strKey, string strValue, string strFilePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string strSection, string strKey, string strDefaultValue, StringBuilder mStringBuilder, int intSize, string strFilePath);

        public static string strDefaultFilePath;

        /// <summary>
        /// 写入Ini文件
        /// </summary>
        /// <param name="strSection">节</param>
        /// <param name="strKey">键</param>
        /// <param name="strValue">值</param>
        /// <param name="strIniFilePath">Ini文件路径</param>
        public static void IniWriteValue(string strSection, string strKey, string strValue, string strIniFilePath)
        {
            if (File.Exists(strIniFilePath) == true && strIniFilePath.GetFileType().ToUpper() == "INI")
            {
                Ini.WritePrivateProfileString(strSection, strKey, strValue, strIniFilePath);
            }
        }

        /// <summary>
        /// 写入默认Ini文件
        /// </summary>
        /// <param name="strSection">节</param>
        /// <param name="strKey">键</param>
        /// <param name="strValue">值</param>
        public static void IniWriteValue(string strSection, string strKey, string strValue)
        {
            if (File.Exists(Ini.strDefaultFilePath) == true && Ini.strDefaultFilePath.GetFileType().ToUpper() == "INI")
            {
                Ini.WritePrivateProfileString(strSection, strKey, strValue, Ini.strDefaultFilePath);
            }
        }

        /// <summary>
        /// 读取Ini文件
        /// </summary>
        /// <param name="strSection">节</param>
        /// <param name="strKey">键</param>
        /// <param name="strFilePath">Ini文件路径</param>
        /// <returns>值</returns>
        public static string IniReadValue(string strSection, string strKey, string strFilePath)
        {
            if (File.Exists(strFilePath) == true && strFilePath.GetFileType().ToUpper() == "INI")
            {
                StringBuilder mStringBuilder = new StringBuilder(4096);
                Ini.GetPrivateProfileString(strSection, strKey, "", mStringBuilder, 4096, strFilePath);
                return mStringBuilder.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 读取默认Ini文件
        /// </summary>
        /// <param name="strSection">节</param>
        /// <param name="strKey">键</param>
        /// <returns>值</returns>
        public static string IniReadValue(string strSection, string strKey)
        {
            if (File.Exists(Ini.strDefaultFilePath) == true && Ini.strDefaultFilePath.GetFileType().ToUpper() == "INI")
            {
                StringBuilder mStringBuilder = new StringBuilder(4096);
                Ini.GetPrivateProfileString(strSection, strKey, "", mStringBuilder, 4096, Ini.strDefaultFilePath);
                return mStringBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}