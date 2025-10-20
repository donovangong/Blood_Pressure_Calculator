using BPCalculator;
using Microsoft.Testing.Platform.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BPFeatureTest.Steps
{
    [Binding]
    public sealed class BP_CalculationSteps
    {
        private BloodPressure bp;
        private BPCategory result;
        private bool invalid;

        [Given(@"systolic is (.*) and diastolic is (.*)")]
        public void GivenSystolicIsAndDiastolicIs(int systolic, int diastolic)
        {
            bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };

            if (systolic < BloodPressure.SystolicMin || systolic > BloodPressure.SystolicMax ||
                diastolic < BloodPressure.DiastolicMin || diastolic > BloodPressure.DiastolicMax)
            {
                invalid = true;
            }
            else
            {
                invalid = false;
            }

        }

        [When(@"calculate pressure category")]
        public void WhenCalculatePressureCategory()
        {
            result = bp.Category;
        }

        [Then(@"BP category is (.*)")]
        public void ThenBPCategoryIs(string expected)
        {
            if (invalid)
            {
                Assert.AreEqual("invalid", expected.ToLower());
            }
            else
            {
                Assert.AreEqual(expected, result.ToString());
            }
        }
    }
}
