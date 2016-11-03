using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HotBox.BE;
using System.Xml.Serialization;

namespace HotBox.DAL
{
    public class HttpGateway
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TrendProject));
        private string HotBoxURI = "http://norrelundparken.se-bb.dk/ws/tsite.xml?Type=Read&Key=ABCDEF&Request=S100-200(V,%25,$)";
        /*public TrendProject GetHotBoxData() {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response =
                    client.GetAsync(HotBoxURI).Result;
                return response.Content.ReadAsAsync<TrendProject>().Result;
            }
        }*/

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

    }
}
