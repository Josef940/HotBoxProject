using HotBox.BLL.Business_Entities;
using HotBox.BLL.Business_Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Unit_Tests
{
    [TestFixture]
    public class UnitTestClass
    {
        string XMLString = Properties.Resources.XMLString;
        Facade facade = Facade.Instance;
       
        [Test]
        public void Serializes_XML_To_HotBox_Test()
        {
            HttpResponseMessage validresponse = new HttpResponseMessage();
            validresponse.Content = new StringContent(XMLString);
            TrendProject validhotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(validresponse);
            // 71 is the CncAddress attribute value in the XMLString.txt file
            Assert.AreEqual(71, validhotboxdata.Site.CncAddress);

            // Returns null if response cannot be serialized
            HttpResponseMessage invalidresponse = new HttpResponseMessage();
            invalidresponse.Content = new StringContent("You cannot serialize this");
            TrendProject invalidhotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(validresponse);
            Assert.AreEqual(null,invalidhotboxdata);

        }
    }
}
