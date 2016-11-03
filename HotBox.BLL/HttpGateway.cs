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
        public TrendProject GetHotBoxData() {
            return facade.GetDALHttpGateway().GetHotBoxData();
        }
    }
}
