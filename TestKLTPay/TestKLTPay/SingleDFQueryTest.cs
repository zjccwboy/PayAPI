using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty_KLPPay.ChannelFatory;
using ThirdParty_KLPPay.Models;

namespace TestKLTPay
{
    /// <summary>
    /// 单笔代付查询测试类
    /// </summary>
    public class SingleDFQueryTest
    {
        public static void StartTest()
        {
            Console.WriteLine("");
            Console.WriteLine("开始测试单笔代付查询");
            Console.WriteLine("请输入代付单号:");
            var orderNo = Console.ReadLine();
            SingleDFQuery(orderNo);
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

        static void SingleDFQuery(string orderNo)
        {
            var model = new SingleDFQueryModelRequest();

            model.head = new SingleDFQueryHead
            {
                merchantId = "903110153110001",
                signType = "1",
                version = "18",
            };

            model.content = new SingleDFQueryContent
            {
                mchtOrderNo = orderNo,
                orderDate = DateTime.Now.ToString("yyyyMMdd"),
                paymentBusinessType = "SINGLE_PAY",
            };

            var fatory = new SingleDFQueryChannelFatory();
            var result = fatory.CreateResult(model);
        }
    }
}
