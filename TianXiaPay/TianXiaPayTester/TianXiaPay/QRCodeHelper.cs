using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianXiaPay
{
    public class QRCodeHelper
    {
        public static string GetQRCodeBase64String(string strCode)
        {
            var result = string.Empty;
            try
            {
                QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrcode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 15, 6, false);

                using (MemoryStream ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] arr1 = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr1, 0, (int)ms.Length);
                    ms.Close();
                    result = Convert.ToBase64String(arr1);
                    result = string.Format("{0}{1}", "data:image/png;base64,", result);
                }
            }
            catch { }
            return result;
        }
    }
}
