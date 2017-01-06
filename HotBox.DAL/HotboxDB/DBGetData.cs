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
                try { 
                // Gets server DateTime, and reduces it by 'minutes' to get the desired start date
                var serverDate = GetServerDate(db);
                var startDate = serverDate.AddMinutes(-minutes);

                int indexNumber = Convert.ToInt32(db.tblStrategies.Where(x => x.Point == pointname).Select(x => x.theIndex).FirstOrDefault());
                var pointvalues = db.tblPointValues.Where(x => x.theIndex == indexNumber && (x.DataTime >= startDate)).OrderBy(x => x.DataTime).ToList();

                return pointvalues;
                }
                catch
                { return null; }
            }
        }

        public DateTime GetServerDate(HOTBOXDBEntities db)
        {
            var dateRaw = db.Database.SqlQuery<DateTime>("SELECT GETDATE()");
            var serverDate = dateRaw.AsEnumerable().First();
            return serverDate;
        }

        public List<tblStrategy> GetPValues()
        {
            using (var db = new HOTBOXDBEntities())
            {
                try
                {
                    var pvalues = db.tblStrategies.Where(x => x.Point.StartsWith("P")).ToList();
                    return pvalues;
                }
                catch
                { return null; }
            }
        }

    }
}
