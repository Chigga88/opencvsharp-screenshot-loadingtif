/********************************************************************************
** 作者： 梅靖
** 描述：
** double扩展方法
*********************************************************************************/
namespace System
{
    public static class DoubleExtension
    {
        /// <summary>
        /// 保留小数点后N位
        /// </summary>
        /// <param name="mDouble">double扩展对象</param>
        /// <param name="intRetainCount">小数点位数</param>
        /// <returns>小数点后N位double值</returns>
        public static double RetentionDecimal(this double mDouble, int intRetainCount)
        {
            if (intRetainCount > 0)
            {
                mDouble = Convert.ToInt64(mDouble * System.Math.Pow(10, intRetainCount)) * 1.0 / System.Math.Pow(10, intRetainCount);
            }
            return mDouble;
        }

        /// <summary>
        /// 转化为Int32
        /// </summary>
        /// <param name="mDouble">double扩展对象</param>
        /// <returns>Int32值</returns>
        public static int ToInt32(this double mDouble)
        {
            int intValue = Convert.ToInt32(mDouble);
            return intValue;
        }

        /// <summary>
        /// 取Int32整
        /// </summary>
        /// <param name="mDouble">double扩展对象</param>
        /// <returns>Int32值</returns>
        public static int RoundInt32(this double mDouble)
        {
            int intValue = Convert.ToInt32(mDouble);
            return (int)mDouble;
        }

        /// <summary>
        /// 把double类型的经纬度转化为字符串类型的度分秒模式经纬度
        /// </summary>
        /// <param name="mDouble">double扩展对象对象</param>
        /// <returns>度分秒模式经纬度字符串值</returns>
        public static string GetDegreesMinutesAndSeconds(this double mDouble)
        {
            mDouble = System.Math.Abs(mDouble);
            int intDegrees;
            int intMinutes;
            int intSeconds;
            intDegrees = (int)mDouble;
            intMinutes = (int)((mDouble - intDegrees) * 60);
            intSeconds = Convert.ToInt32(((mDouble - intDegrees) * 60 - intMinutes) * 60);
            return intDegrees.ToString() + "°" + intMinutes.ToString() + "′" + intSeconds.ToString() + "″";
        }

        /// <summary>
        /// 获取绝对值
        /// </summary>
        /// <param name="mDouble">double扩展对象对象</param>
        /// <returns>绝对值</returns>
        public static double GetAbsoluteValue(this double mDouble)
        {
            return System.Math.Abs(mDouble);
        }
    }
}