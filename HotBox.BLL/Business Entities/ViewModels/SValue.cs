using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Entities.ViewModels
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    namespace HotBox.BLL.Business_Entities.ViewModels
    {
        [XmlRoot(ElementName = "TrendProject")]
        public class SValue
        {
            public Site Site { get; set; }
        }
        public class Site
        {
            [XmlAttribute]
            public int CncAddress { get; set; }
            [XmlAttribute]
            public string TuaString { get; set; }
            public Lan Lan { get; set; }
        }

        public class Lan
        {
            bool IsLocalToBoolean;
            [XmlAttribute]
            public string IsLocalString { get; set; }
            [XmlAttribute]
            public int LanNumber { get; set; }
            public Device Device { get; set; }
        }

        public class Device
        {
            [XmlAttribute]
            public int DeviceNumber { get; set; }
            [XmlAttribute]
            public string VersionString { get; set; }
            public DeviceOverview DeviceOverview { get; set; }
            [XmlElement("Module")]
            public List<Module> Modules { get; set; }
            public ServiceResponse ServiceResponse { get; set; }
        }
        public class ServiceResponse
        {
            [XmlAttribute]
            public string Type { get; set; }
        }
        public class DeviceOverview
        {
            [XmlAttribute]
            public string DeviceType { get; set; }
            [XmlAttribute]
            public int MajorVersion { get; set; }
            [XmlAttribute]
            public int MinorVersion { get; set; }
            [XmlAttribute]
            public string HexEncoded { get; set; }
            [XmlAttribute]
            public int Revision { get; set; }
        }

        public class Module
        {
            [XmlAttribute]
            public string Name { get; set; }
            [XmlAttribute]
            public int SubTypeNumber { get; set; }
            [XmlElement("Param")]
            public List<Param> Params { get; set; }
        }

        public class Param
        {
            [XmlAttribute]
            public string Name { get; set; }
            [XmlAttribute]
            public int Type { get; set; }
            [XmlAttribute]
            public string Value { get; set; }
            [XmlElement]
            public string ConEndPoint { get; set; }
        }

    }
}
