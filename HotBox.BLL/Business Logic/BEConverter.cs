using HotBox.BLL.Business_Entities.DBViewModels;
using HotBox.BLL.Business_Entities.ViewModels;
using HotBox.DAL.HotboxDB;
using HotBox.DAL.HotboxXML;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class BEConverter
    {
        Facade facadebll = Facade.Instance;
        public PointValue PointValueConverter(tblPointValue dbpointvalue)
        {
            if (dbpointvalue == null)
                return null;

            var pointvalue = new PointValue
            {
                DataTime = dbpointvalue.DataTime,
                DataValue = dbpointvalue.DataValue,
                theIndex = dbpointvalue.theIndex
            };

            return pointvalue;
        }

        // Values in the Params list are converted to a HotBoxValues List
        // for easier access in front-end implementation.
        public List<HotBoxValues> HotboxConverter(Hotbox hotbox)
        {
            if (hotbox == null)
                return null;
            List<HotBoxValues> values = new List<HotBoxValues>();
            foreach (Module item in facadebll.GetXMLLogic().GetModules(hotbox))
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
    }
}
