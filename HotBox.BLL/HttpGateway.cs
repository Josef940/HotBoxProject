using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotBox.DAL;
using HotBox.BE;
namespace HotBox.BLL
{
    public class HttpGateway
    {
        Facade facade = Facade.Instance;
        TrendProject hotboxdata = null;
        public TrendProject GetHotBoxData() {
            hotboxdata = facade.GetDALHttpGateway().GetHotBoxData();
            return hotboxdata;
        }

        public List<Module> getModules(TrendProject hbdata){
            return hbdata.Site.Lan.Device.Modules;
        }
    }
}
