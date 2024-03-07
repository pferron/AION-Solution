using AION.Manager.AccelaBusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.AccelaBusinessObjects
{
    [TestClass]
    public class AccelaProjectOccupancyTypeBOTests
    {
        [DataTestMethod]
        [DataRow("F2     * FACTORY/INDUSTRIAL - LOW HAZARD", "FACTORY INDUSTRIAL")]
        [DataRow("A1     * ASSEMBLY - THEATER", "ASSEMBLY")]
        [DataRow("A1S     * ASSEMBLY - THEATER w/ STAGE", "ASSEMBLY")]
        [DataRow("A2N     * ASSEMBLY - NIGHTCLUBS", "ASSEMBLY")]
        [DataRow("E      * EDUCATIONAL", "EDUCATIONAL")]
        [DataRow("H2     * HIGH HAZARD - DEFLAGRATION", "HAZARDOUS")]
        [DataRow("I1     * INSTITUTIONAL - SUPERVISED ENVIRONMENT, CONDITION 2 (NON-AMBULATORY)", "INSTITUTIONAL")]
        [DataRow("R3     * RESIDENTIAL - SINGLE FAMILY", "RESIDENTIAL")]
        [DataRow("U      * UTILITY", "UTILITYMISCELLANEOUS")]
        public void TestOccupancyTypeNameGetsReturnedCorrectly(string accelaOccupancyType, string expectedTypeName)
        {
            string actualTypeName = new AccelaProjectOccupancyTypeBO().GetProjectOccupancyTypeNameFromAccelaOccupancyType(accelaOccupancyType);

            Assert.AreEqual(expectedTypeName, actualTypeName);
        }
    }
}
