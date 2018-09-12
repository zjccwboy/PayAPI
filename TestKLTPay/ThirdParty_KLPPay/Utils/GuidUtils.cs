using System;

namespace ThirdParty_KLPPay
{
    public class GuidUtils
    {
        /// <summary>
        /// 由连字符分隔的32位数字
        /// </summary>
        /// <returns></returns>
        private static string GetGuid()
        {
            Guid guid = new Guid();
            guid = Guid.NewGuid();
            return guid.ToString();
        }

        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>
        public static string GetLongStringGuid()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }

        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>
        public static long GetLongGuid()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
