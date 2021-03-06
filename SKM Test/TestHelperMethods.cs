﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKM.V3;
using System.Collections.Generic;

using SKM.V3.Models;
using SKM.V3.Internal;

namespace SKM_Test
{
    [TestClass]
    public class TestHelperMethods
    {
        [TestMethod]
        public void SendRequestToWebAPI3Test()
        {
            var keydata = new ExtendLicenseModel() { Key = "ITVBC-GXXNU-GSMTK-NIJBT" , NoOfDays=30, ProductId = 3349 };

            string tkn = "WyI0IiwiY0E3aHZCci9FWFZtOWJYNVJ5eTFQYk8rOXJSNFZ5TTh1R25YaDVFUiJd";

            var ans = HelperMethods.SendRequestToWebAPI3<BasicResult>(keydata, "/key/extendlicense/", tkn);
            Assert.AreEqual(ans.Result, ResultType.Success);
        }
    }
}
