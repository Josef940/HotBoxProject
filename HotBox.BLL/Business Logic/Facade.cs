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
                    instance = new Facade();
                return instance;
            }
        }

        private static XMLLogic xmllogic;
        public XMLLogic GetXMLLogic()
        {
            if (xmllogic == null)
                xmllogic = new XMLLogic();

            return xmllogic;
        }

        private static DataXMLBridge dataxmlbridge;
        public DataXMLBridge GetDataXMLBridge()
        {
            if (dataxmlbridge == null)
                dataxmlbridge = new DataXMLBridge();

            return dataxmlbridge;
        }

        private static DBLogic dblogic;
        public DBLogic GetDBLogic()
        {
            if (dblogic == null)
                dblogic = new DBLogic();

            return dblogic;
        }

        private static DatabaseBridge dbbridge;
        public DatabaseBridge GetDBBridge()
        {
            if (dbbridge == null)
                dbbridge = new DatabaseBridge();

            return dbbridge;
        }

        private static BEConverter BEconverter;
        public BEConverter GetBEConverter()
        {
            if (BEconverter == null)
                BEconverter = new BEConverter();

            return BEconverter;
        }

        private static ViewLogic viewLogic;
        public ViewLogic GetViewLogic()
        {
            if (viewLogic == null)
                viewLogic = new ViewLogic();

            return viewLogic;
        }
    }
}
