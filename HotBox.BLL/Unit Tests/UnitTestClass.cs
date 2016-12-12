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
            Hotbox validhotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(validresponse);
            // 71 is the CncAddress attribute value in the XMLString.txt file
            Assert.AreEqual(71, validhotboxdata.Site.CncAddress);

            // Returns null if response cannot be serialized
            HttpResponseMessage invalidresponse = CreateResponseMessage();
            Hotbox invalidhotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(validresponse);
            Assert.AreEqual(null,invalidhotboxdata);

        }
        [Test]
        public void Retrieving_Values_From_HotBox_Test()
        {
            HttpResponseMessage response = CreateResponseMessage();
            Hotbox hotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(response);
            List<HotBoxValues> hbvalues = facade.GetDataLogic().GetHotBoxValues(hotboxdata);

            Assert.AreEqual((double)1963848,hbvalues[0].Value);
        }
        [Test]
        public void Get_Modules_From_HotBox_Test()
        {
            HttpResponseMessage response = CreateResponseMessage();
            Hotbox hotboxdata = facade.GetDataLogic().XMLSerializeToHotbox(response);
            List<Module> modules = facade.GetDataLogic().GetModules(hotboxdata);

            Assert.AreEqual("S100",modules[0].Name);
        }

        [Test]
        public void Value_Is_A_Viable_Double()
        {
            string number1 = "24124";
            string number2 = "20.45";
            string number3 = "23.45.4";
            string number4 = "321d451";
            string number5 = "344,02";
            string number6 = "3qde4qw3";

            Assert.AreEqual(true,facade.GetDataLogic().ValueIsADouble(number1));
            Assert.AreEqual(true, facade.GetDataLogic().ValueIsADouble(number2));
            Assert.AreEqual(false, facade.GetDataLogic().ValueIsADouble(number3));
            Assert.AreEqual(false, facade.GetDataLogic().ValueIsADouble(number4));
            Assert.AreEqual(true, facade.GetDataLogic().ValueIsADouble(number5));
            Assert.AreEqual(false, facade.GetDataLogic().ValueIsADouble(number6));
        }

        [Test]
        public void Hotbox_Gets_Updated_And_ValueDifference_Gets_Set()
        {
            List<HotBoxValues> oldboxvalues = new List<HotBoxValues>();
            List<HotBoxValues> newboxvalues = new List<HotBoxValues>();
            oldboxvalues.Add(new HotBoxValues { Module="SomeModule", Label="SomeLabel", Unit="SomeUnit", Value=5, valueDifference=null});
            newboxvalues.Add(new HotBoxValues { Module = "SomeModule", Label = "SomeLabel", Unit = "SomeUnit", Value = 7, valueDifference = null });
            var returnedlist = facade.GetDataLogic().SetNewValues(oldboxvalues,newboxvalues);
            Assert.AreEqual(2,returnedlist[0].valueDifference);
        }

    }
}
