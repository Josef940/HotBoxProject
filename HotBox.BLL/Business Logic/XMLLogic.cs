using HotBox.BLL.Business_Entities.ViewModels;
using HotBox.BLL.Business_Entities.ViewModels.HotBox.BLL.Business_Entities.ViewModels;
using HotBox.DAL.HotboxXML;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotBox.BLL.Business_Logic
{
    public class XMLLogic
    {
        Facade facade = Facade.Instance;
        public Hotbox GetHotBoxData()
        {
            return null;
        }

        // Returns the Module List<> of a HotBox object
        public List<DAL.HotboxXML.Module> GetModules(Hotbox hbdata)
        {
            return hbdata==null ? null : hbdata.Site.Lan.Device.Modules;
        }

        public bool WriteToHotbox(string modulename, string value)
        {
            if (ValueIsADouble(value))
            {
                facade.GetDataXMLBridge().PostHotBoxValue(modulename,value);
            }
            return false;
        }

        public bool ValueIsADouble(string value)
        {
            double d;
            value = value.Replace(',', '.');
            return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
        }

        
        // Set parameter to 'true
        //public List<HotBoxValues> UpdateHotBoxValues(bool writeable)
        //{
        //    List<HotBoxValues> values = new List<HotBoxValues>();
        //    var hotboxData = writeable ? facade.GetDataBridge().GetWriteableHotBoxData() : facade.GetDataBridge().GetHotBoxData();
        //    values = GetHotBoxValues(hotboxData);
        //    return values;
        //}

        public bool UpdateHotBoxValues(ref List<HotBoxValues> hotboxvalues, ref List<HotBoxValues> writeablehotboxvalues)
        {
            if(hotboxvalues == null)
            {
                hotboxvalues = facade.GetBEConverter().HotboxConverter(facade.GetDataXMLBridge().GetHotBoxData());
                writeablehotboxvalues = facade.GetBEConverter().HotboxConverter(facade.GetDataXMLBridge().GetWriteableHotBoxData());
            }
            var newhotboxvalues = facade.GetBEConverter().HotboxConverter(facade.GetDataXMLBridge().GetHotBoxData());
            var newwriteablehotboxvalues = facade.GetBEConverter().HotboxConverter(facade.GetDataXMLBridge().GetWriteableHotBoxData());
            if (newhotboxvalues != null && newwriteablehotboxvalues != null)
            {
                hotboxvalues = SetNewValues(hotboxvalues, newhotboxvalues);
                writeablehotboxvalues = SetNewValues(writeablehotboxvalues, newwriteablehotboxvalues);
                return true;
            }
            else
                return false;
        }

        public List<HotBoxValues> SetNewValues(List<HotBoxValues> oldValues, List<HotBoxValues> newValues)
        {
            int newValuesCount = 0;
            foreach (var item in oldValues)
            {
                if (item.Module == newValues[newValuesCount].Module)
                {
                    newValues[newValuesCount].ValueDifference = Convert.ToDecimal(newValues[newValuesCount].Value) - Convert.ToDecimal(item.Value);
                    newValuesCount++;
                }
            }
            return newValues;
        }

        public Hotbox XMLSerializeToHotbox(HttpResponseMessage response)
        {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(Hotbox));
                var serializedhotbox = serializer.Deserialize(response.Content.ReadAsStreamAsync().Result);
                Hotbox hotbox = (Hotbox)serializedhotbox;
                return hotbox;
            }
            catch { return null; }
        }
        public SValue XMLSerializeSValue(HttpResponseMessage response)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SValue));
                var serializedsvalue = serializer.Deserialize(response.Content.ReadAsStreamAsync().Result);
                SValue svalue = (SValue)serializedsvalue;
                return svalue;
            }
            catch { return null; }
        }
    }
}
