using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CryptUtils_Csharp;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Xml;

namespace verifySign
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tbCerPath.Text = openFileDialog1.FileName;
                X509Certificate2 cer = CryptUtils.getPublicKeyXmlFromCer(openFileDialog1.FileName);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(cer.PublicKey.Key.ToXmlString(false));
                XmlNode node = xmldoc.SelectNodes("//Modulus")[0];
                this.tbPublicN.Text = CryptUtils.hex2asc(CryptUtils.Base64Decoder(node.FirstChild.Value));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tbMsg.Text))
            {
                MessageBox.Show("请输入报文");
                return;
            }

            MessageWorker worker = new MessageWorker();
            worker.CerFile = this.tbCerPath.Text;
            MessageWorker.trafficMessage msg = worker.UrlDecodeMessage(this.tbMsg.Text);
            msg = worker.CheckSignMessageAfterResponse(msg);
            lbcheck.Text = msg.sign;
            MessageBox.Show(msg.sign);
        }
    }
}
