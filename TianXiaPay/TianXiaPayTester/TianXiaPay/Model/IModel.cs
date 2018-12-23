using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{

    public interface IRequestModel : IModel
    {
        string sign { get; set; }
    }

    public interface IResponseModel : IModel
    {

    }

    public interface IAsyncNotifyModel : IModel
    {

    }

    public interface IPageReturnModel : IModel
    {

    }

    public interface IModel
    {

    }
}
