using Lion.BLL;
using Lion.Common;
using Lion.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//得仕退款
public partial class Pay_refundOrder_deshipayRefund : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
        #region "校验QueryString"
        string mysp_billno = "";
        if (Request["num"] != null)
        {
            mysp_billno = Request["num"];
        }
        if (mysp_billno == "")
        {
            Response.Write("缺少参数！");
            Response.End();
        }
        #region 根据订单号查询出本次需要提交的金额 by wkn
        Lion.BLL.MMobile1 mobilebll = new Lion.BLL.MMobile1();
        var mobilemodel = mobilebll.GetModelByNum(mysp_billno);
        if (mobilemodel == null)
        {
            Response.Write(System.Web.HttpUtility.UrlEncode("{\"respCode\":\"Warning\",\"respMsg\":\"订单不存在\",\"refundNum\":\"\",\"refundTime\":\"\"}", Encoding.GetEncoding("GB2312")));
            Response.End();
        }

        string MyPrice = mobilemodel.Price.ToString("f2");
        #endregion

        string customerid = "";
        string key = "";
        string priKey = "";
        string publicKey = "";

        L_pay l_pay = new L_pay();
        ML_pay managerl_pay = new ML_pay();
        l_pay = managerl_pay.GetModel(" ptype='deshipay'");
        if (l_pay == null)
        {
            Response.Write(System.Web.HttpUtility.UrlEncode("{\"respCode\":\"Warning\",\"respMsg\":\"支付未设置\",\"refundNum\":\"\",\"refundTime\":\"\"}", Encoding.GetEncoding("GB2312")));
            Response.End();
        }
        else
        {
            string str_tempUrl = l_pay.SumbitUrl;
            if (Request["goto"] != "1" && str_tempUrl.Trim().ToLower().Replace("https://", "").Replace("http://", "").Length > 0)
            {
                string str_temp_GotoUrl = "../default.aspx";
                str_temp_GotoUrl += "&num=" + Request["num"];
                str_temp_GotoUrl += "&PayUrl=" + str_tempUrl.ToLower().Trim() + "/Pay/refundOrder/deshipayRefund.aspx";
                str_temp_GotoUrl += "&UserUrl=" + Request.ServerVariables["HTTP_HOST"];
                Response.Redirect(str_temp_GotoUrl, true);
            }

            customerid = l_pay.Sid;                                       // 这里替换为您的实际商户号
            priKey = l_pay.SKey;
            publicKey = l_pay.Other;
            try
            {
                priKey = StringHelper.Decrypt_KeyValue(l_pay.SKey);
            }
            catch
            {
                priKey = l_pay.SKey;
            }
        }

        if (customerid == string.Empty || priKey == string.Empty || publicKey == string.Empty)
        {
            Response.Write(System.Web.HttpUtility.UrlEncode("{\"respCode\":\"Warning\",\"respMsg\":\"支付未设置\",\"refundNum\":\"\",\"refundTime\":\"\"}", Encoding.GetEncoding("GB2312")));
            Response.End();
        }
        #endregion "校验QueryString"

        #region 定义参数
        string version = "1.0";//版本号
        string sign_method = "01";//签名方法
        string sign = "";//签名
        string trans_code = "11";//交易类型
        string mer_code = customerid;//商户代码
        string order_no = DataSecurity.MakeFileRndName();//商户订单号
        string str_date = DateTime.Now.ToString();
        string txn_date = Convert.ToDateTime(str_date).ToString("yyyyMMddHHmmss");//订单发送时间
        string amount = (Convert.ToDecimal(MyPrice) * 100).ToString("f0");//退货金额
        string org_order_no = mysp_billno;//原交易订单号
        string org_txn_date = mobilemodel.Num.Substring(0, 14);//原交易订单时间
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


        sign = deshiSHA1Rsa.sign(datasign, priKey, "UTF-8");

        disMap.Add("sign", sign);
        #endregion

        string submitUrl = "http://120.76.161.93/acq-gateway/api/backTxn.do";
        submitUrl = "https://backupay.dayspay.com.cn/acq-gateway/api/backTxn.do";

        string prams = datasign + "&sign=" + sign;

        string RES = HttpPost(submitUrl, prams);
        WriteTextForWarnString("SSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        WriteTextForWarnString(RES);
        JObject json = JObject.Parse(RES);
        if (json != null)
        {
            if (json["resp_code"].ToString().Replace("\"", "") == "00")
            {
                Response.Write("{\"respCode\":\"success\",\"respMsg\":\"成功\",\"refundNum\":\"" + order_no + "\",\"refundTime\":\"" + str_date + "\"}");
                Response.End();
            }
            else
            {
                Response.Write(System.Web.HttpUtility.UrlEncode("{\"respCode\":\"Fail\",\"respMsg\":\"" + json["resp_msg"].ToString().Replace("\"", "") + "\",\"refundNum\":\"" + order_no + "\",\"refundTime\":\"" + str_date + "\"}", Encoding.GetEncoding("GB2312")));
                Response.End();
            }
        }
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
