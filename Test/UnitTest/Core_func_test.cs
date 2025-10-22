using BPCalculator;
using System.ComponentModel.DataAnnotations;

namespace UnitTest
{
    [TestClass]
    public sealed class Core_func_test
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bp = new BloodPressure { Systolic = 80, Diastolic = 50 };
            Assert.AreEqual(BPCategory.Low, bp.Category);

            bp = new BloodPressure { Systolic = 89, Diastolic = 59 };
            Assert.AreEqual(BPCategory.Low, bp.Category);

            bp = new BloodPressure { Systolic = 90, Diastolic = 60 };
            Assert.AreEqual(BPCategory.Ideal, bp.Category);

            bp = new BloodPressure { Systolic = 110, Diastolic = 75 };
            Assert.AreEqual(BPCategory.Ideal, bp.Category);

            bp = new BloodPressure { Systolic = 119, Diastolic = 79 };
            Assert.AreEqual(BPCategory.Ideal, bp.Category);

            bp = new BloodPressure { Systolic = 120, Diastolic = 80 };
            Assert.AreEqual(BPCategory.PreHigh, bp.Category);

            bp = new BloodPressure { Systolic = 130, Diastolic = 85 };
            Assert.AreEqual(BPCategory.PreHigh, bp.Category);

            bp = new BloodPressure { Systolic = 139, Diastolic = 89 };
            Assert.AreEqual(BPCategory.PreHigh, bp.Category);

            bp = new BloodPressure { Systolic = 140, Diastolic = 90 };
            Assert.AreEqual(BPCategory.High, bp.Category);

            bp = new BloodPressure { Systolic = 150, Diastolic = 95 };
            Assert.AreEqual(BPCategory.High, bp.Category);

            bp = new BloodPressure { Systolic = BloodPressure.SystolicMax, Diastolic = BloodPressure.DiastolicMax };
            Assert.AreEqual(BPCategory.High, bp.Category);

            bp = new BloodPressure { Systolic = 50, Diastolic = 30 };
            var context = new ValidationContext(bp);
            var results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(bp, context, results, true);
            Assert.IsFalse(valid);

            bp = new BloodPressure { Systolic = 200, Diastolic = 110 };
            context = new ValidationContext(bp);
            results = new List<ValidationResult>();
            valid = Validator.TryValidateObject(bp, context, results, true);
            Assert.IsFalse(valid);

        }
    }
}
