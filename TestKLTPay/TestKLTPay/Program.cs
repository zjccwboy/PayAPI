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
    class Program
    {
        static void Main(string[] args)
        {
            //快捷支付
            //TestQuickPay.StartTest();

            //单笔代付
            //SingleDFPayTest.StartTest();

            //单笔代付查询
            SingleDFQueryTest.StartTest();


            Console.Read();
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

        //平安银行贷记卡：6221558812340000
        //    手机号：13552535506
        //    cvn2：123
        //    有效期：1123（后台接口注意格式YYMM需倒一下）
        //    短信验证码：123456（手机）/111111（PC）（先点获取验证码之后再输入）
        //    姓名：互联网
        //    证件类型：01
        //    证件号：341126197709218366

        //测试环境第二次之后都需要我们技术手工查验证码。

    }
}
