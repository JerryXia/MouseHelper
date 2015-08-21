using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MouseHelper
{
    public class AttributeHelper
    {
        public static string GetAssemblyGuid()
        {
            Attribute guid_attr = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute));
            string guid = ((GuidAttribute)guid_attr).Value;
            return guid;
        }

    }
}
