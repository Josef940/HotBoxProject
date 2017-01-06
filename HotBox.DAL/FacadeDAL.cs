using HotBox.DAL.HotboxDB;
using HotBox.DAL.HotboxXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.DAL
{
    public class FacadeDAL
    {
        private static FacadeDAL instance;
        private FacadeDAL() { }

        public static FacadeDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new FacadeDAL();
                return instance;
            }
        }

        private static HttpGateway DALHttpGateway;
        public HttpGateway GetDALHttpGateway()
        {
            if (DALHttpGateway == null)
                DALHttpGateway = new HttpGateway();

            return DALHttpGateway;
        }

        private static DBGetData DBRepository;
        public DBGetData GetDBRepository()
        {
            if (DBRepository == null)
                DBRepository = new DBGetData();

            return DBRepository;
        }
    }
}
