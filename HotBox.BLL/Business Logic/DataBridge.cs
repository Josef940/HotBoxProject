using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotBox.DAL;
using HotBox.BLL.Business_Entities;
using HotBox.BLL.Business_Logic;
using System.Net.Http;

namespace HotBox.BLL.Business_Logic
{
    public class DataBridge
    {
        Facade facade = Facade.Instance;

        //Returns the data from a HotBox Http(XML) response
        public Hotbox GetHotBoxData()
        {
            var hotboxxml = facade.GetDALHttpGateway().GetHotBoxXML();
            Hotbox hotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(hotboxxml);
            return hotboxdata;
        }


        // METHOD TO USE WHILE DEVELOPING, DELETE ON DEPLOYMENT

        //public Hotbox GetHotBoxData()
        //{
        //    string XMLString = Properties.Resources.XMLString;
        //    HttpResponseMessage responsexml = new HttpResponseMessage();
        //    responsexml.Content = new StringContent(XMLString);
        //    Hotbox hotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(responsexml);
        //    return hotboxdata;
        //}
        //---------------------------------------------------------------------------------------

        public string PostHotBoxValue(string modulename, string value)
        {
            string response;

            return "";
        }

    }
}
