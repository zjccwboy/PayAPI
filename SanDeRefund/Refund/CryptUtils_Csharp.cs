using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using log4net;

namespace CryptUtils_Csharp
{
    public class CryptUtils
    {
        public static byte[] AESEncrypt(byte[] Data, string Key)
        {
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(Key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Data, 0, Data.Length);

            return resultArray;
        }

        public static byte[] AESDecrypt(byte[] Data, string Key)
        {

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(Key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Data, 0, Data.Length);

            return resultArray;
        }


        public static string GuidTo16String()
        {
            string dic = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int keylen = dic.Length;
            long x = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                x *= ((int)b + 1);
            string value = string.Empty;
            Random ra = new Random((int)(x & 0xffffffffL) | (int)(x >> 32));
            for (int i = 0; i < 16; i++)
            {
                value += dic[ra.Next() % keylen];
            }
            return value;
        }

        public static string getStringFromBytes(byte[] hexbytes, Encoding enc)
        {
            return enc.GetString(hexbytes);
        }

        public static byte[] getBytesFromString(string str, Encoding enc)
        {
            return enc.GetBytes(str);
        }

        public static byte[] asc2hex(string hexString)
        {

            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string hex2asc(byte[] hexbytes)
        {
            return BitConverter.ToString(hexbytes).Replace("-", string.Empty);

        }
        /// <summary>   
        /// RSA解密   要加密较长的数据，则可以采用分段加解密的方式
        /// </summary>   
        /// <param name="xmlPrivateKey"></param>   
        /// <param name="EncryptedBytes"></param>   
        /// <returns></returns>   
        public static byte[] RSADecrypt(string xmlPrivateKey, byte[] EncryptedBytes)
        {

            using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
            {
                RSACryptography.FromXmlString(xmlPrivateKey);


                int MaxBlockSize = RSACryptography.KeySize / 8;    //解密块最大长度限制

                if (EncryptedBytes.Length <= MaxBlockSize)
                    return RSACryptography.Decrypt(EncryptedBytes, false);

                using (MemoryStream CrypStream = new MemoryStream(EncryptedBytes))
                using (MemoryStream PlaiStream = new MemoryStream())
                {
                    Byte[] Buffer = new Byte[MaxBlockSize];
                    int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                    while (BlockSize > 0)
                    {
                        Byte[] ToDecrypt = new Byte[BlockSize];
                        Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                        Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
                        PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                        BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                    }

                    return PlaiStream.ToArray();
                }
            }
        }


        /// <summary>   
        /// RSA加密   要加密较长的数据，则可以采用分段加解密的方式
        /// </summary>   
        /// <param name="xmlPublicKey"></param>   
        /// <param name="SourceBytes"></param>   
        /// <returns></returns>   
        public static byte[] RSAEncrypt(string xmlPublicKey, byte[] SourceBytes)
        {
            using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
            {
                RSACryptography.FromXmlString(xmlPublicKey);

                int MaxBlockSize = RSACryptography.KeySize / 8 - 11;    //加密块最大长度限制

                if (SourceBytes.Length <= MaxBlockSize)
                    return RSACryptography.Encrypt(SourceBytes, false);

                using (MemoryStream PlaiStream = new MemoryStream(SourceBytes))
                using (MemoryStream CrypStream = new MemoryStream())
                {
                    Byte[] Buffer = new Byte[MaxBlockSize];
                    int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                    while (BlockSize > 0)
                    {
                        Byte[] ToEncrypt = new Byte[BlockSize];
                        Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                        Byte[] Cryptograph = RSACryptography.Encrypt(ToEncrypt, false);
                        CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                        BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                    }

                    return CrypStream.ToArray();
                }
            }
        }


        public static X509Certificate2 getPrivateKeyXmlFromPFX(string PFX_file, string password)
        {
            return new X509Certificate2(PFX_file, password, X509KeyStorageFlags.Exportable);
        }

        public static X509Certificate2 getPublicKeyXmlFromCer(string Cer_file)
        {
            return new X509Certificate2(Cer_file);
        }


        public static byte[] CreateSignWithPrivateKey(byte[] msgin, X509Certificate2 pfx)
        {
            HashAlgorithm SHA1 = HashAlgorithm.Create("sha1");
            byte[] hashdata = SHA1.ComputeHash(msgin);//求数字指纹

            try
            {
                AsymmetricAlgorithm prikey = pfx.PrivateKey;
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
            }

            RSAPKCS1SignatureFormatter signCrt = new RSAPKCS1SignatureFormatter();
            signCrt.SetKey(pfx.PrivateKey);
            signCrt.SetHashAlgorithm("sha1");
            return signCrt.CreateSignature(hashdata);
        }

        public static bool VerifySignWithPublicKey(byte[] msgin, X509Certificate2 cer, byte[] sign)
        {
            HashAlgorithm SHA1 = HashAlgorithm.Create("sha1");
            byte[] hashdata = SHA1.ComputeHash(msgin);//求数字指纹

            RSACryptoServiceProvider signV = new RSACryptoServiceProvider();
            signV.FromXmlString(cer.PublicKey.Key.ToXmlString(false));
            return signV.VerifyData(msgin, CryptoConfig.MapNameToOID("SHA1"), sign);
        }

        public static string Base64Encoder(byte[] b)
        {
            return Convert.ToBase64String(b);
        }

        public static byte[] Base64Decoder(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

    }



    public class MessageWorker
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MessageWorker));

