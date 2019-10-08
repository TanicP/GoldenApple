using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using GoldenAppleApp;
using System;
using System.Linq;


namespace GoldenAppleTest
{
    [TestClass]
    public class GoldenAppTest
    {
        [TestMethod]
        public void Validate_Y_or_N()
        {
            Helper validation = new Helper();
            // Act - Test if ValidateYorN works
            bool bY = Helper.ValidateYorN("Y");
            bool bN = Helper.ValidateYorN("N");
            bool by = Helper.ValidateYorN("y");
            bool bn = Helper.ValidateYorN("n");
            bool bAnyOtherChar = Helper.ValidateYorN("G");
            bool bNumToString = Helper.ValidateYorN(9.ToString());
            // Assert
            Assert.IsTrue(bY, "Validating Y");
            Assert.IsTrue(bN, "Validating N");
            Assert.IsTrue(by, "Validating y");
            Assert.IsTrue(bn, "Validating n");
            Assert.IsFalse(bAnyOtherChar, "Validating G");
            Assert.IsFalse(bNumToString, "Validating 9");
        }
        [TestMethod]
        public void Validate_T_or_F()
        {
            Helper validation = new Helper();
            // Act - Test if ValidateTorF works
            bool bT = Helper.ValidateTorF("T");
            bool bF = Helper.ValidateTorF("F");
            bool bt = Helper.ValidateTorF("t");
            bool bf = Helper.ValidateTorF("f");
            bool bAnyOtherChar = Helper.ValidateTorF("G");
            bool bNumToString = Helper.ValidateTorF(9.ToString());
            // Assert
            Assert.IsTrue(bT, "Validating T");
            Assert.IsTrue(bF, "Validating F");
            Assert.IsTrue(bt, "Validating t");
            Assert.IsTrue(bf, "Validating f");
            Assert.IsFalse(bAnyOtherChar, "Validating G");
            Assert.IsFalse(bNumToString, "Validating 9");
        }
        [TestMethod]
        public void Check_For_Existing_Properties()
        {
            // Arrange
            string title = "Prop1";
            string Ntitle = "XXX";
            // Act - Test for existing title in Database
            bool success = Functions.CheckForExistingProperties(title);
            bool successN = Functions.CheckForExistingProperties(Ntitle);
            // Assert
            Assert.IsTrue(success);
            Assert.IsFalse(successN);
        }
        [TestMethod]
        public void Check_For_Existing_Datas()
        {
            // Arrange
            string data = "Data21";
            string Ndata = "XXXÕ";
            // Act - Test for existing title in Database
            bool success = Functions.CheckForExistingDatas(data);
            bool successN = Functions.CheckForExistingDatas(Ndata);
            // Assert
            Assert.IsTrue(success);
            Assert.IsFalse(successN);
        }
        [TestMethod]
        public void Get_Data_By_Name()
        {
            // Arrange
            string data = "Data21";
            string Ndata = "XXX123";
            Data dataT;

            using (var context = new ApplicationContext())
            {
                dataT = context.Datas.First(x => x.Name == data);
            }   

                // Act - Test for existing title in Database
            Data success = Functions.GetDataByName(data);
            Data successN = Functions.GetDataByName(Ndata);

            // Assert
            Assert.AreEqual(dataT.Id,  success.Id);
            Assert.IsNull(successN);
        }
        [TestMethod]
        public void Get_Property()
        {
            // Arrange
            string title = "Prop1";
            string Ntitle = "XXX";
            Property prop;

            using (var context = new ApplicationContext())
            {
                prop = context.Propertys.First(x => x.Title == title);
            }

            // Act - Test for existing title in Database
            Property success = Program.GetProperty(title);
            Property successN = Program.GetProperty(Ntitle);

            // Assert
            Assert.AreEqual(prop.Id, success.Id);
            Assert.IsNull(successN);
        }
    }
}
