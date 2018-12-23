using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class RSAUtils
    {
        const int Max_Block = 117;
        const int Res_Max_Block = 128;

        /// <summary>
        /// 使用RSA实现加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <returns></returns>
        public static string RSAEncrypt(string data, string publicKey)
        {
            //创建RSA对象并载入[公钥]
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider(1024);
            rsaPublic.FromXmlString(publicKey);
            int bufferSize = rsaPublic.KeySize / 8 - 11;
            byte[] buffer = new byte[bufferSize];
            //分段加密
            using (MemoryStream input = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            using (MemoryStream ouput = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, bufferSize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] encrypt = rsaPublic.Encrypt(temp, false);
                    ouput.Write(encrypt, 0, encrypt.Length);
                }
                return Convert.ToBase64String(ouput.ToArray());
            }
        }
        /// <summary>
        /// RSA将公钥PEM生成XML格式
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string RSAPublicKeyJava2DotNetP(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
            Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
            Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary> 
        /// RSA私钥格式转换，java->.net
        /// </summary> 
        /// <param name="privateKey">java生成的RSA私钥</param> 
        /// <returns></returns> 
        public static string RSAPrivateKeyJava2DotNet(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey)) as RsaPrivateCrtKeyParameters;

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
            Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary>
        /// 通过XML私钥进行RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, byte[] data, Encoding encoding)
        {
            try
            {
                byte[] DypherTextBArray;
                string Result;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);

                int Len = data.Length;
                int offset = 0;
                List<byte> cache = new List<byte>();
                while (Len - offset > 0)
                {
                    byte[] PlainTextBArray = new byte[Res_Max_Block];
                    if (Len - offset > Res_Max_Block)
                    {
                        Array.Copy(data, offset, PlainTextBArray, 0, Res_Max_Block);
                    }
                    else
                    {
                        Array.Copy(data, offset, PlainTextBArray, 0, Len - offset);
                    }
                    offset += Res_Max_Block;
                    DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                    cache.AddRange(DypherTextBArray);
                }

                Result = encoding.GetString(cache.ToArray());
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}