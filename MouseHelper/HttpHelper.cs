using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace MouseHelper
{
    /// <example>
    ///  HttpHelper http = new HttpHelper();
    ///  HttpItem item = new HttpItem()
    ///  {
    ///   URL = "http://www.cckan.net",//URL     ������
    ///   Encoding = "gbk",//�����ʽ��utf-8,gb2312,gbk��     ��ѡ�� Ĭ������Զ�ʶ��
    ///   Method = "get",//URL     ��ѡ�� Ĭ��ΪGet
    ///   //Timeout = 100000,//���ӳ�ʱʱ��     ��ѡ��Ĭ��Ϊ100000
    ///   //ReadWriteTimeout = 30000,//д��Post���ݳ�ʱʱ��     ��ѡ��Ĭ��Ϊ30000
    ///   //IsToLower = false,//�õ���HTML�����Ƿ�ת��Сд     ��ѡ��Ĭ��תСд
    ///   Cookie = "",//�ַ���Cookie     ��ѡ��
    ///   // UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//�û�����������ͣ��汾������ϵͳ     ��ѡ����Ĭ��ֵ
    ///   // Accept = "text/html, application/xhtml+xml, */*",//    ��ѡ����Ĭ��ֵ
    ///   // ContentType = "text/html",//��������    ��ѡ����Ĭ��ֵ
    ///   Referer = "http://www.cckan.net",//��ԴURL     ��ѡ��
    ///   //Allowautoredirect = true,//�Ƿ���ݣ�������ת     ��ѡ��
    ///   //CerPath = "d:\\123.cer",//֤�����·��     ��ѡ���Ҫ֤��ʱ���Բ�д�������
    ///   //Connectionlimit = 1024,//���������     ��ѡ�� Ĭ��Ϊ1024
    ///   //Postdata = "username=sufei&pwd=cckan.net",//Post����     ��ѡ��GETʱ����Ҫд
    ///   //ProxyIp = "192.168.1.105",//���������ID     ��ѡ�� ����Ҫ���� ʱ���Բ���������������
    ///   //ProxyPwd = "123456",//�������������     ��ѡ��
    ///   // ProxyUserName = "administrator",//����������˻���     ��ѡ��
    ///  };
    ///  //�õ�HTML����
    ///  string html = http.GetHtml(item);
    ///
    ///  //ȡ�����ص�Cookie
    ///  string cookie = item.Cookie;
    ///  //ȡ�����ص�Request
    ///  HttpWebRequest request = item.Request;
    ///  //ȡ�����ص�Response
    ///  HttpWebResponse response = item.Response;
    ///  //ȡ�����ص�Reader
    ///  StreamReader reader = item.Reader;
    ///  //ȡ�����ص�Headers
    ///  WebHeaderCollection header = response.Headers;
    /// </example>

    /// <summary>
    /// Http���Ӳ���������
    /// </summary>
    public class HttpHelper
    {
        #region Ԥ���巽�����߱��

        //Ĭ�ϵı���
        private Encoding encoding = Encoding.Default;
        //Post���ݱ���
        private Encoding postencoding = Encoding.Default;
        //HttpWebRequest����������������
        private HttpWebRequest request = null;
        //��ȡӰ���������ݶ���
        private HttpWebResponse response = null;
        /// <summary>
        /// �����ഫ������ݣ��õ���Ӧҳ������
        /// </summary>
        /// <param name="objhttpitem">���������</param>
        /// <returns>����HttpResult����</returns>
        public HttpResult GetHtml(HttpItem objhttpitem)
        {
            //���ز���
            HttpResult result = new HttpResult();
            try
            {
                //׼������
                SetRequest(objhttpitem);
            }
            catch (Exception ex)
            {
                result = new HttpResult();
                result.Cookie = string.Empty;
                result.Header = null;
                result.Html = ex.Message;
                result.StatusDescription = "���ò���ʱ����" + ex.Message;
                return result;
            }
            try
            {
                #region �õ������response
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    result.StatusCode = response.StatusCode;
                    result.StatusDescription = response.StatusDescription;
                    result.Header = response.Headers;
                    if (response.Cookies != null) result.CookieCollection = response.Cookies;
                    if (response.Headers["set-cookie"] != null) result.Cookie = response.Headers["set-cookie"];
                    MemoryStream _stream = new MemoryStream();
                    //GZIIP����
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //��ʼ��ȡ�������ñ��뷽ʽ
                        //new GZipStream(response.GetResponseStream(), CompressionMode.Decompress).CopyTo(_stream, 10240);
                        //.net4.0����д��
                        _stream = GetMemoryStream(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        //��ʼ��ȡ�������ñ��뷽ʽ
                        //response.GetResponseStream().CopyTo(_stream, 10240);
                        //.net4.0����д��
                        _stream = GetMemoryStream(response.GetResponseStream());
                    }
                    //��ȡByte
                    byte[] RawResponse = _stream.ToArray();
                    _stream.Close();
                    //�Ƿ񷵻�Byte��������
                    if (objhttpitem.ResultType == ResultType.Byte) result.ResultByte = RawResponse;
                    //�����￪ʼ����Ҫ���ӱ�����
                    if (encoding == null)
                    {
                        Match meta = Regex.Match(Encoding.Default.GetString(RawResponse), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
                        string charter = (meta.Groups.Count > 1) ? meta.Groups[2].Value.ToLower() : string.Empty;
                        if (charter.Length > 2)
                            encoding = Encoding.GetEncoding(charter.Trim().Replace("\"", "").Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk"));
                        else
                        {
                            if (string.IsNullOrEmpty(response.CharacterSet)) encoding = Encoding.UTF8;
                            else encoding = Encoding.GetEncoding(response.CharacterSet);
                        }
                    }
                    //�õ����ص�HTML
                    result.Html = encoding.GetString(RawResponse);
                }
                #endregion
            }
            catch (WebException ex)
            {
                //�������ڷ����쳣ʱ���صĴ�����Ϣ
                response = (HttpWebResponse)ex.Response;
                result.Html = ex.Message;
                if (response != null)
                {
                    result.StatusCode = response.StatusCode;
                    result.StatusDescription = response.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                result.Html = ex.Message;
            }
            if (objhttpitem.IsToLower) result.Html = result.Html.ToLower();
            return result;
        }
        /// <summary>
        /// 4.0����.net�汾ȡ����ʹ��
        /// </summary>
        /// <param name="streamResponse">��</param>
        private static MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            return _stream;
        }
        /// <summary>
        /// Ϊ����׼������
        /// </summary>
        ///<param name="objhttpItem">�����б�</param>
        private void SetRequest(HttpItem objhttpItem)
        {
            // ��֤֤��
            SetCer(objhttpItem);
            //����Header����
            if (objhttpItem.Header != null && objhttpItem.Header.Count > 0) foreach (string item in objhttpItem.Header.AllKeys)
                {
                    request.Headers.Add(item, objhttpItem.Header[item]);
                }
            // ���ô���
            SetProxy(objhttpItem);
            if (objhttpItem.ProtocolVersion != null) request.ProtocolVersion = objhttpItem.ProtocolVersion;
            request.ServicePoint.Expect100Continue = objhttpItem.Expect100Continue;
            //����ʽGet����Post
            request.Method = objhttpItem.Method;
            request.Timeout = objhttpItem.Timeout;
            request.ReadWriteTimeout = objhttpItem.ReadWriteTimeout;
            //Accept
            request.Accept = objhttpItem.Accept;
            //ContentType��������
            request.ContentType = objhttpItem.ContentType;
            //UserAgent�ͻ��˵ķ������ͣ�����������汾�Ͳ���ϵͳ��Ϣ
            request.UserAgent = objhttpItem.UserAgent;
            // ����
            encoding = objhttpItem.Encoding;
            //����Cookie
            SetCookie(objhttpItem);
            //��Դ��ַ
            request.Referer = objhttpItem.Referer;
            //�Ƿ�ִ����ת����
            request.AllowAutoRedirect = objhttpItem.Allowautoredirect;
            //����Post����
            SetPostData(objhttpItem);
            //�����������
            if (objhttpItem.Connectionlimit > 0) request.ServicePoint.ConnectionLimit = objhttpItem.Connectionlimit;
        }
        /// <summary>
        /// ����֤��
        /// </summary>
        /// <param name="objhttpItem"></param>
        private void SetCer(HttpItem objhttpItem)
        {
            if (!string.IsNullOrEmpty(objhttpItem.CerPath))
            {
                //��һ��һ��Ҫд�ڴ������ӵ�ǰ�档ʹ�ûص��ķ�������֤����֤��
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                //��ʼ�����񣬲����������URL��ַ
                request = (HttpWebRequest)WebRequest.Create(objhttpItem.URL);
                SetCerList(objhttpItem);
                //��֤����ӵ�������
                request.ClientCertificates.Add(new X509Certificate(objhttpItem.CerPath));
            }
            else
            {
                //��ʼ�����񣬲����������URL��ַ
                request = (HttpWebRequest)WebRequest.Create(objhttpItem.URL);
                SetCerList(objhttpItem);
            }
        }
        /// <summary>
        /// ���ö��֤��
        /// </summary>
        /// <param name="objhttpItem"></param>
        private void SetCerList(HttpItem objhttpItem)
        {
            if (objhttpItem.ClentCertificates != null && objhttpItem.ClentCertificates.Count > 0)
            {
                foreach (X509Certificate item in objhttpItem.ClentCertificates)
                {
                    request.ClientCertificates.Add(item);
                }
            }
        }
        /// <summary>
        /// ����Cookie
        /// </summary>
        /// <param name="objhttpItem">Http����</param>
        private void SetCookie(HttpItem objhttpItem)
        {
            if (!string.IsNullOrEmpty(objhttpItem.Cookie))
                //Cookie
                request.Headers[HttpRequestHeader.Cookie] = objhttpItem.Cookie;
            //����Cookie
            if (objhttpItem.CookieCollection != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(objhttpItem.CookieCollection);
            }
        }
        /// <summary>
        /// ����Post����
        /// </summary>
        /// <param name="objhttpItem">Http����</param>
        private void SetPostData(HttpItem objhttpItem)
        {
            //��֤�ڵõ����ʱ�Ƿ��д�������
            if (request.Method.Trim().ToLower().Contains("post"))
            {
                if (objhttpItem.PostEncoding != null)
                {
                    postencoding = objhttpItem.PostEncoding;
                }
                byte[] buffer = null;
                //д��Byte����
                if (objhttpItem.PostDataType == PostDataType.Byte && objhttpItem.PostdataByte != null && objhttpItem.PostdataByte.Length > 0)
                {
                    //��֤�ڵõ����ʱ�Ƿ��д�������
                    buffer = objhttpItem.PostdataByte;
                }//д���ļ�
                else if (objhttpItem.PostDataType == PostDataType.FilePath && !string.IsNullOrEmpty(objhttpItem.Postdata))
                {
                    StreamReader r = new StreamReader(objhttpItem.Postdata, postencoding);
                    buffer = postencoding.GetBytes(r.ReadToEnd());
                    r.Close();
                } //д���ַ���
                else if (!string.IsNullOrEmpty(objhttpItem.Postdata))
                {
                    buffer = postencoding.GetBytes(objhttpItem.Postdata);
                }
                if (buffer != null)
                {
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }
            }
        }
        /// <summary>
        /// ���ô���
        /// </summary>
        /// <param name="objhttpItem">��������</param>
        private void SetProxy(HttpItem objhttpItem)
        {
            if (!string.IsNullOrEmpty(objhttpItem.ProxyIp))
            {
                //���ô��������
                if (objhttpItem.ProxyIp.Contains(":"))
                {
                    string[] plist = objhttpItem.ProxyIp.Split(':');
                    WebProxy myProxy = new WebProxy(plist[0].Trim(), Convert.ToInt32(plist[1].Trim()));
                    //��������
                    myProxy.Credentials = new NetworkCredential(objhttpItem.ProxyUserName, objhttpItem.ProxyPwd);
                    //����ǰ�������
                    request.Proxy = myProxy;
                }
                else
                {
                    WebProxy myProxy = new WebProxy(objhttpItem.ProxyIp, false);
                    //��������
                    myProxy.Credentials = new NetworkCredential(objhttpItem.ProxyUserName, objhttpItem.ProxyPwd);
                    //����ǰ�������
                    request.Proxy = myProxy;
                }
                //���ð�ȫƾ֤
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
        }
        /// <summary>
        /// �ص���֤֤������
        /// </summary>
        /// <param name="sender">������</param>
        /// <param name="certificate">֤��</param>
        /// <param name="chain">X509Chain</param>
        /// <param name="errors">SslPolicyErrors</param>
        /// <returns>bool</returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        #endregion
    }
    /// <summary>
    /// Http����ο���
    /// </summary>
    public class HttpItem
    {
        string _URL = string.Empty;
        /// <summary>
        /// ����URL������д
        /// </summary>
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
        string _Method = "GET";
        /// <summary>
        /// ����ʽĬ��ΪGET��ʽ,��ΪPOST��ʽʱ��������Postdata��ֵ
        /// </summary>
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        int _Timeout = 20000;
        /// <summary>
        /// Ĭ������ʱʱ��
        /// </summary>
        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }
        int _ReadWriteTimeout = 30000;
        /// <summary>
        /// Ĭ��д��Post���ݳ�ʱʱ��
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _ReadWriteTimeout; }
            set { _ReadWriteTimeout = value; }
        }
        string _Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        /// <summary>
        /// �����ͷֵ Ĭ��Ϊtext/html, application/xhtml+xml, */*
        /// </summary>
        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }
        string _ContentType = "text/html";
        /// <summary>
        /// ���󷵻�����Ĭ�� text/html
        /// </summary>
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }
        string _UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        /// <summary>
        /// �ͻ��˷�����ϢĬ��Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        /// </summary>
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        Encoding _Encoding = null;
        /// <summary>
        /// �������ݱ���Ĭ��ΪNUll,�����Զ�ʶ��,һ��Ϊutf-8,gbk,gb2312
        /// </summary>
        public Encoding Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }
        private PostDataType _PostDataType = PostDataType.String;
        /// <summary>
        /// Post����������
        /// </summary>
        public PostDataType PostDataType
        {
            get { return _PostDataType; }
            set { _PostDataType = value; }
        }
        string _Postdata = string.Empty;
        /// <summary>
        /// Post����ʱҪ���͵��ַ���Post����
        /// </summary>
        public string Postdata
        {
            get { return _Postdata; }
            set { _Postdata = value; }
        }
        private byte[] _PostdataByte = null;
        /// <summary>
        /// Post����ʱҪ���͵�Byte���͵�Post����
        /// </summary>
        public byte[] PostdataByte
        {
            get { return _PostdataByte; }
            set { _PostdataByte = value; }
        }
        CookieCollection cookiecollection = null;
        /// <summary>
        /// Cookie���󼯺�
        /// </summary>
        public CookieCollection CookieCollection
        {
            get { return cookiecollection; }
            set { cookiecollection = value; }
        }
        string _Cookie = string.Empty;
        /// <summary>
        /// ����ʱ��Cookie
        /// </summary>
        public string Cookie
        {
            get { return _Cookie; }
            set { _Cookie = value; }
        }
        string _Referer = string.Empty;
        /// <summary>
        /// ��Դ��ַ���ϴη��ʵ�ַ
        /// </summary>
        public string Referer
        {
            get { return _Referer; }
            set { _Referer = value; }
        }
        string _CerPath = string.Empty;
        /// <summary>
        /// ֤�����·��
        /// </summary>
        public string CerPath
        {
            get { return _CerPath; }
            set { _CerPath = value; }
        }
        private Boolean isToLower = false;
        /// <summary>
        /// �Ƿ�����Ϊȫ��Сд��Ĭ��Ϊ��ת��
        /// </summary>
        public Boolean IsToLower
        {
            get { return isToLower; }
            set { isToLower = value; }
        }
        private Boolean allowautoredirect = false;
        /// <summary>
        /// ֧����תҳ�棬��ѯ���������ת���ҳ�棬Ĭ���ǲ���ת
        /// </summary>
        public Boolean Allowautoredirect
        {
            get { return allowautoredirect; }
            set { allowautoredirect = value; }
        }
        private int connectionlimit = 1024;
        /// <summary>
        /// ���������
        /// </summary>
        public int Connectionlimit
        {
            get { return connectionlimit; }
            set { connectionlimit = value; }
        }
        private string proxyusername = string.Empty;
        /// <summary>
        /// ����Proxy �������û���
        /// </summary>
        public string ProxyUserName
        {
            get { return proxyusername; }
            set { proxyusername = value; }
        }
        private string proxypwd = string.Empty;
        /// <summary>
        /// ���� ����������
        /// </summary>
        public string ProxyPwd
        {
            get { return proxypwd; }
            set { proxypwd = value; }
        }
        private string proxyip = string.Empty;
        /// <summary>
        /// ���� ����IP
        /// </summary>
        public string ProxyIp
        {
            get { return proxyip; }
            set { proxyip = value; }
        }
        private ResultType resulttype = ResultType.String;
        /// <summary>
        /// ���÷�������String��Byte
        /// </summary>
        public ResultType ResultType
        {
            get { return resulttype; }
            set { resulttype = value; }
        }
        private WebHeaderCollection header = new WebHeaderCollection();
        /// <summary>
        /// header����
        /// </summary>
        public WebHeaderCollection Header
        {
            get { return header; }
            set { header = value; }
        }

        private Version _ProtocolVersion;

        /// <summary>
        /// ��ȡ��������������� HTTP �汾�����ؽ��:��������� HTTP �汾��Ĭ��Ϊ System.Net.HttpVersion.Version11��
        /// </summary>
        public Version ProtocolVersion
        {
            get { return _ProtocolVersion; }
            set { _ProtocolVersion = value; }
        }
        private Boolean _expect100continue = true;
        /// <summary>
        /// ��ȡ������һ�� System.Boolean ֵ����ֵȷ���Ƿ�ʹ�� 100-Continue ��Ϊ����� POST ������Ҫ 100-Continue ��Ӧ����Ϊ true������Ϊ false��Ĭ��ֵΪ true��
        /// </summary>
        public Boolean Expect100Continue
        {
            get { return _expect100continue; }
            set { _expect100continue = value; }
        }
        private X509CertificateCollection _ClentCertificates;
        /// <summary>
        /// ����509֤�鼯��
        /// </summary>
        public X509CertificateCollection ClentCertificates
        {
            get { return _ClentCertificates; }
            set { _ClentCertificates = value; }
        }
        private Encoding _PostEncoding;
        /// <summary>
        /// ���û��ȡPost��������,Ĭ�ϵ�ΪDefault����
        /// </summary>
        public Encoding PostEncoding
        {
            get { return _PostEncoding; }
            set { _PostEncoding = value; }
        }
    }
    /// <summary>
    /// Http���ز�����
    /// </summary>
    public class HttpResult
    {
        private string _Cookie;
        /// <summary>
        /// Http���󷵻ص�Cookie
        /// </summary>
        public string Cookie
        {
            get { return _Cookie; }
            set { _Cookie = value; }
        }

        private CookieCollection _CookieCollection;
        /// <summary>
        /// Cookie���󼯺�
        /// </summary>
        public CookieCollection CookieCollection
        {
            get { return _CookieCollection; }
            set { _CookieCollection = value; }
        }
        private string _Html;
        /// <summary>
        /// ���ص�String�������� ֻ��ResultType.Stringʱ�ŷ������ݣ��������Ϊ��
        /// </summary>
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        private byte[] _ResultByte;
        /// <summary>
        /// ���ص�Byte���� ֻ��ResultType.Byteʱ�ŷ������ݣ��������Ϊ��
        /// </summary>
        public byte[] ResultByte
        {
            get { return _ResultByte; }
            set { _ResultByte = value; }
        }

        private WebHeaderCollection _Header;
        /// <summary>
        /// header����
        /// </summary>
        public WebHeaderCollection Header
        {
            get { return _Header; }
            set { _Header = value; }
        }

        private string _StatusDescription;
        /// <summary>
        /// ����״̬˵��
        /// </summary>
        public string StatusDescription
        {
            get { return _StatusDescription; }
            set { _StatusDescription = value; }
        }
        private HttpStatusCode _StatusCode;
        /// <summary>
        /// ����״̬��,Ĭ��ΪOK
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return _StatusCode; }
            set { _StatusCode = value; }
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// ��ʾֻ�����ַ��� ֻ��Html������
        /// </summary>
        String,
        /// <summary>
        /// ��ʾ�����ַ������ֽ��� ResultByte��Html�������ݷ���
        /// </summary>
        Byte
    }
    /// <summary>
    /// Post�����ݸ�ʽĬ��Ϊstring
    /// </summary>
    public enum PostDataType
    {
        /// <summary>
        /// �ַ������ͣ���ʱ����Encoding�ɲ�����
        /// </summary>
        String,
        /// <summary>
        /// Byte���ͣ���Ҫ����PostdataByte������ֵ����Encoding������Ϊ��
        /// </summary>
        Byte,
        /// <summary>
        /// ���ļ���Postdata��������Ϊ�ļ��ľ���·������������Encoding��ֵ
        /// </summary>
        FilePath
    }
}