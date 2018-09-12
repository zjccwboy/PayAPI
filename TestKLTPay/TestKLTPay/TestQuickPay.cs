using System;
using ThirdParty_KLPPay;
using ThirdParty_KLPPay.ChannelFatory;
using ThirdParty_KLPPay.Models;

namespace TestKLTPay
{
    /// <summary>
    /// 快捷支付测试类，快捷支付流程：
    /// 1、先调用SMS短信接口发送短信，收到短信以后再调用快捷确认支付接口完成支付
    /// 2、目前Demo只完成支付部分逻辑，支付成功回调处理需要另行解决。
    /// </summary>
    public class TestQuickPay
    {
        /// <summary>
        /// 快捷支付测试接口
        /// </summary>
        public static void StartTest()
        {
            Console.WriteLine("开始测试快捷支付");

            //先发送短信
            var smsResult = SendSMS();

            Console.WriteLine("请输入短信码(111111):");
            var smsCode = Console.ReadLine();

            //使用短信号支付
            QuickPay(smsResult, smsCode);
        }

        //  测试md5Key
        //  public static final String md5Key = "742fa3ffd050fb441763bf8fb6c0594f";

        //  测试商户号
        //  public static final String merchantId = "903110153110001";

        //测试环境快捷的测试卡号：
        //测试环境第一次：
        //平安银行借记卡：6216261000000000018
        //    手机号：13552535506
        //    证件类型：01
        //    证件号：341126197709218366
        //    密码：123456
        //    姓名：全渠道
        //    短信验证码：123456（手机）/111111（PC）（先点获取验证码之后再输入）

        /// <summary>
        /// 发送短信接口
        /// </summary>
        static SendSMSModelResponse SendSMS()
        {
            var model = new SendSMSModelRequest();
            model.head = new SendSMSHead
            {
                merchantId = "903110153110001",
                signType = "1",
            };

            model.content = new SendSMSContent
            {
                payerAcctNo = "6216261000000000018",
                payerIdNo = "341126197709218366",
                payerIdType = "01",
                payerName = "全渠道",
                payerTelephone = "13552535506",
                orderAmount = "100",
                orderNo = GuidUtils.GetLongStringGuid(),
            };

            var fatory = new SendSMSChannelFatory();
            var result = fatory.CreateResult(model);

            return result;
        }

        /// <summary>
        /// 快捷确认支付接口
        /// </summary>
        /// <param name="smsResponse"></param>
        /// <param name="smsCode"></param>
        static void QuickPay(SendSMSModelResponse smsResponse, string smsCode)
        {
            var model = new QuickPayModelRequest();

            model.head = new QuickPayHead
            {
                version = "18",
                merchantId = "903110153110001",
                signType = "1",
            };

            model.content = new QuickPayContent
            {
                orderNo = smsResponse.orderNo, //发送短信接口的原订单号
                productId = "P1005001",
                orderCurrency = 156,
                productNum = 1,
                smsCode = smsCode,
                orderExpireDatetime = 9999,
                productName = "test",
                productDesc = "test",
                orderDatetime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                orderAmount = 100,
                acctValiddate = "2311",//yyMM
                originalRequestId = smsResponse.requestId, //发送短信接口返回的 requestId
                productPrice = 100,
                receiveUrl = "http://47.92.68.54:8000", //交易结果通知，需要参考文档 4 确认支付 自行处理.
                ext1 = "test",
                ext2 = "test",
            };

            var fatory = new QuickPayChannelFatory();
            var result = fatory.CreateResult(model);
        }
    }
}
