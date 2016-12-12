using HotBox.BLL.Business_Entities.DBViewModels;
using HotBox.DAL.HotboxDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class BEConverter
    {
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
    }
}
