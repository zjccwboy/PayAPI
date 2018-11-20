using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization; //处理json要有用到，需要添加引用 System.Web,Extensions
//另外，需要引用CryptUtil_Csharp项目的dll

namespace testDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverURL = string.Empty;
            string transCode = string.Empty;
            string pfxFilePath = string.Empty;
            string pfxPassword = string.Empty;
            string cerFilePath = string.Empty;
            string merId = string.Empty;

            //读取配置文件
            INIClass iniReader = new INIClass(System.IO.Directory.GetCurrentDirectory() + "\\config.ini");

            pfxFilePath = iniReader.IniReadValue("Main", "pfxFilePath");
            pfxPassword = iniReader.IniReadValue("Main", "pfxPassword");
            cerFilePath = iniReader.IniReadValue("Main", "cerFilePath");
            merId = iniReader.IniReadValue("Main", "merId");

            //定义Json转换类
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            Dictionary<string, string> dicRslt;

            //解密后的服务器返回
            CryptUtils_Csharp.MessageCryptWorker.trafficMessage resp;
            
            // 实时代收
            serverURL = iniReader.IniReadValue("实时代收", "serverURL");
            transCode = iniReader.IniReadValue("实时代收", "transCode");
            resp =  SampleCollectionMessage(pfxFilePath, pfxPassword, cerFilePath, merId, serverURL,transCode);
            //检查验签结果
            Console.WriteLine("验签结果" + resp.sign);
            //解析报文，读取业务报文体内具体字段的值
            dicRslt = jsonSer.Deserialize<Dictionary<string, string>>(resp.encryptData);
            Console.WriteLine("respCode[" + dicRslt["respCode"] + "]" + "respDesc[" + dicRslt["respDesc"] + "]");

            // 实时代付
            serverURL = iniReader.IniReadValue("实时代付", "serverURL");
            transCode = iniReader.IniReadValue("实时代付", "transCode");
            resp = SampleAgentPayMessage(pfxFilePath, pfxPassword, cerFilePath, merId, serverURL, transCode);
            //检查验签结果
            Console.WriteLine("验签结果" + resp.sign);
            //解析报文，读取业务报文体内具体字段的值
            dicRslt = jsonSer.Deserialize<Dictionary<string, string>>(resp.encryptData);
            Console.WriteLine("respCode[" + dicRslt["respCode"] + "]" + "respDesc[" + dicRslt["respDesc"] + "]");

            // 实时代付手续费查询
            serverURL = iniReader.IniReadValue("实时代付手续费查询", "serverURL");
            transCode = iniReader.IniReadValue("实时代付手续费查询", "transCode");
            resp = SampleQueryAgentpayFeeMessage(pfxFilePath, pfxPassword, cerFilePath, merId, serverURL, transCode);
            //检查验签结果
            Console.WriteLine("验签结果" + resp.sign);
            //解析报文，读取业务报文体内具体字段的值
            dicRslt = jsonSer.Deserialize<Dictionary<string, string>>(resp.encryptData);
            Console.WriteLine("respCode[" + dicRslt["respCode"] + "]" + "respDesc[" + dicRslt["respDesc"] + "]");

            // 订单查询
            serverURL = iniReader.IniReadValue("订单查询", "serverURL");
            transCode = iniReader.IniReadValue("订单查询", "transCode");
            resp = SampleQueryOrderMessage(pfxFilePath, pfxPassword, cerFilePath, merId, serverURL, transCode);
            Console.WriteLine("验签结果" + resp.sign);
            //检查验签结果
            Console.WriteLine("验签结果" + resp.sign);
            //解析报文，读取业务报文体内具体字段的值
            dicRslt = jsonSer.Deserialize<Dictionary<string, string>>(resp.encryptData);
            Console.WriteLine("respCode[" + dicRslt["respCode"] + "]" + "respDesc[" + dicRslt["respDesc"] + "]");

            // 账户余额查询
            serverURL = iniReader.IniReadValue("账户余额查询", "serverURL");
            transCode = iniReader.IniReadValue("账户余额查询", "transCode");
            resp = SampleQueryBalanceMessage(pfxFilePath, pfxPassword, cerFilePath, merId, serverURL, transCode);
            Console.WriteLine("验签结果" + resp.sign);
            //检查验签结果
            Console.WriteLine("验签结果" + resp.sign);
            //解析报文，读取业务报文体内具体字段的值
            dicRslt = jsonSer.Deserialize<Dictionary<string, string>>(resp.encryptData);
            Console.WriteLine("respCode["+dicRslt["respCode"] +"]" + "respDesc["+ dicRslt["respDesc"] + "]");

            Console.ReadKey();
        }



        #region 报文生成及发送

        /// <summary>
        /// 实时代收样例
        /// </summary>
        /// <returns></returns>
        private static CryptUtils_Csharp.MessageCryptWorker.trafficMessage SampleCollectionMessage(
                string pfxFilePath,
                string pfxPassword,
                string cerFilePath,
                string merId,
                string ServerUrl,
                string transCode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("version", "01");
            dic.Add("cityNo", "010000");
            dic.Add("certType", "0001");
            dic.Add("productId", "00000002");
            dic.Add("purpose", "collection");
            dic.Add("accNo", "6226220209634996");
            dic.Add("accName", "TEST");
            dic.Add("bankInsCode", "48270000");
            dic.Add("bankName", "世界银行");
            dic.Add("accAttr", "0");
            dic.Add("timeOut", "20161115123021");
            dic.Add("certNo", "321281198702253717");
            dic.Add("tranTime", "20161114123021");
            dic.Add("provNo", "010000");
            dic.Add("phone", "12345678901");
            dic.Add("cardId", "321281198702253717");
            dic.Add("tranAmt", "000000000100");
            dic.Add("orderCode", "201611131000001042");
            dic.Add("accType", "4");
            dic.Add("currencyCode", "156");
            //报文结构体初始化
            CryptUtils_Csharp.MessageCryptWorker.trafficMessage msgRequestSource = new CryptUtils_Csharp.MessageCryptWorker.trafficMessage();
            //发送类实体化
            CryptUtils_Csharp.MessageCryptWorker worker = new CryptUtils_Csharp.MessageCryptWorker();
            worker.EncodeCode = Encoding.UTF8; //encoding 使用utf8

            worker.PFXFile = pfxFilePath ; //商户pfx证书路径
            worker.PFXPassword = pfxPassword;  //商户pfx证书密码
            worker.CerFile = cerFilePath; //杉德cer证书路径


            msgRequestSource.merId = merId; //商户号
            msgRequestSource.transCode = transCode;        //交易代码
            msgRequestSource.extend = "";               //扩展域

            //报文体json
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            msgRequestSource.encryptData = jsonSer.Serialize(dic);
            //encrytpKey会在发送前加密时自动生成16位的随机字符

            Console.WriteLine("待发送报文为：" + msgRequestSource.encryptData);

            CryptUtils_Csharp.MessageCryptWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);
            Console.WriteLine("服务器返回为：" + respMessage.encryptData);
            return respMessage;
        }

        /// <summary>
        /// 实时代付样例
        /// </summary>
        /// <returns></returns>
        private static CryptUtils_Csharp.MessageCryptWorker.trafficMessage SampleAgentPayMessage(
                string pfxFilePath,
                string pfxPassword,
                string cerFilePath,
                string merId,
                string ServerUrl,
                string transCode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("version", "01");
            dic.Add("productId", "00000003");// 代收对公    00000001 代收对私    00000002  代付对私    00000004 
            dic.Add("tranTime", "20161114123021");
            dic.Add("orderCode", "20161113000000001038");
            dic.Add("timeOut", "20161115123021");
            dic.Add("tranAmt", "000000000001");
            dic.Add("currencyCode", "156");
            dic.Add("accAttr", "0");
            dic.Add("accType", "2");
            dic.Add("accNo", "6216261000000000018");
            dic.Add("accName", "啊啊");
            dic.Add("bankName", "aaa");
            dic.Add("bankType", "1234567890");
            dic.Add("remark", "pay");
            dic.Add("reqReserved", "");
            dic.Add("noticeUrl", "");
            dic.Add("extend", "");
            //报文结构体初始化
            CryptUtils_Csharp.MessageCryptWorker.trafficMessage msgRequestSource = new CryptUtils_Csharp.MessageCryptWorker.trafficMessage();
            //发送类实体化
            CryptUtils_Csharp.MessageCryptWorker worker = new CryptUtils_Csharp.MessageCryptWorker();
            worker.EncodeCode = Encoding.UTF8; //encoding 使用utf8

            worker.PFXFile = pfxFilePath; //商户pfx证书路径
            worker.PFXPassword = pfxPassword;  //商户pfx证书密码
            worker.CerFile = cerFilePath; //杉德cer证书路径


            msgRequestSource.merId = merId; //商户号
            msgRequestSource.transCode = transCode;        //交易代码
            msgRequestSource.extend = "";               //扩展域

            //报文体json
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            msgRequestSource.encryptData = jsonSer.Serialize(dic);
            //encrytpKey会在发送前加密时自动生成16位的随机字符


            Console.WriteLine("待发送报文为：" + msgRequestSource.encryptData);

            CryptUtils_Csharp.MessageCryptWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);
            Console.WriteLine("服务器返回为：" + respMessage.encryptData);

            return respMessage;
        }

        /// <summary>
        /// 实时代付手续费查询样例
        /// </summary>
        /// <returns></returns>
        private static CryptUtils_Csharp.MessageCryptWorker.trafficMessage SampleQueryAgentpayFeeMessage(string pfxFilePath,
                string pfxPassword,
                string cerFilePath,
                string merId,
                string ServerUrl,
                string transCode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("version", "01");
            dic.Add("productId", "00000003");// 代收对公    00000001 代收对私    00000002  代付对私    00000004 
            dic.Add("tranTime", "20161114123021");
            dic.Add("orderCode", "20161113000000001038");
            dic.Add("tranAmt", "000000000001");
            dic.Add("currencyCode", "156");
            dic.Add("accAttr", "0");
            dic.Add("accType", "2");
            dic.Add("accNo", "6216261000000000018");
            dic.Add("accName", "啊啊");
            dic.Add("bankName", "aaa");
            dic.Add("bankType", "1234567890");
            dic.Add("extend", "");
            //报文结构体初始化
            CryptUtils_Csharp.MessageCryptWorker.trafficMessage msgRequestSource = new CryptUtils_Csharp.MessageCryptWorker.trafficMessage();
            //发送类实体化
            CryptUtils_Csharp.MessageCryptWorker worker = new CryptUtils_Csharp.MessageCryptWorker();
            worker.EncodeCode = Encoding.UTF8; //encoding 使用utf8

            worker.PFXFile = pfxFilePath; //商户pfx证书路径
            worker.PFXPassword = pfxPassword;  //商户pfx证书密码
            worker.CerFile = cerFilePath; //杉德cer证书路径


            msgRequestSource.merId = merId; //商户号
            msgRequestSource.transCode = transCode;        //交易代码
            msgRequestSource.extend = "";               //扩展域

            //报文体json
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            msgRequestSource.encryptData = jsonSer.Serialize(dic);
            //encrytpKey会在发送前加密时自动生成16位的随机字符

            Console.WriteLine("待发送报文为：" + msgRequestSource.encryptData);

            CryptUtils_Csharp.MessageCryptWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);
            Console.WriteLine("服务器返回为：" + respMessage.encryptData);
            return respMessage;
        }

        /// <summary>
        /// 订单查询样例
        /// </summary>
        /// <returns></returns>
        private static CryptUtils_Csharp.MessageCryptWorker.trafficMessage SampleQueryOrderMessage(string pfxFilePath,
                string pfxPassword,
                string cerFilePath,
                string merId,
                string ServerUrl,
                string transCode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("version", "01");
            dic.Add("productId", "00000003");// 代收对公    00000001 代收对私    00000002  代付对私    00000004 
            dic.Add("tranTime", "20161114123021");
            dic.Add("orderCode", "20161113000000001038");
            //报文结构体初始化
            CryptUtils_Csharp.MessageCryptWorker.trafficMessage msgRequestSource = new CryptUtils_Csharp.MessageCryptWorker.trafficMessage();
            //发送类实体化
            CryptUtils_Csharp.MessageCryptWorker worker = new CryptUtils_Csharp.MessageCryptWorker();
            worker.EncodeCode = Encoding.UTF8; //encoding 使用utf8

            worker.PFXFile = pfxFilePath; //商户pfx证书路径
            worker.PFXPassword = pfxPassword;  //商户pfx证书密码
            worker.CerFile = cerFilePath; //杉德cer证书路径


            msgRequestSource.merId = merId; //商户号
            msgRequestSource.transCode = transCode;        //交易代码
            msgRequestSource.extend = "";               //扩展域

            //报文体json
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            msgRequestSource.encryptData = jsonSer.Serialize(dic);
            //encrytpKey会在发送前加密时自动生成16位的随机字符

            Console.WriteLine("待发送报文为：" + msgRequestSource.encryptData);

            CryptUtils_Csharp.MessageCryptWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);

            Console.WriteLine("服务器返回为：" + respMessage.encryptData);
            return respMessage;
        }

        /// <summary>
        /// 账户余额查询样例
        /// </summary>
        /// <returns></returns>
        private static CryptUtils_Csharp.MessageCryptWorker.trafficMessage SampleQueryBalanceMessage(string pfxFilePath,
                string pfxPassword,
                string cerFilePath,
                string merId,
                string ServerUrl,
                string transCode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("version", "01");
            dic.Add("productId", "00000003"); // 代收对公    00000001 代收对私    00000002  代付对私    00000004 
            dic.Add("tranTime", "20161114123021");
            dic.Add("orderCode", "20161113400000001038");
            //报文结构体初始化
            CryptUtils_Csharp.MessageCryptWorker.trafficMessage msgRequestSource = new CryptUtils_Csharp.MessageCryptWorker.trafficMessage();
            //发送类实体化
            CryptUtils_Csharp.MessageCryptWorker worker = new CryptUtils_Csharp.MessageCryptWorker();
            worker.EncodeCode = Encoding.UTF8; //encoding 使用utf8

            worker.PFXFile = pfxFilePath; //商户pfx证书路径
            worker.PFXPassword = pfxPassword;  //商户pfx证书密码
            worker.CerFile = cerFilePath; //杉德cer证书路径


            msgRequestSource.merId = merId; //商户号
            msgRequestSource.transCode = transCode;        //交易代码
            msgRequestSource.extend = "";               //扩展域

            //报文体json
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            msgRequestSource.encryptData = jsonSer.Serialize(dic);
            //encrytpKey会在发送前加密时自动生成16位的随机字符

            Console.WriteLine("待发送报文为：" + msgRequestSource.encryptData);

            CryptUtils_Csharp.MessageCryptWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);
            Console.WriteLine("服务器返回为：" + respMessage.encryptData);
            return respMessage;
        }

        #endregion
    }

    public class INIClass
    {
        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="INIPath">文件路径</param>
        public INIClass(string INIPath)
        {
            inipath = INIPath;
        }
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }
        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }
        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <returns>布尔值</returns>
        public bool ExistINIFile()
        {
            return File.Exists(inipath);
        }
    }
}
