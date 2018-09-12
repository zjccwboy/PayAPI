using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty_KLPPay;
using ThirdParty_KLPPay.ChannelFatory;
using ThirdParty_KLPPay.Models;

namespace TestKLTPay
{
    /// <summary>
    /// 单笔代付接口测试类
    /// 1、目前Demo只完成支付部分功能测试，回调通知接口需要另行处理
    /// </summary>
    public class SingleDFPayTest
    {

        public static void StartTest()
        {
            Console.WriteLine("");
            Console.WriteLine("开始测试单笔代付");
            SingleDFPay();
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
        /// 单笔代付
        /// </summary>
        static void SingleDFPay()
        {
            var model = new SingleDFPayModelRequest();

            model.head = new SingleDFHead
            {
                merchantId = "903110153110001",
                signType = "1",
                version = "18",
            };

            model.content = new SingleDFContent
            {
                accountName = "全渠道",
                accountNo = "6216261000000000018",
                accountType = "1",
                amt = 1,
                bankName = "平安银行",
                bankNo = "105290068018",
                mchtOrderNo = GuidUtils.GetLongStringGuid(),
                notifyUrl = "http://47.92.68.54:8000",
                orderDateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                purpose = "test",
                remark = "单笔代付",
            };

            var fatory = new SingleDFPayChannelFatory();
            var result = fatory.CreateResult(model);
        }
    }
}
