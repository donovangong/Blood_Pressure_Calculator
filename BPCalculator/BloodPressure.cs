using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace BPCalculator
{
    public enum BPCategory
    {
        [Display(Name = "Low Blood Pressure")] Low,
        [Display(Name = "Ideal Blood Pressure")] Ideal,
        [Display(Name = "Pre-High Blood Pressure")] PreHigh,
        [Display(Name = "High Blood Pressure")] High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }

        public BPCategory Category
        {
            get
            {
                if (Systolic < 90 && Diastolic < 60)
                    return BPCategory.Low;
                else if (Systolic < 120 && Diastolic < 80)
                    return BPCategory.Ideal;
                else if (Systolic < 140 && Diastolic < 90)
                    return BPCategory.PreHigh;
                else
                    return BPCategory.High;
            }
        }
        public static Queue<BloodPressure> History { get; } = new();

        // Add to history safely
        public void AddToHistory()
        {
            if (this == null)
                return;

            if (History.Count == 10)
                History.Dequeue();

            // Never enqueue null ¡ª always new BP instance
            History.Enqueue(new BloodPressure
            {
                Systolic = this.Systolic,
                Diastolic = this.Diastolic
            });
        }

        public static int[] SysSeries =>
            History.Where(h => h != null).Select(h => h.Systolic).ToArray();

        public static int[] DiaSeries =>
            History.Where(h => h != null).Select(h => h.Diastolic).ToArray();
    }
}
