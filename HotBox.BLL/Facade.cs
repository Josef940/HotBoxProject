using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL
{
    public class Facade
    {
        /// <summary>
        /// Facade classes returns instances of certain classes if they already exist.
        /// If not, a new instance of that class is created.
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

        private DAL.HttpGateway DALHttpGateway;
        public DAL.HttpGateway GetDALHttpGateway()
        {
            if (DALHttpGateway == null)
                DALHttpGateway = new DAL.HttpGateway();

                return DALHttpGateway;
        }
    }
}
