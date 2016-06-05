﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKGL;
using SKM.V3;
using SKM.V3.Models;

namespace SKM_Test
{

    [TestClass]
    public class TestWebAPI3
    {
        [TestMethod]
        public void ExtendLicenseTest()
        {
            var keydata = new ExtendLicenseModel() { Key = "ITVBC-GXXNU-GSMTK-NIJBT", NoOfDays = 30, ProductId = 3349 };
            var auth = new AuthDetails() { Token = "WyI0IiwiY0E3aHZCci9FWFZtOWJYNVJ5eTFQYk8rOXJSNFZ5TTh1R25YaDVFUiJd" };

            var result = Key.ExtendLicense(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                
                // the license was successfully extended with 30 days.
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddFeatureTest()
        {
            var keydata = new FeatureModel() { Key = "LXWVI-HSJDU-CADTC-BAJGW", Feature = 2, ProductId = 3349 };
            var auth = new AuthDetails() { Token = "WyI2Iiwib3lFQjFGYk5pTHYrelhIK2pveWdReDdEMXd4ZDlQUFB3aGpCdTRxZiJd" };

            var result = Key.AddFeature(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                // feature 2 is set to true.
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveFeatureTest()
        {
            var keydata = new FeatureModel() { Key = "LXWVI-HSJDU-CADTC-BAJGW", Feature = 2, ProductId = 3349 };
            var auth = new AuthDetails() { Token = "WyI2Iiwib3lFQjFGYk5pTHYrelhIK2pveWdReDdEMXd4ZDlQUFB3aGpCdTRxZiJd" };

            var result = Key.RemoveFeature(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                // feature 2 is set to true.
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddDataObjectTest()
        {
            var keydata = new AddDataObjectModel() { };
            var auth = new AuthDetails() { Token = "WyIxMSIsInRFLzRQSzJkT2V0Y1pyN3Y3a1I2Rm9YdmczNUw0SzJTRHJwUERhRFMiXQ==" };

            var result = Data.AddDataObject(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                if (result.Id == 0)
                    Assert.Fail();

                var removeObj = Data.RemoveDataObject(auth, new RemoveDataObjectModel { Id = result.Id });

                if(removeObj == null || removeObj.Result == ResultType.Error)
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ListDataObjectsTest()
        {
            var keydata = new ListDataObjectsModel {  ShowAll = true };
            var auth = new AuthDetails() { Token = "WyIxMSIsInRFLzRQSzJkT2V0Y1pyN3Y3a1I2Rm9YdmczNUw0SzJTRHJwUERhRFMiXQ==" };

            var result = Data.ListDataObjects(auth, keydata);
            
            if (result != null && result.Result == ResultType.Success)
            {
                var firstObject =  (DataObjectWithReferencer)result.DataObjects[0];

                if (firstObject.ReferencerId == 0)
                    Assert.Fail();
            }
            else
            {
                Assert.Fail();
            }

            keydata.ShowAll = false;

            result = Data.ListDataObjects(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                if (result.DataObjects[0] is DataObjectWithReferencer)
                    Assert.Fail();
            }
            else
            {
                Assert.Fail();
            }


        }

        [TestMethod]
        public void SetIntValueTest()
        {
            var auth = new AuthDetails() { Token = "WyIxMSIsInRFLzRQSzJkT2V0Y1pyN3Y3a1I2Rm9YdmczNUw0SzJTRHJwUERhRFMiXQ==" };

            //first, let's obtain a random object. we record the old int value and the object id
            var objInt = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0];
            int oldInt = objInt.IntValue;
            long Id = objInt.Id;

            var keydata = new ChangeIntValueModel() {IntValue = 4711, Id = Id };
           
            var result = Data.SetIntValue(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                int objIntNew = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0].IntValue;
                Assert.IsTrue(objIntNew == 4711);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SetStringValueTest()
        {
            var auth = new AuthDetails() { Token = "WyIxMSIsInRFLzRQSzJkT2V0Y1pyN3Y3a1I2Rm9YdmczNUw0SzJTRHJwUERhRFMiXQ==" };

            //first, let's obtain a random object. we record the old string value and the object id
            var objInt = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0];
            string oldString = objInt.StringValue;
            long Id = objInt.Id;

            var keydata = new ChangeStringValueModel() { StringValue = "foo", Id = Id };

            var result = Data.SetStringValue(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                string objIntNew = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0].StringValue;
                Assert.AreEqual(objIntNew, "foo");
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void IncrementIntValueTest()
        {
            var auth = new AuthDetails() { Token = "WyIxMSIsInRFLzRQSzJkT2V0Y1pyN3Y3a1I2Rm9YdmczNUw0SzJTRHJwUERhRFMiXQ==" };

            //first, let's obtain a random object. we record the old int value and the object id
            var objInt = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0];
            int oldInt = objInt.IntValue;
            long Id = objInt.Id;

            var keydata = new ChangeIntValueModel() { IntValue = 10, Id = Id };

            var result = Data.IncrementIntValue(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                int objIntNew = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0].IntValue;
                Assert.IsTrue(objIntNew == oldInt+10);
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void DecrementIntValueTest()
        {
            var auth = new AuthDetails() { Token = "WyIxMSIsInRFLzRQSzJkT2V0Y1pyN3Y3a1I2Rm9YdmczNUw0SzJTRHJwUERhRFMiXQ==" };

            //first, let's obtain a random object. we record the old int value and the object id
            var objInt = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0];
            int oldInt = objInt.IntValue;
            long Id = objInt.Id;

            var keydata = new ChangeIntValueModel() { IntValue = 10, Id = Id };

            var result = Data.DecrementIntValue(auth, keydata);

            if (result != null && result.Result == ResultType.Success)
            {
                int objIntNew = Data.ListDataObjects(auth, new ListDataObjectsModel { ShowAll = true }).DataObjects[0].IntValue;
                Assert.IsTrue(objIntNew == oldInt - 10);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void KeyLockTest()
        {
            // make sure the access token below has key lock set to "-1".
            // the token below has key lock set to "-1" and access to "AddFeature" method.
            // note, we cannot use this token for anything but the Key Lock method, in order
            // to get a new token.
            var auth = new AuthDetails() { Token = "WyI0NCIsInRhOGNJZm1BS0xkbGJjUW55UkdEN3lzTzhWckd6SzRzYlgvRkFOQmQiXQ==" };

            // 1. Get a new token
            var key = "ITVBC-GXXNU-GSMTK-NIJBT";
            var result = Key.KeyLock(auth, new KeyLockModel { Key = key, ProductId = 3349 });

            var newAuth = result.GetAuthDetails();

            // 2. Access the method
            var addFeature = Key.AddFeature(newAuth, new FeatureModel { Feature = 2, ProductId = 3349, Key = key });

            // this should work
            if (addFeature.Result == ResultType.Error)
                Assert.Fail();

            var wrongKey = "MTMPW-VZERP-JZVNZ-SCPZM";  
            var addFeatureWrongKey = Key.AddFeature(newAuth, new FeatureModel { Feature = 2, ProductId = 3, Key = wrongKey });

            // this should not work
            if (addFeatureWrongKey != null && addFeatureWrongKey.Result == ResultType.Success)
                Assert.Fail();

        }

        [TestMethod]
        public void MyTestMethod()
        {
            var keyInfoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyInfoResult>(TestCases.TestData.signedData);

            var license = keyInfoResult.LicenseKey;

            Assert.IsTrue(SKM.V3.Key.IsLicenceseKeyGenuine(license, TestCases.TestData.pubkey));
            
        }

    }
}
