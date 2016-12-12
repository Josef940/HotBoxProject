using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.DAL.HotboxDB
{
    public class DBGetData
    {
        // Return a list of data-values from one module from 'minutes' ago to current time
        public List<tblPointValue> GetValuesFromTime(string pointname,int minutes)
        {
            using (var db = new HOTBOXDBEntities())
            {
                // Gets server DateTime, and reduces it by 'minutes' to get the desired start date
                var dateQuery = db.Database.SqlQuery<DateTime>("SELECT getdate()");
                var serverDate = dateQuery.AsEnumerable().First();
                var startDate = serverDate.AddMinutes(-minutes);

                int indexNumber = Convert.ToInt32(db.tblStrategies.Where(x => x.Point == pointname).Select(x => x.theIndex).FirstOrDefault());
                var pointvalues = db.tblPointValues.Where(x => x.DataTime >= startDate).ToList();

                return pointvalues;
            }
        }

    }
}
