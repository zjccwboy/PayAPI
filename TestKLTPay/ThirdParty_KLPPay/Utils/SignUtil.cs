using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirdParty_KLPPay.Models;
using System;

namespace ThirdParty_KLPPay
{
    public class SignUtil
    {
        private static Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.None,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        };

        public static string SetSign<TBaseContent, TBaseHead>(IModel model, Dictionary<string, object> mapDic, string md5Key)
                    where TBaseContent : class, new() where TBaseHead : BaseHead, new()
        {
            var builder = new StringBuilder();
            var keys = mapDic.Keys.OrderBy(a => a).ToList();
            foreach (var key in keys)
            {
                var append = $"{key}={mapDic[key]}&";
                builder.Append(append);
            }

            var signOrigin = builder.ToString().TrimEnd('&');
            var signString = MD5Utils.GetSign(signOrigin, md5Key);

            (model as BaseModel<TBaseContent, TBaseHead>).head.sign = signString;

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(model, jsonSettings);
            return result;
        }
    }
}
