using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace deshiRefund
{
    public partial class Refund : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //注意：请将如下参数正确赋值后执行此而即可完成退款操作
            string customerid = Request.Form["customerid"];//商户ID
            string mysp_billno = Request.Form["mysp_billno"];//原交易订单号
            string mysp_billno_time = Request.Form["mysp_billno_time"];//原交易订单时间
            string MyPrice = Request.Form["MyPrice"];//订单金额
                                                     //        string priKey = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAKtRtikKJtykwOw7
                                                     //V5wRd9j80l9HMlt0KBmANyA3Y1BEMwGMRjgWh6zrHPFAxeBqhV6lSB1+G8RvtQxf
                                                     //m5uYIuQaQedNzSY09K3+nTACcRgrABQbERQayOjsxmzfzhUcYoG1DQpMhk7ubZIO
                                                     //9A/E2TkJte6Bs29oVqugQd3NEykLAgMBAAECgYAuQu1enagq0q5p3AhnU2w6guLH
                                                     //6bDWc0JUyIOaRnqh9RiU5W0cvjC65+4z01rbo8gJ67XSiPg0jjmkcrjRRj69Sk5Q
                                                     //xcQR33xts8ygNIYf4Ws4xDxWVfcw0niLRaqgY0Aq+tfdTS92LTLCG2C7QVePyVni
                                                     //kTDxM5GyKJYRxfJXOQJBAOIfiCDxIaTMSktYTEsHTTFbAxTB2fdfFb4Wln+sd+0C
                                                     //UoPhnRC5t+7V+DQrWgXRXGunT6ucBvMEvGsWa+4RiIUCQQDB9HiMBG0DGRxlOMRl
                                                     //X5L/2V80jg7WiO3nzKvbkZ8RQAJc1FdGHd4WgcDaLcXYcxvc4vMZGpICy962clYN
                                                     //pGhPAkEAiOUGCMtyzs5O/CZMqe+VsBelWd+yEayjAR2zpz/GqtoJEoZ4DDQjQpiP
                                                     //VyYXrgX9qb704LPpER8A4uQEG3DJ8QJBALGwkFPuphSFh98wL7WT5u+grLlQQEXJ
                                                     //svN/Lh99fNZn5wI2wzIIoPPLsevwrWYMpwUpon9oOUZ4kjRh8XaUHb8CQFyy4QPd
                                                     //404i0fuABdvdy5WbzxFuKK0qbx8lhKB3srySHuUX4xnUAx3l6mzfUPLLJlnZ5KjH
                                                     //wRR4gtNaeSTvB3s=";//私钥

            var priKey = Request.Form["priKey"];

            if (customerid == null)
                return;


            #region 定义参数
            string version = "1.0";//版本号
            string sign_method = "01";//签名方法
            string sign = "";//签名
            string trans_code = "11";//交易类型
            string mer_code = customerid;//商户代码
            string order_no = MakeFileRndName();//退款订单号
            string str_date = DateTime.UtcNow.AddHours(8).ToString();
            string txn_date = Convert.ToDateTime(str_date).ToString("yyyyMMddHHmmss");//订单发送时间
            string amount = (Convert.ToDecimal(MyPrice) * 100).ToString("f0");//退货金额
            string org_order_no = mysp_billno;//原交易订单号
            string org_txn_date = mysp_billno_time;//原交易订单时间
            string mer_addmsg = "";//商户保留域
            #endregion

            #region 签名
            SortedDictionary<string, string> disMap = new SortedDictionary<string, string>();
            disMap.Add("version", version);
            disMap.Add("sign_method", sign_method);
            disMap.Add("trans_code", trans_code);
            disMap.Add("mer_code", mer_code);
            disMap.Add("order_no", order_no);
            disMap.Add("txn_date", txn_date);
            disMap.Add("amount", amount);
            disMap.Add("org_order_no", org_order_no);
            disMap.Add("org_txn_date", org_txn_date);
            String datasign = null;
            foreach (KeyValuePair<string, string> kvp in disMap)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    datasign = datasign + kvp.Key + "=" + kvp.Value + "&";
                }
            }
            datasign = datasign.Substring(0, datasign.Length - 1);


            sign = Getsign(datasign, priKey, "UTF-8");

            disMap.Add("sign", sign);
            #endregion

            string submitUrl = "http://120.76.161.93/acq-gateway/api/backTxn.do";
            submitUrl = "https://backupay.dayspay.com.cn/acq-gateway/api/backTxn.do";

            string prams = datasign + "&sign=" + sign;

            string RES = HttpPost(submitUrl, prams);
            //WriteTextForWarnString("SSSSSSSSSSSSSSSSSSSSSSSSSSSS");
            //WriteTextForWarnString("退款订单号" + order_no);
            //WriteTextForWarnString("原交易订单号" + org_order_no);
            //WriteTextForWarnString("原交易订单时间" + org_txn_date);
            //WriteTextForWarnString("退款结果" + RES);

            JObject json = JObject.Parse(RES);
            if (json != null)
            {
                Response.Write(RES);
                Response.End();
                //if (json["resp_code"].ToString().Replace("\"", "") == "00")
                //{
                //    Response.Write("{\"respCode\":\"success\",\"respMsg\":\"成功\",\"refundNum\":\"" + order_no + "\",\"refundTime\":\"" + str_date + "\"}");
                //    Response.End();
                //}
                //else
                //{
                //    Response.Write(System.Web.HttpUtility.UrlEncode("{\"respCode\":\"Fail\",\"respMsg\":\"" + json["resp_msg"].ToString().Replace("\"", "") + "\",\"refundNum\":\"" + order_no + "\",\"refundTime\":\"" + str_date + "\"}", Encoding.GetEncoding("GB2312")));
                //    Response.End();
                //}
            }
        }
        static Random random = new Random(unchecked((int)DateTime.Now.Ticks));
        public static string MakeFileRndName()
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmss") + MakeRandomString("0123456789", 4));
        }
        public static string MakeRandomString(string pwdchars, int pwdlen)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < pwdlen; i++)
            {
                int num = random.Next(pwdchars.Length);
                builder.Append(pwdchars[num]);
            }
            return builder.ToString();
        }
        #region 发送请求
        /// <summary>
        /// post请求到指定地址并获取返回的信息内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <param name="encodeType">编码类型如：UTF-8</param>
        /// <returns>返回响应内容</returns>
        public static string HttpPost(string POSTURL, string PostData)
        {
            //发送请求的数据
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(POSTURL);
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.UserAgent = "chrome";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }

        #endregion
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        #region 加密 
        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="content">待签名字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>签名后字符串</returns>
        public static string Getsign(string content, string privateKey, string input_charset)
        {
            byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(SHA1(content, Encoding.UTF8).ToLower());
            RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKey);
            SHA1 sh = new SHA1CryptoServiceProvider();
            byte[] signData = rsa.SignData(Data, sh);
            return BitConverter.ToString(signData).Replace("-", string.Empty);
        }
        private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
        {
            byte[] pkcs8privatekey;
            pkcs8privatekey = Convert.FromBase64String(pemstr);
            if (pkcs8privatekey != null)
            {
                RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8privatekey);
                return rsa;
            }
            else
                return null;
        }
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", string.Empty);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if (twobytes != 0x0001)
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04) //expect an Octet string 
                    return null;

                bt = binr.ReadByte();       //read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                if (bt == 0x81)
                    binr.ReadByte();
                else
                    if (bt == 0x82)
                    binr.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key

                byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                return rsacsp;
            }

            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }
        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)     //expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }



            while (binr.ReadByte() == 0x00)
            {   //remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);       //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
        #endregion
        #region 写入日志
        private static void WriteTextForWarnString(string strUrl)
        {
            FileStream fileSteam = null;
            StreamWriter streamWrite = null;
            try
            {
                string strPath = HttpContext.Current.Request.PhysicalApplicationPath + @"Log\deshiRefund.log";

                fileSteam = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                streamWrite = new StreamWriter(fileSteam, Encoding.GetEncoding("gb2312"));
                //将 BaseStream 与 Seek 和 SeekOrigin 一起使用，将基础流的文件指针设置到末尾。 

                streamWrite.BaseStream.Seek(0, SeekOrigin.End);
                streamWrite.Write(strUrl + "\r\n");

            }
            catch
            {
                WriteTextForWarnString(DateTime.Now.ToString() + "  " + "  ----------  throw error ! \r\n");
            }
            finally
            {
                streamWrite.Flush();
                streamWrite.Close();
                fileSteam.Dispose();
                fileSteam.Close();
            }

        }
        #endregion
    }
}