using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace Merchant_Demo_QuickPay
{
    public partial class webtransf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            string url = string.Empty;
            string datastr = string.Empty;
            if (Request.HttpMethod == "GET")
            {
                url = Request.QueryString["urlstr"];
                datastr = Request.QueryString["msg"];

            }
            else if (Request.HttpMethod == "POST")
            {

                url = Request.QueryString["urlstr"];
                datastr = Request.QueryString["msg"];

            }

            if (string.IsNullOrEmpty(datastr) || string.IsNullOrEmpty(url))
            {
                return;
            }
            Dictionary<string, object> data = (Dictionary<string, object>)jsonSer.DeserializeObject(datastr);

            Dictionary<string, string> requestData = new Dictionary<string, string>();



            requestData["charset"] = data["charset"].ToString();
            requestData["signType"] = data["signType"].ToString();
            requestData["data"] = System.Web.HttpUtility.HtmlEncode(jsonSer.Serialize(data["data"]));
            requestData["sign"] = System.Web.HttpUtility.UrlEncode(data["sign"].ToString());
            requestData["extend"] = data["extend"].ToString();

            RedirectAndPOST(this.Page, url, requestData);
        }

        /// <summary>
        /// This method prepares an Html form which holds all data
        /// in hidden field in the addetion to form submitting script.
        /// </summary>
        /// <param name="url">The destination Url to which the post and redirection
        /// will occur, the Url can be in the same App or ouside the App.</param>
        /// <param name="data">A collection of data that
        /// will be posted to the destination Url.</param>
        /// <returns>Returns a string representation of the Posting form.</returns>
        /// <Author>Samer Abu Rabie</Author>

        private static String PreparePOSTForm(string url, Dictionary<string, string> data)
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (KeyValuePair<string, string> key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }

            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }

        /// <summary>
        /// POST data and Redirect to the specified url using the specified page.
        /// </summary>
        /// <param name="page">The page which will be the referrer page.</param>
        /// <param name="destinationUrl">The destination Url to which
        /// the post and redirection is occuring.</param>
        /// <param name="data">The data should be posted.</param>
        /// <Author>Samer Abu Rabie</Author>

        public static void RedirectAndPOST(Page page, string destinationUrl,
                                           Dictionary<string, string> data)
        {
            //Prepare the Posting form
            string strForm = PreparePOSTForm(destinationUrl, data);
            //Add a literal control the specified page holding 
            //the Post Form, this is to submit the Posting form with the request.
            page.Controls.Add(new LiteralControl(strForm));
        }
    }
}