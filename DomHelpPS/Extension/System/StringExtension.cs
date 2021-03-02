using System.Linq;

namespace System
{
    /// <summary>
    /// String扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 根据文件路径提取文件所在目录路径
        /// </summary>
        /// <param name="mString">string对象</param>
        /// <returns>文件目录路径字符串</returns>
        public static string GetFolderPath(this string mString)
        {
            mString = mString.Replace(@"/", @"\");
            string[] arrStr = mString.Split('\\');
            string strValue = string.Empty;
            for (int i = 0; i < arrStr.Length - 1; i++)
            {
                strValue += arrStr[i];
                strValue += "\\";
            }
            return strValue.IsNullOrEmpty() ? "" : strValue.Remove(strValue.Length - 1);
        }

        /// <summary>
        /// 根据文件全路径提取文件名称（不带格式）
        /// </summary>
        /// <param name="mString">string扩展对象</param>
        /// <returns>文件名字符串（不带格式）</returns>
        public static string GetFileName(this string mString)
        {
            string[] arrStrPath = mString.Split('\\');
            string strValue = string.Empty;
            if (arrStrPath.Length >= 1)
            {
                strValue = arrStrPath[arrStrPath.Length - 1];
                strValue = strValue.Remove(strValue.IndexOf('.'));
            }
            return strValue;
        }

        /// <summary>
        /// 根据文件路径提取文件名称（带格式）
        /// </summary>
        /// <param name="mString">string对象</param>
        /// <returns>文件名称字符串（带格式）</returns>
        public static string GetFileNameWithType(this string mString)
        {
            mString = mString.Replace(@"/", @"\");
            string[] arrStr = mString.Split('\\');
            return arrStr[arrStr.Length - 1];
        }

        /// <summary>
        /// 根据文件路径提取文件格式（不带"."）
        /// </summary>
        /// <param name="mString">string对象</param>
        /// <returns>文件格式字符串（不带"."）</returns>
        public static string GetFileType(this string mString)
        {
            mString = mString.Replace(@"/", @"\");
            string[] arrStr = mString.Split('\\');
            string strValue = arrStr[arrStr.Length - 1];
            return strValue.Split('.')[strValue.Split('.').Length - 1];
        }

        /// <summary>
        /// string类型转化为Int32类型
        /// </summary>
        /// <param name="mString">string对象</param>
        /// <returns>string对象Int32值</returns>
        public static Int32 ToInt32(this string mString)
        {
            if (mString.IsDoubleCharacter() == false)
            {
                return 0;
            }
            if (mString.Contains(".") == true)
            {
                return Convert.ToInt32(Convert.ToDouble(mString));
            }
            else
            {
                return Convert.ToInt32(mString);
            }
        }

        /// <summary>
        /// 判断文本是否合法字符
        /// </summary>
        /// <param name="mString">string对象</param>
        /// <returns>是否合法字符</returns>
        public static bool IsLegalCharacter(this string mString)
        {
            string strIllegalCharacter = "~!@#$%^&*()_+{}:"+'"'+"|<>?`-=[];'\\,./！￥…（）—｛｝：“”《》？·【】；‘’、，。、";
            for (int i = 0; i < mString.Length; i++)
            {
                if (strIllegalCharacter.Contains(mString.Substring(i, 1)) == true)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断文本是否为Int
        /// </summary>
        /// <param name="mString">string对象</param>
        /// <returns>是否为Int</returns>
        public static bool IsIntCharacter(this string mString)
        {
            if (mString.IsNullOrEmpty() == true)
            {
                return false;
            }
            string strIntCharacter = "1234567890";
            for (int i = 0; i < mString.Length; i++)
            {
                if (strIntCharacter.Contains(mString.Substring(i, 1)) == false)
                {
                    return false;
                }
            }
            if (mString.StartsWith("0") && mString.Length >= 2)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断文本是否为Double
        /// </summary>
        /// <param name="mString">string对象</param>
        /// <returns>是否为Double</returns>
        public static bool IsDoubleCharacter(this string mString)
        {
            if (mString.IsNullOrEmpty() == true)
            {
                return false;
            }
            string strDoubleCharacter = "1234567890.";
            for (int i = 0; i < mString.Length; i++)
            {
                if (strDoubleCharacter.Contains(mString.Substring(i, 1)) == false)
                {
                    return false;
                }
            }
            if (mString.StartsWith(".") || mString.EndsWith("."))//开头和结尾是小数点的返回false
            {
                return false;
            }
            if (mString.IndexOf('.') > 0)//如果有小数点，删除第一个小数点，在判断是否有小数点，还有小数点的返回false
            {
                string strTemporaryString = mString.Remove(mString.IndexOf('.'), 1);
                if (strTemporaryString.Contains('.') == true)
                {
                    return false;
                }
                if (mString.StartsWith("0") && mString.StartsWith("0.") == false)//如果第一位是0，则第二位必须是.
                {
                    return false;
                }
            }
            else if (mString.StartsWith("0") && mString.Length >= 2)//如果没有小数点，则两位以上的数字开头不能是0
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断字符是否为Null或""
        /// </summary>
        /// <param name="mString">string扩展对象</param>
        /// <returns>否为Null或""</returns>
        public static bool IsNullOrEmpty(this string mString)
        {
            return string.IsNullOrEmpty(mString);
        }
    }
}