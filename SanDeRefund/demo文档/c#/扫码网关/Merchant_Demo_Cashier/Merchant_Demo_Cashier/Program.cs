using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization; //处理json要有用到，需要添加引用 System.Web,Extensions
//另外，需要引用CryptUtil_Csharp项目的dll

using Gma.QrCodeNet.Encoding;
using System.Drawing;

namespace Merchant_Demo_Cashier
{
    class Program
    {
        static string logHeader = "Program_";
        static void Main(string[] args)
        {

            string signType = string.Empty;
            string charset = string.Empty;
            string pfxFilePath = string.Empty;
            string pfxPassword = string.Empty;
            string cerFilePath = string.Empty;
            string merId = string.Empty;


            //读取配置文件
            INIClass iniReader = new INIClass(System.IO.Directory.GetCurrentDirectory() + "\\config.ini");

            pfxFilePath = iniReader.IniReadValue("Main", "pfxFilePath");
            pfxPassword = iniReader.IniReadValue("Main", "pfxPassword");
            cerFilePath = iniReader.IniReadValue("Main", "cerFilePath");
            charset = iniReader.IniReadValue("Main", "charset");
            signType = iniReader.IniReadValue("Main", "signType");
            merId = iniReader.IniReadValue("Main", "merId");

            //定义Json转换类
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            Dictionary<string, object> dicRslt;

            //解密后的服务器返回
            CryptUtils_Csharp.MessageWorker.trafficMessage resp;


            //生成报文
            string serverUrl = iniReader.IniReadValue("扫码支付_预下单", "serverURL");
            Dictionary<string, object> msgData = qrCode_PreCreate(merId);

            //string serverUrl = iniReader.IniReadValue("扫码支付_统一下单并支付", "serverURL");
            //Dictionary<string, object> msgData = qrCode_BarPay(merId);

            //string serverUrl = iniReader.IniReadValue("扫码支付_订单查询", "serverURL");
            //Dictionary<string, object> msgData = qrCode_Query(merId);

            resp = SendMessageSample(
                pfxFilePath,
                pfxPassword,
                cerFilePath,
                serverUrl,
                signType,
                charset,
                msgData
                );
            dicRslt = jsonSer.DeserializeObject(resp.data) as Dictionary<string, object>;

            Dictionary<string, object> respHead = dicRslt["head"] as Dictionary<string, object>;
            if (respHead["respCode"].ToString() != "000000")
            {
                Console.WriteLine(respHead["respMsg"].ToString());
                Console.ReadKey();
                return;
            }

            if (((Dictionary<string, object>)msgData["head"])["method"].ToString() == "sandpay.trade.precreate")
            {//预下单使用qrcode生成二维码，其他的报文用这段代码会报错
                Dictionary<string, object> respData = dicRslt["body"] as Dictionary<string, object>;
                string qrCode = respData["qrCode"].ToString();
                string orderCode = respData["orderCode"].ToString();


                qrCodeToConsole(qrCode);
                Console.Write(@"订单号：" + orderCode);
                qrCodeToImageFile(qrCode, orderCode + ".png",8);

            }



            Console.ReadKey();
        }

        private static void qrCodeToConsole(string qrCodeString)
        {
            
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(qrCodeString);
            for (int j = 0; j < qrCode.Matrix.Width; j++)
            {
                for (int i = 0; i < qrCode.Matrix.Width; i++)
                {

                    char charToPrint = qrCode.Matrix[i, j] ?  '　': '█';
                    Console.Write(charToPrint);
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"Press any key to quit.");
        }


        private static void qrCodeToImageFile(string qrCodeString, string imageFileName, int ZoomMulti)
        {
            Bitmap bmp = GenerateQRCode(qrCodeString, Color.Black, Color.White, ZoomMulti);
            bmp.Save(imageFileName, System.Drawing.Imaging.ImageFormat.Png);
        }


        private static Bitmap GenerateQRCode(string text, Color DarkColor, Color LightColor, int ZoomMulti)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);

            QrCode Code = qrEncoder.Encode(text);

            Bitmap TempBMP = new Bitmap(Code.Matrix.Width * ZoomMulti, Code.Matrix.Height * ZoomMulti);
            for (int X = 0; X <= Code.Matrix.Width * ZoomMulti - 1; X++)
            {
                for (int Y = 0; Y <= Code.Matrix.Height * ZoomMulti - 1; Y++)
                {
                    if (Code.Matrix.InternalArray[X / ZoomMulti, Y / ZoomMulti])
                        TempBMP.SetPixel(X, Y, DarkColor);
                    else
                        TempBMP.SetPixel(X, Y, LightColor);
                }
            }
            return TempBMP;
        }

