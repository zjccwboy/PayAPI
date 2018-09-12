
namespace ThirdParty_KLPPay.Models
{

    public class SingleElectronicCertificateModelRequest : BaseModel<SingleElectronicCertificateContent, SingleElectronicCertificateHead>
    {
        public override SingleElectronicCertificateHead head { get; set; }
        public override SingleElectronicCertificateContent content { get; set; }
    }

    public class SingleElectronicCertificateHead : BaseHead
    {
        public string merchantId { get; set; }
        public string sign { get; set; }
        public string signType { get; set; }
    }

    public class SingleElectronicCertificateContent
    {
        public string orderDate { get; set; }
        public string mchtOrderNo { get; set; }
    }

    public class SingleElectronicCertificateModelResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public string mchtId { get; set; }
        public string signMsg { get; set; }
        public string signType { get; set; }
        public string requestId { get; set; }
        public string receipt { get; set; }
    }

}
