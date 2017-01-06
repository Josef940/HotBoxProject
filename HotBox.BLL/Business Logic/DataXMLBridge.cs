using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotBox.DAL;
using HotBox.BLL.Business_Logic;
using System.Net.Http;
using HotBox.DAL.HotboxXML;
using HotBox.DAL.HotboxDB;

namespace HotBox.BLL.Business_Logic
{
    public class DataXMLBridge
    {
        Facade facade = Facade.Instance;
        FacadeDAL facadedal = FacadeDAL.Instance;

        const string WRITE_OK_MESSAGE = "Acknowledge - Write was OK";

        //Returns the data from a HotBox Http(XML) response
        public Hotbox GetHotBoxData()
        {
            var hotboxxml = facadedal.GetDALHttpGateway().GetHotBoxXML();
            Hotbox hotboxdata = facade.GetXMLLogic().XMLSerializeToHotbox(hotboxxml);
            return hotboxdata;
        }

        public Hotbox GetWriteableHotBoxData()
        {
            var hotboxxml = facadedal.GetDALHttpGateway().GetWriteableHotBoxXML();
            Hotbox hotboxdata = facade.GetXMLLogic().XMLSerializeToHotbox(hotboxxml);
            return hotboxdata;
        }

        // Returns true if the Post request succeeds
        public bool PostHotBoxValue(string modulename, string value)
        {
            value = value.Replace(",",".");
            var hotboxxml = facadedal.GetDALHttpGateway().PostHotBoxValue(modulename,value);
            Hotbox hotboxdata = facade.GetXMLLogic().XMLSerializeToHotbox(hotboxxml);
            try{
                if (hotboxdata != null && hotboxdata.Site.Lan.Device.ServiceResponse.Type == WRITE_OK_MESSAGE)
                    return true;
                else
                    return false;
            }
            catch{
                return false;
            }
        }

        public Dictionary<string, string> PtoSDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var ppoints = facade.GetDBBridge().PPoints();
            if (ppoints == null)
                return null;
            foreach(var item in ppoints)
            {
                var svalue = facadedal.GetDALHttpGateway().GetPtoS(item.Point).Content.ToString();
                if (svalue == null)
                    return null;
                dict.Add(item.Point,svalue);
            }
            return dict;
        }

    }
}