        public struct trafficMessage
        {
            public string charset;  //合作商户ID 杉德系统分配，唯一标识
            public string signType;// 加密后的AES秘钥 公钥加密(RSA/ECB/PKCS1Padding)，加密结果采用base64编码
            public string data; //加密后的请求/应答报文 AES加密(AES/ECB/PKCS5Padding)，加密结果采用base64编码
            public string sign;//    签名 对encryptData对应的明文进行签名(SHA1WithRSA)，签名结果采用base64编码
            public string extend;//  扩展域 暂时不用
        }

        private Encoding encodeCode = Encoding.UTF8;


        private string pfxFilePath = string.Empty;
        public string PFXFile
        {
            get
            {
                return pfxFilePath;
            }
            set
            {
                pfxFilePath = value;
            }
        }

        private string pfxPassword = string.Empty;
        public string PFXPassword
        {
            get
            {
                return pfxPassword;
            }
            set
            {
                pfxPassword = value;
            }
        }


        private string cerFilePath = string.Empty;
        public string CerFile
        {
            get
            {
                return cerFilePath;
            }
            set
            {
                cerFilePath = value;
            }
        }
        public trafficMessage UrlDecodeMessage(string msgResponse)
        {
            trafficMessage msgEncrypt = new trafficMessage();
            string[] EncryptBody = System.Web.HttpUtility.UrlDecode(msgResponse).Split('&');
            for (int i = 0; i < EncryptBody.Length; i++)
            {
                string[] tmp = EncryptBody[i].Split('=');
                switch (tmp[0])
                {
                    //需要添加引用System.Web，用于url转码，处理base64产生的+/=
                    case "charset": msgEncrypt.charset = EncryptBody[i].Replace("charset=", "").Trim('"'); break;
                    case "signType": msgEncrypt.signType = EncryptBody[i].Replace("signType=", "").Trim('"'); break;
                    case "data": msgEncrypt.data = EncryptBody[i].Replace("data=", "").Trim('"'); break;
                    case "sign": msgEncrypt.sign = EncryptBody[i].Replace("sign=", "").Trim('"'); break;
                    case "extend": msgEncrypt.extend = EncryptBody[i].Replace("extend=", "").Trim('"'); break;
                }
            }
            return msgEncrypt;
        }
        public trafficMessage SplitMessage(string msgResponse)
        {
            trafficMessage msgEncrypt = new trafficMessage();
            string[] EncryptBody = msgResponse.Split('&');
            for (int i = 0; i < EncryptBody.Length; i++)
            {
                string[] tmp = EncryptBody[i].Split('=');
                switch (tmp[0])
                {
                    //需要添加引用System.Web，用于url转码，处理base64产生的+/=
                    case "charset": msgEncrypt.charset = EncryptBody[i].Replace("charset=", "").Trim('"'); break;
                    case "signType": msgEncrypt.signType = EncryptBody[i].Replace("signType=", "").Trim('"'); break;
                    case "data": msgEncrypt.data = EncryptBody[i].Replace("data=", "").Trim('"'); break;
                    case "sign": msgEncrypt.sign = EncryptBody[i].Replace("sign=", "").Trim('"'); break;
                    case "extend": msgEncrypt.extend = EncryptBody[i].Replace("extend=", "").Trim('"'); break;
                }
            }
            return msgEncrypt;
        }

