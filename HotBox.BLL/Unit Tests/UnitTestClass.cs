﻿using HotBox.BLL.Business_Entities;
using HotBox.BLL.Business_Entities.DBViewModels;
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

        // NOTE:
        // If the variable used to increase the 'y' value is changed at some point,
        // this test will fail. Simply change the expected asserted values to (-y+VALUE)
        [Test]
        public void Converts_XY_To_Point_To_Be_Used_In_ValueChartWindow()
        {
            double x = 0;
            double y = 30;
            double divisor = 1;
            var point1 = facade.GetDBLogic().GetChartPoint(x,y,divisor);
            Assert.AreEqual(0,point1.X);
            Assert.AreEqual(370,point1.Y);
            x = 12.25;
            y = 25.552;
            var point2 = facade.GetDBLogic().GetChartPoint(x, y,divisor);
            Assert.AreEqual(12.25, point2.X);
            Assert.AreEqual(374.448, point2.Y);
        }

        [Test]
        public void PointValueList_To_PointCollection_Is_Successful()
        {
            double divisor = 1;
            var date1 = new DateTime(2014, 6, 17, 15, 20, 00);
            var date2 = new DateTime(2014, 6, 17, 15, 25, 00);
            var date3 = new DateTime(2014, 6, 17, 15, 35, 00);
            var pointvalue1 = new PointValue { DataTime = date1, DataValue=20.5, theIndex = 1};
            var pointvalue2 = new PointValue { DataTime = date2, DataValue = 22.5, theIndex = 1 };
            var pointvalue3 = new PointValue { DataTime = date3, DataValue = 19.22, theIndex = 1 };
            var pointvalues = new List<PointValue>();
            pointvalues.Add(pointvalue1);
            pointvalues.Add(pointvalue2);
            pointvalues.Add(pointvalue3);
            var pointcollection = facade.GetDBLogic().PointValuesToPointCollection(pointvalues);
            Assert.AreEqual(0,pointcollection.PointCollection[0].X);
            var y1 = facade.GetDBLogic().GetChartPoint(0, pointvalue1.DataValue, divisor);
            Assert.AreEqual(y1.Y, pointcollection.PointCollection[0].Y);

            Assert.AreEqual(5, pointcollection.PointCollection[1].X);
            var y2 = facade.GetDBLogic().GetChartPoint(0, pointvalue2.DataValue, divisor);
            Assert.AreEqual(y2.Y, pointcollection.PointCollection[1].Y);

            Assert.AreEqual(15, pointcollection.PointCollection[2].X);
            var y3 = facade.GetDBLogic().GetChartPoint(0, pointvalue3.DataValue,divisor);
            Assert.AreEqual(y3.Y, pointcollection.PointCollection[2].Y);
        }
    }
}
