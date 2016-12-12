using HotBox.BLL.Business_Entities.DBViewModels;
using HotBox.DAL.HotboxDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class DatabaseBridge
    {
        Facade facade = Facade.Instance;

        public List<tblPointValue> GetLatestPointValues(string pointname, int minutes)
        {
            var dbpointvalues = facade.GetPointRepository().GetValuesFromTime(pointname, minutes);
            return dbpointvalues;
        }
    }
}
