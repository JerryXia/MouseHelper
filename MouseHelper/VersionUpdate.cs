using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MouseHelper
{
    public class VersionUpdate
    {
        string versionCheckUrl = "http://blog.163.com/gqkzwy@126/blog/static/3451300620130162218248/";
        private HttpItem httpItem;
        private HttpHelper httpHelper;

        public VersionUpdate()
        {
            httpItem = new HttpItem();
            httpHelper = new HttpHelper();
        }

        public bool HasNewVersion(Version nowVersion, out string updateUrl)
        {
            updateUrl = string.Empty;
            httpItem.URL = versionCheckUrl;
            httpItem.Timeout = 60000;
            httpItem.Encoding = Encoding.GetEncoding("gb2312");
            try
            {
                HttpResult httpResult = httpHelper.GetHtml(httpItem);
                if (httpResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Match match = Regex.Match(httpResult.Html, @"<div class=""bct fc05 fc11 nbw-blog ztag""><p>(.+)</p></div>", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        string content = match.Groups[1].Value;
                        byte[] rawData = Convert.FromBase64String(content);
                        string rawProtocolDataStr = Encoding.UTF8.GetString(rawData);
                        LogHelper.Info(rawProtocolDataStr);
                        string[] arrary = rawProtocolDataStr.Split(';');
                        if (arrary.Length == 2)
                        {
                            string[] arr = arrary[0].Split('.');
                            Version newVersion = new Version(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]),
                                Convert.ToInt32(arr[2]), Convert.ToInt32(arr[3]));
                            LogHelper.Info(newVersion.ToString() + arrary[1]);
                            if (newVersion > nowVersion)
                            {
                                updateUrl = arrary[1];
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
