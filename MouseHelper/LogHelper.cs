using System;
using System.IO;
using System.Text;

namespace MouseHelper
{
    public class LogHelper
    {

        public static void Info(object value)
        {
            #if DEBUG
            File.WriteAllText(string.Format(@"D:\Develop\logs\MouseHelper\{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmssfffffff")),
                string.Format("{0}", value), Encoding.UTF8);
            #endif
        }

    }
}
