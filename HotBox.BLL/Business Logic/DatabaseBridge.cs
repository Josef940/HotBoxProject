using HotBox.BLL.Business_Entities.DBViewModels;
using HotBox.DAL;
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
        FacadeDAL facadedal = FacadeDAL.Instance;

        public List<tblPointValue> GetLatestPointValues(string pointname, int minutes)
        {
            var dbpointvalues = facadedal.GetDBRepository().GetValuesFromTime(pointname, minutes);
            return dbpointvalues;
        }

        public List<tblStrategy> PPoints()
        {
            var pPoints = facadedal.GetDBRepository().GetPValues();
            return pPoints;
        }
    }

}
