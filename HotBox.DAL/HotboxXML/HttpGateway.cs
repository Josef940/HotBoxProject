using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Serialization;

namespace HotBox.DAL.HotboxXML
{
    public class HttpGateway
    {
        private const string KEY = "FF3F390D";
        private const string IP = "10.176.131.250";
        // FOR DEVELOPMENT OUTSIDE SCHOOL --------------
        //private string HotBoxReadURI = "http://norrelundparken.se-bb.dk/ws/tsite.xml?Type=Read&Key=ABCDEF&Request=S100-200(V,%25,$)";
        //private string WriteableHotBoxReadURI = "http://norrelundparken.se-bb.dk/ws/tsite.xml?Type=Read&Request=K1-300(V,%25,$)";
        //private string HotBoxWriteURI(string modulename, string value)
        //{
        //    return String.Format("http://10.176.131.250/ws/tsite.xml?Type=Write&Key=FF3F390D&Request={0}(V={1})", modulename, value);
        //}
        // ---------------------------------------------

        // REAL HOTBOX:
        private string HotBoxReadURI = "http://10.176.131.250/ws/tsite.xml?Type=Read&Request=S1-300(V,%25,$)";
        private string WriteableHotBoxReadURI = "http://10.176.131.250/ws/tsite.xml?Type=Read&Request=K1-300(V,%25,$)";
        private string HotBoxWriteURI(string modulename, string value)
        {
            return String.Format("http://10.176.131.250/ws/tsite.xml?Type=Write&Key={0}&Request={1}(V={2})", KEY, modulename, value);
        }
        private string PtoS(string p)
        {
            return String.Format("http://10.176.131.250/ws/tsite.xml/Type=Read&Request={0}(Sc)",p);
        }
        // ----------------------------


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

        


        public HttpResponseMessage GetPtoS(string P)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response =
                        client.GetAsync(PtoS(P)).Result;
                    return response;
                }
                catch { return null; }
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
