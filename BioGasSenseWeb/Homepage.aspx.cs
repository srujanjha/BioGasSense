using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BioGasSenseWeb
{
    public partial class Homepage : System.Web.UI.Page
    {
        Boolean Refresh = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            txt[0] = Label1;
            txt[1] = Label2;
            txt[2] = Label3;
            txt[3] = Label4;
            txt[4] = Label5;
            refresh();
        }
        Label[] txt = new Label[5];
        public void refresh()
        {

            string url = "https://biogas.azure-mobile.net/tables/biogassensor?$top=1&$orderby=__createdAt%20desc";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string str = readStream.ReadToEnd();
            for (int i = 0; i < 5; i++)
            {
                int ix = str.IndexOf("sensor" + (i + 1));
                int lx = str.IndexOf("\"", ix + 8);
                int rx = str.IndexOf("\"", lx + 2);
                string s = str.Substring(lx + 1, rx - lx - 1);
                txt[i].Text = s;
            }
            Refresh = true;
            btnRefresh.Enabled = true;
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

            if (Refresh)
            {
                Refresh = false;// btnRefresh.Enabled = false; 
                refresh();
            }
        }

    }
}