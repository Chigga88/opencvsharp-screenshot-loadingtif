/********************************************************************************
** 作者： 梅靖
** 描述：
** Object扩展方法
*********************************************************************************/
namespace System
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Object类型转化为double类型
        /// </summary>
        /// <param name="mObject">Object扩展对象</param>
        /// <returns>double值</returns>
        public static double ToDouble(this object mObject)
        {
            double douValue = 0;
            try
            {
                douValue = Convert.ToDouble(mObject);
            }
            catch
            {

            }
            return douValue;
        }

        /// <summary>
        /// Object类型转化为ToInt32类型
        /// </summary>
        /// <param name="mObject">Object扩展对象</param>
        /// <returns>Int32值</returns>
        public static Int32 ToInt32(this object mObject)
        {
            int intValue = 0;
            intValue = Convert.ToInt32(mObject);
            return intValue;
        }
    }
}