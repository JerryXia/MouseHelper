using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseHelper
{
    public class MouseEventHelper
    {
        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /*
         dwFlags常数 意义 
         */

        public static int MouseEventAbs(OperatingFlag flag, int x, int y)
        {
            return MouseEventAbs(flag, x, y, 1);
        }
        public static int MouseEventAbs(OperatingFlag flag, int x, int y, int count)
        {
            int result = 0;
            for (int i = 0; i < count; i++)
            {
                result += MouseEvent(OperatingFlag.MouseEvent_Absolute | flag, x, y);
            }
            return result;
        }

        public static int MouseEvent(OperatingFlag flag, int x, int y)
        {
            int px = 65536;
            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;
            return mouse_event((int)flag, x * px / sw, y * px / sh, 0, 0);
        }

    }

    [Flags]
    public enum OperatingFlag
    {
        MouseEvent_Move = 0x0001,       //移动鼠标 
        MouseEvent_LeftDown = 0x0002,   //模拟鼠标左键按下 
        MouseEvent_LeftUp = 0x0004,     //模拟鼠标左键抬起 
        MouseEvent_RightDown = 0x0008,  //模拟鼠标右键按下 
        MouseEvent_RightUp = 0x0010,    //模拟鼠标右键抬起 
        MouseEvent_MiddleDown = 0x0020, //模拟鼠标中键按下 
        MouseEvent_MiddleUp = 0x0040,   //模拟鼠标中键抬起 
        MouseEvent_Absolute = 0x8000    //标示是否采用绝对坐标
    }
}
