using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class Facade
    {
        /// <summary>
        /// Facade classes returns instances of certain classes if they already exist.
        /// If not, a new instance of that class is created.
        /// Singleton is implemented for this class.
        /// </summary>

        private static Facade instance;
        private Facade() { }

        public static Facade Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Facade();
                }
                return instance;
            }
        }


        private static DAL.HttpGateway DALHttpGateway;
        public DAL.HttpGateway GetDALHttpGateway()
        {
            if (DALHttpGateway == null)
                DALHttpGateway = new DAL.HttpGateway();

                return DALHttpGateway;
        }

        private static DataLogic datalogic;
        public DataLogic GetDataLogic()
        {
            if (datalogic == null)
                datalogic = new DataLogic();

            return datalogic;
        }

        private static DataBridge databridge;
        public DataBridge GetDataBridge()
        {
            if (databridge == null)
                databridge = new DataBridge();

            return databridge;
        }
    }
}
