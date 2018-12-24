using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{

    public abstract class ChannelFactory<TRequestModel, TOprions> : IChannelFactory where TRequestModel : IRequestModel, new() where TOprions : IOptions
    {
        public abstract string Url { get; }
        protected string Key { get; }
        public ChannelFactory(string key)
        {
            this.Key = key;
        }
        public abstract TRequestModel GenerateRequestModel(TOprions options);
        public string GenerateSign(TRequestModel requestModel)
        {
            if (requestModel == null)
                return null;

            requestModel.sign = null;
            var dic = new SortedDictionary<string, object>();
            var propes = typeof(TRequestModel).GetProperties();
            foreach (var prope in propes)
            {
                var name = prope.Name;
                var val = prope.GetValue(requestModel, null);
                if (val == null)
                    continue;

                dic[name] = val;
            }

            var builder = new StringBuilder();
            foreach (var kv in dic)
            {
                builder.AppendFormat($"{kv.Key}={kv.Value}&");
            }
            builder.AppendFormat($"key={this.Key}");

            var signStr = builder.ToString();
            var sign = GetMD5(signStr).ToLower();
            return sign;
        }

        public string GenerateRequestFormString(TRequestModel requestModel)
        {
            var dic = new SortedDictionary<string, object>();
            var propes = typeof(TRequestModel).GetProperties();
            foreach (var prope in propes)
            {
                var name = prope.Name;
                var val = prope.GetValue(requestModel, null);
                if (val == null)
                    continue;

                dic[name] = val;
            }

            var builder = new StringBuilder();
            var index = 0;
            foreach (var kv in dic)
            {
                index++;
                if(index < dic.Count)
                {
                    builder.AppendFormat($"{kv.Key}={kv.Value}&");
                }
                else
                {
                    builder.AppendFormat($"{kv.Key}={kv.Value}");
                }
            }
            var result = builder.ToString();
            //result = Encoding.GetEncoding("GB2312").GetString(Encoding.UTF8.GetBytes(result));
            return result;
        }

        private static string GetMD5(string myString)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);

            var builder = new StringBuilder();
            for (int i = 0; i < targetData.Length; i++)
            {
                builder.Append(targetData[i].ToString("x2"));
            }
            var result = builder.ToString();
            return result;
        }
    }

    public interface IChannelFactory
    {

    }
}
