
namespace ThirdParty_KLPPay.Models
{
    public abstract class BaseModel<TBaseContent, TBaseHead> : IModel where TBaseContent : class,new() where TBaseHead : BaseHead, new()
    {
        public abstract TBaseContent content { get; set; }
        public abstract TBaseHead head { get; set; }
    }

    public interface IModel
    {

    }

    public abstract class BaseHead
    {
        public virtual string sign { get; set; }
    }
}
