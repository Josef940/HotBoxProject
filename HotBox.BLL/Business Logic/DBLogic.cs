using HotBox.BLL.Business_Entities.DBViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class DBLogic
    {
        Facade facade = Facade.Instance;
        public List<PointValue> GetPointValues(string pointname, int minutes)
        {
            List<PointValue> values = new List<PointValue>();
            var dbpoints = facade.GetDBBridge().GetLatestPointValues(pointname,minutes);
            foreach (var item in dbpoints)
            {
                values.Add(facade.GetBEConverter().PointValueConverter(item));
            }

            return values;
        }
    }
}
