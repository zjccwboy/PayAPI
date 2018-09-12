﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestHYPay
{
    class Program
    {
        static void Main(string[] args)
        {
            var insCode = "80000384";
            var insMerchantCode = "887581298600467";
            var hpMerCode = "WKJGWKQTCS@20180813173307";
            var orderNo = DateTime.Now.Ticks.ToString();
            var orderTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            var currencyCode = "156";
            var orderAmount = "10";
            var name = "互联网";
            var idNumber = "341126197709218366";
            var accNo = "6221558812340000";
            var telNo = "13552535506";
            var productType = "100000";
            var paymentType = "2008";
            var merGroup = "";
            var nonceStr = new Random().Next(1, 12).ToString();
            var frontUrl = "http://47.92.68.54/sync";
            var backUrl = "http://47.92.68.54/async";


            var signKey = "3F7DB75AFBE34A4B40ECD0CC4A8B6492";
            var signString = $"{insCode}|{insMerchantCode}|{hpMerCode}|{orderNo}|" +
                $"{orderTime}|{orderAmount}|{name}|{idNumber}|{accNo}|{telNo}|{productType}|{paymentType}|{nonceStr}|{signKey}";

            var signature = GetMD5(signString);

            string postString = $"insCode={insCode}&" +
                $"insMerchantCode={insMerchantCode}&" +
                $"hpMerCode={hpMerCode}&" +
                $"orderNo={orderNo}&" +
                $"orderTime={orderTime}&" +
                $"currencyCode={currencyCode}&" +
                $"orderAmount={orderAmount}&" +
                $"name={name}&" +
                $"idNumber={idNumber}&" +
                $"accNo={accNo}&" +
                $"telNo={telNo}&" +
                $"productType={productType}&" +
                $"paymentType={paymentType}&" +
                $"nonceStr={nonceStr}&" +
                $"frontUrl={frontUrl}&" +
                $"backUrl={backUrl}&" +
                $"signature={signature}";

            Console.WriteLine(postString);

            byte[] postData = Encoding.UTF8.GetBytes(postString);
            string url = "http://180.168.61.86:27380/hpayTransGatewayWeb/trans/debit.htm";
            var webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            string srcString = Encoding.UTF8.GetString(responseData);
            Console.Write(srcString);

            Console.Read();
        }

        static string GetSignature(string signString)
        {
            var bytes = Encoding.Default.GetBytes(signString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(bytes);
            var result = BitConverter.ToString(output).Replace("-", "");
            return result;
        }




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

            return byte2String;
        }


        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            return System.Text.Encoding.Default.GetString(result);
        }

    }
}
