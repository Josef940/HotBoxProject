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
       
        private HttpResponseMessage CreateResponseMessage()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(XMLString);
            return response;
        }

        [Test]
        public void Serializes_XML_To_HotBox_Test()
        {
            HttpResponseMessage validresponse = CreateResponseMessage();
            TrendProject validhotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(validresponse);
            // 71 is the CncAddress attribute value in the XMLString.txt file
            Assert.AreEqual(71, validhotboxdata.Site.CncAddress);

            // Returns null if response cannot be serialized
            HttpResponseMessage invalidresponse = CreateResponseMessage();
            TrendProject invalidhotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(validresponse);
            Assert.AreEqual(null,invalidhotboxdata);

        }
        [Test]
        public void Retrieving_Values_From_HotBox_Test()
        {
            HttpResponseMessage response = CreateResponseMessage();
            TrendProject hotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(response);
            List<HotBoxValues> hbvalues = facade.GetDataLogic().GetHotBoxValues(hotboxdata);

            Assert.AreEqual(19638480,hbvalues[0].Value);
        }
        [Test]
        public void Get_Modules_From_HotBox_Test()
        {
            HttpResponseMessage response = CreateResponseMessage();
            TrendProject hotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(response);
            List<Module> modules = facade.GetDataLogic().GetModules(hotboxdata);

            Assert.AreEqual("S100",modules[0].Name);
        }
    }
}
