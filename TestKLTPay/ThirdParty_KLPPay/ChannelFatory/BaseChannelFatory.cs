using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThirdParty_KLPPay.Models;
using ThirdParty_KLPPay.Utils;

namespace ThirdParty_KLPPay.ChannelFatory
{
    public abstract class BaseChannelFatory<TBaseContent, TBaseHead, TResutl>
        where TBaseContent : class, new() where TBaseHead : BaseHead, new() where TResutl : class,new()
    {
        public abstract string Url { get; }
        public string MD5Key { get; } = "742fa3ffd050fb441763bf8fb6c0594f";

        public TResutl CreateResult<TModel>(TModel model) where TModel : BaseModel<TBaseContent, TBaseHead>, new()
        {
            return RequestHandler(model);
        }

        public Task<TResutl> CreateResultAsync<TModel>(TModel model) where TModel : BaseModel<TBaseContent, TBaseHead>, new()
        {
            return RequestHandlerAsync(model);
        }

        private TResutl RequestHandler(IModel model)
        {
            var json = CreateRequest(model);

            if (string.IsNullOrEmpty(json))
                return default(TResutl);

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResutl>(json);
            return result;
        }

        private async Task<TResutl> RequestHandlerAsync(IModel model)
        {
            var json = await CreateRequestAsync(model);

            if (string.IsNullOrEmpty(json))
                return default(TResutl);

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResutl>(json);
            return result;
        }

        private string CreateRequest(IModel model)
        {
            var dic = new Dictionary<string, object>();

            var baseModel = model as BaseModel<TBaseContent, TBaseHead>;

            var props = baseModel.content.GetType().GetProperties();
            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(baseModel.content);
                if (value == null)
                    continue;

                dic[name] = value;
            }

            props = baseModel.head.GetType().GetProperties();
            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(baseModel.head);
                if (value == null)
                    continue;

                dic[name] = value;
            }

            var postData = SignUtil.SetSign<TBaseContent, TBaseHead>(baseModel, dic, MD5Key);

            Console.WriteLine($"请求报文:{postData}");

            var result = WebRequestUtil.PostJson(Url, postData);

            Console.WriteLine($"接收报文:{result}");

            return result;
        }

        private Task<string> CreateRequestAsync(IModel model)
        {
            var dic = new Dictionary<string, object>();

            var baseModel = model as BaseModel<TBaseContent, TBaseHead>;

            var props = baseModel.content.GetType().GetProperties();
            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(model);
                dic[name] = value;
            }

            props = baseModel.head.GetType().GetProperties();
            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(model);
                dic[name] = value;
            }

            var postData = SignUtil.SetSign<TBaseContent, TBaseHead>(model, dic, MD5Key);

            Console.WriteLine($"请求报文:{postData}");

            var result = WebRequestUtil.PostJsonAsync(Url, postData);

            Console.WriteLine($"接收报文:{result}");

            return result;
        }
    }
}
