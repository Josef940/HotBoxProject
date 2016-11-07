using HotBox.BLL.Business_Entities;
using System;
using System.Collections.Generic;
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
        public TrendProject GetHotBoxData()
        {
            return null;
        }

        // Returns the Module List<> of a HotBox object
        public List<Module> GetModules(TrendProject hbdata)
        {
            return hbdata.Site.Lan.Device.Modules;
        }

        // Values in the Params list are converted to a HotBoxValues List
        // for easier access and front-end implementation.
        public List<HotBoxValues> GetHotBoxValues(TrendProject hotbox)
        {
            List<HotBoxValues> values = new List<HotBoxValues>();
            foreach (Module item in GetModules(hotbox))
            {
                    values.Add(new HotBoxValues()
                    {
                        Label = item.Params[2].Value,
                        Value = Convert.ToDouble(item.Params[0].Value),
                        Unit = item.Params[1].Value
                    });
                
            }
            return values;
        }

        public List<HotBoxValues> AutoUpdateHotBoxValues(int secondsToSleep)
        {
            List<HotBoxValues> values = new List<HotBoxValues>();
                Thread.Sleep(secondsToSleep*1000);
                var hotboxData = facade.GetDataBridge().GetHotBoxData();
                values = GetHotBoxValues(hotboxData);
            return values;
        }

        public void SetValueDifference(ref List<HotBoxValues> oldValues, List<HotBoxValues> newValues)
        {
            var newValuesCount = 0;
            foreach (HotBoxValues item in oldValues)
            {
                item.valueDifference = newValues[newValuesCount].Value-item.Value;
                newValuesCount++;
            }
        }

        // Serializes an XML HttpResponse response to a HotBox object if successful
        public TrendProject XMLSerializeToHotbox(HttpResponseMessage response)
        {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(TrendProject));
                var serializedhotbox = serializer.Deserialize(response.Content.ReadAsStreamAsync().Result);
                TrendProject hotbox = (TrendProject)serializedhotbox;
                return hotbox;
            }
            catch { return null; }
        }
    }
}
