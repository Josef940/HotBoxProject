using HotBox.BLL.Business_Entities;
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
    public class DataLogic
    {
        Facade facade = Facade.Instance;
        public Hotbox GetHotBoxData()
        {
            return null;
        }

        // Returns the Module List<> of a HotBox object
        public List<Module> GetModules(Hotbox hbdata)
        {
            return hbdata==null ? null : hbdata.Site.Lan.Device.Modules;
        }

        public bool WriteToHotbox(string modulename, string value)
        {
            if (ValueIsADouble(value))
            {
                facade.GetDataBridge().PostHotBoxValue(modulename,value);
            }
            return false;
        }

        public bool ValueIsADouble(string value)
        {
            double d;
            value = value.Replace(',', '.');
            return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
        }

        // Values in the Params list are converted to a HotBoxValues List
        // for easier access and front-end implementation.
        public List<HotBoxValues> GetHotBoxValues(Hotbox hotbox)
        {
            if (hotbox == null)
                return null;
            List<HotBoxValues> values = new List<HotBoxValues>();
            foreach (Module item in GetModules(hotbox))
            {
                values.Add(new HotBoxValues()
                {
                        Module = item.Name,
                        Label = item.Params[2].Value,
                        // This parsing method was necessary to maintain the decimal points from the Value string
                        Value = double.Parse(item.Params[0].Value, CultureInfo.InvariantCulture),
                        Unit = item.Params[1].Value
                    });
                
            }
            return values;
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
                hotboxvalues = GetHotBoxValues(facade.GetDataBridge().GetHotBoxData());
                writeablehotboxvalues = GetHotBoxValues(facade.GetDataBridge().GetWriteableHotBoxData());
            }
            var newhotboxvalues = GetHotBoxValues(facade.GetDataBridge().GetHotBoxData());
            var newwriteablehotboxvalues = GetHotBoxValues(facade.GetDataBridge().GetWriteableHotBoxData());
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
                    newValues[newValuesCount].valueDifference = newValues[newValuesCount].Value - item.Value;
                    newValuesCount++;
                }
            }
            return newValues;
        }

        // Serializes an XML HttpResponse response to a HotBox object if successful
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
    }
}
