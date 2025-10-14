using BPCalculator;

using Microsoft.CodeAnalysis;

namespace UnitTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bp = new BloodPressure { Systolic = 160, Diastolic = 80 };
            var cat = bp.Category;
            Assert.AreEqual(BPCategory.High, cat);

            bp = new BloodPressure { Systolic = 80, Diastolic = 50 };
            cat = bp.Category;
            Assert.AreEqual(BPCategory.Low, cat);

            bp = new BloodPressure { Systolic = 100, Diastolic = 70 };
            cat = bp.Category;
            Assert.AreEqual(BPCategory.Ideal, cat);

            bp = new BloodPressure { Systolic = 130, Diastolic =  85};
            cat = bp.Category;
            Assert.AreEqual(BPCategory.PreHigh, cat);

        }
    }
}
