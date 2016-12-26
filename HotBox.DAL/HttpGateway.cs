using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Serialization;

namespace HotBox.DAL
{
    public class HttpGateway
    {
        private string Key = "FF3F390D";

        // FOR DEVELOPMENT OUTSIDE SCHOOL --------------
        private string HotBoxReadURI = "http://norrelundparken.se-bb.dk/ws/tsite.xml?Type=Read&Key=ABCDEF&Request=S100-200(V,%25,$)";
        private string WriteableHotBoxReadURI = "http://norrelundparken.se-bb.dk/ws/tsite.xml?Type=Read&Request=K1-300(V,%25,$)";
        private string HotBoxWriteURI(string modulename, string value)
        {
            return String.Format("http://10.176.131.250/ws/tsite.xml?Type=Write&Key=FF3F390D&Request={0}(V={1})", modulename, value);
        }
        // ---------------------------------------------
        // REAL:
        //private string HotBoxReadURI = "http://10.176.131.250/ws/tsite.xml?Type=Read&Request=S1-300(V,%25,$)";
        //private string WriteableHotBoxReadURI = "http://10.176.131.250/ws/tsite.xml?Type=Read&Request=K1-300(V,%25,$)";
        //private string HotBoxWriteURI(string modulename, string value)
        //{
        //    return String.Format("http://10.176.131.250/ws/tsite.xml?Type=Write&Key=FF3F390D&Request={0}(V={1})", modulename, value);
        //}
        // ----------------------------
        /*public TrendProject GetHotBoxData() {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response =
                    client.GetAsync(HotBoxURI).Result;
                return response.Content.ReadAsAsync<TrendProject>().Result;
            }
        }*/
        /*
        public TrendProject GetHotBoxData()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response =
                    client.GetAsync(HotBoxURI).Result;
                var serializedtrendproject = serializer.Deserialize(response.Content.ReadAsStreamAsync().Result);
                TrendProject trendproject = (TrendProject)serializedtrendproject;
                return trendproject;
            }
        }
        */
        public HttpResponseMessage GetHotBoxXML()
        {
            using (var client = new HttpClient())
            {
                try {
                    HttpResponseMessage response =
                        client.GetAsync(HotBoxReadURI).Result;
                    return response;
                }
                catch{ return null;}
            }
        }

        public HttpResponseMessage GetWriteableHotBoxXML()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response =
                        client.GetAsync(WriteableHotBoxReadURI).Result;
                    return response;
                }
                catch { return null; }
            }
        }

        public HttpResponseMessage PostHotBoxValue(string modulename, string value)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = 
                        client.GetAsync(HotBoxWriteURI(modulename,value)).Result;
                    return response;
                }
                catch { return null; }
            }
        }

    }
}