        public string UrlEncodeMessage(trafficMessage msgRequest)
        {
            //需要添加引用System.Web，用于url转码，处理base64产生的+/=
            return "charset=" + System.Web.HttpUtility.UrlEncode(msgRequest.charset) + "&" +
                 "signType=" + System.Web.HttpUtility.UrlEncode(msgRequest.signType) + "&" +
                  "data=" + System.Web.HttpUtility.UrlEncode(msgRequest.data) + "&" +
                   "sign=" + System.Web.HttpUtility.UrlEncode(msgRequest.sign) + "&" +
                   "extend=" + System.Web.HttpUtility.UrlEncode(msgRequest.extend);
        }



        public trafficMessage SignMessageBeforePost(trafficMessage msgSource)
        {
            trafficMessage msgEncrypt = new trafficMessage();


            //获取报文字符集
            this.encodeCode = Encoding.GetEncoding(msgSource.charset);
            log.Debug("Message Charset is [" + msgSource.charset + "]");

            msgEncrypt.charset = msgSource.charset;
            msgEncrypt.signType = msgSource.signType;
            msgEncrypt.extend = msgSource.extend;
            msgEncrypt.data = msgSource.data;

            //报文签名
            msgEncrypt.sign = CryptUtils.Base64Encoder(CryptUtils.CreateSignWithPrivateKey(CryptUtils.getBytesFromString(msgSource.data, encodeCode),
                CryptUtils.getPrivateKeyXmlFromPFX(pfxFilePath, pfxPassword)));

           log.Debug( "sign[" + msgEncrypt.sign + "]");

            return msgEncrypt;

        }
        public trafficMessage CheckSignMessageAfterResponse(trafficMessage msgEncrypt)
        {
            trafficMessage msgSource = new trafficMessage();

            //获取报文字符集
            this.encodeCode = Encoding.GetEncoding(msgEncrypt.charset);
           log.Debug( "Message Charset is [" + msgEncrypt.charset + "]");

            msgSource.charset = msgEncrypt.charset;
            msgSource.signType = msgEncrypt.signType;
            msgSource.extend = msgEncrypt.extend;
            msgSource.data = msgEncrypt.data;

            msgSource.sign = CryptUtils.VerifySignWithPublicKey(
                                (CryptUtils.getBytesFromString(msgEncrypt.data, encodeCode)),
                                CryptUtils.getPublicKeyXmlFromCer(cerFilePath),
                                CryptUtils.Base64Decoder(msgEncrypt.sign)
                                ).ToString();

           log.Debug( "sign[" + msgSource.sign + "][" + msgEncrypt.sign + "]");
            return msgSource;
        }


        public trafficMessage postMessage(string serverUrl, trafficMessage requestSourceMessage)
        {
            trafficMessage responseMessage = new trafficMessage();
            try
            {
                string requestString = UrlEncodeMessage(SignMessageBeforePost(requestSourceMessage));
                //Console.WriteLine("url:" + serverUrl);
               log.Debug( "request  ==>[" + requestString + "]");

                string responseString = HttpUtils.HttpPost(serverUrl, requestString, encodeCode);
               log.Debug( "response <==[" + responseString + "]");
                responseMessage = CheckSignMessageAfterResponse(UrlDecodeMessage(responseString));
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
                responseMessage.extend = er.ToString();
            }
            return responseMessage;
        }
    }

    public class HttpUtils
    {
        public static string HttpPost(string postUrl, string paramData, Encoding EncodingName)
        {
            string postDataStr = paramData;
            byte[] buff = EncodingName.GetBytes(postDataStr);

            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (postUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(postUrl) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(postUrl) as HttpWebRequest;
            }

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 10000;
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(buff, 0, buff.Length);
            myRequestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, EncodingName);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }

}
