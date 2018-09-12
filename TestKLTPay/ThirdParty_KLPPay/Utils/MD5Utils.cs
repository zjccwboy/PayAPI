using System.Security.Cryptography;
using System.Text;

namespace ThirdParty_KLPPay
{
    public class MD5Utils
    {
        public static string GetMD5(string myString)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String.ToUpper();
        }

        public static string GetSign(string content, string posKey)
        {

            var result = content;
            // key是双方约定的加密秘钥
            result += "&key=" + posKey;
            result = GetMD5(result);
            return result;
        }
    }
}