        private static CryptUtils_Csharp.MessageWorker.trafficMessage SendMessageSample(
              string pfxFilePath,
              string pfxPassword,
              string cerFilePath,
              string ServerUrl,
              string signType,
              string charset,
              Dictionary<string, object> msgData)
        {

            //报文结构体初始化
            CryptUtils_Csharp.MessageWorker.trafficMessage msgRequestSource = new CryptUtils_Csharp.MessageWorker.trafficMessage();
            //发送类实体化
            CryptUtils_Csharp.MessageWorker worker = new CryptUtils_Csharp.MessageWorker();


            worker.PFXFile = pfxFilePath; //商户pfx证书路径
            worker.PFXPassword = pfxPassword;  //商户pfx证书密码
            worker.CerFile = cerFilePath; //杉德cer证书路径


            msgRequestSource.charset = charset; //商户号
            msgRequestSource.signType = signType;        //交易代码
            msgRequestSource.extend = "";               //扩展域

            //报文体json
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            msgRequestSource.data = jsonSer.Serialize(msgData);


            CryptUtils_Csharp.Logger.Logging(logHeader, "待发送报文为：" + msgRequestSource.data, true);

            CryptUtils_Csharp.MessageWorker.trafficMessage respMessage = worker.postMessage(ServerUrl, msgRequestSource);

            CryptUtils_Csharp.Logger.Logging(logHeader, "服务器返回为：" + respMessage.data, true);
            return respMessage;
        }



        #region 报文生成


        /// <summary>
        /// 扫码支付 统一下单并支付
        /// </summary>
        private static Dictionary<string, object> qrCode_BarPay(string merid)
        {

            string strDateTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");

            Dictionary<string, object> head = new Dictionary<string, object>();
            head.Add("accessType", "1");
            head.Add("channelType", "07");
            head.Add("method", "sandpay.trade.barpay");
            head.Add("mid", merid);
            head.Add("productId", "00000005");
            head.Add("reqTime", strDateTime);
            head.Add("subMid", "");
            head.Add("subMidAddr", "");
            head.Add("subMidName", "");
            head.Add("version", "1.0");


            Dictionary<string, object> body = new Dictionary<string, object>();


            body.Add("authCode", "130029746142310153");
            body.Add("bizExtendParams", "");
            body.Add("body", "用户购买话费0.01");
            body.Add("extend", "");
            body.Add("merchExtendParams", "");
            body.Add("notifyUrl", "http://192.168.22.116:8086/merPayReturn/");
            body.Add("operatorId", "");
            body.Add("orderCode", strDateTime + "0000001");
            body.Add("payTool", "0402");
            body.Add("scene", "1");
            body.Add("storeId", "");
            body.Add("subject", "话费充值");
            body.Add("terminalId", "");
            body.Add("totalAmount", "000000000001");
            body.Add("txnTimeOut", "20161229170000");

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("head", head);
            dic.Add("body", body);



            return dic;
        }


        /// <summary>
        /// 扫码支付 预下单接口
        /// </summary>
        private static Dictionary<string, object> qrCode_PreCreate(string merid)
        {

            string strDateTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");

            Dictionary<string, object> head = new Dictionary<string, object>();
            head.Add("accessType", "1");
            head.Add("channelType", "07");
            head.Add("method", "sandpay.trade.precreate");
            head.Add("mid", merid);
            head.Add("productId", "00000005");
            head.Add("reqTime", strDateTime);
            head.Add("subMid", "");
            head.Add("subMidAddr", "");
            head.Add("subMidName", "");
            head.Add("version", "1.0");


            Dictionary<string, object> body = new Dictionary<string, object>();
            body.Add("bizExtendParams", "");
            body.Add("body", "用户购买话费0.01");
            body.Add("extend", "");
            body.Add("limitPay", "1");
            body.Add("merchExtendParams", "");
            body.Add("notifyUrl", "http://192.168.22.116:8086/merPayReturn/");
            body.Add("operatorId", "");
            body.Add("orderCode", strDateTime + "0000001");
            body.Add("payTool", "0401");
            body.Add("storeId", "");
            body.Add("subject", "话费充值");
            body.Add("terminalId", "");
            body.Add("totalAmount", "000000000001");
            body.Add("txnTimeOut", "20171229170000");

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("head", head);
            dic.Add("body", body);



            return dic;
        }


        /// <summary>
        /// 扫码支付 订单查询
        /// </summary>
        private static Dictionary<string, object> qrCode_Query(string merid)
        {

            string strDateTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");

            Dictionary<string, object> head = new Dictionary<string, object>();
            head.Add("accessType", "1");
            head.Add("channelType", "07");
            head.Add("method", "sandpay.trade.query");
            head.Add("mid", merid);
            head.Add("productId", "00000005");
            head.Add("reqTime", strDateTime);
            head.Add("subMid", "");
            head.Add("subMidAddr", "");
            head.Add("subMidName", "");
            head.Add("version", "1.0");


            Dictionary<string, object> body = new Dictionary<string, object>();

            body.Add("orderCode", "201702030332580000001");
            body.Add("extend", "");

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("head", head);
            dic.Add("body", body);


            return dic;
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
