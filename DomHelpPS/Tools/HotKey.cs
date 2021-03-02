using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DomHelpPS
{
    public class HotKey
    {
        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr mIntPtr,                //要定义热键的窗口的句柄
            int mInt,                     //定义热键ID（不能与其它ID重复）           
            KeyModifiers mKeyModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys mKeys                     //定义热键的内容
            );

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr mIntPtr,                //要取消热键的窗口的句柄
            int mInt                      //要取消热键的ID
            );

        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
    }
}