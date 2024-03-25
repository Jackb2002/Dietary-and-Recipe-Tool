using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsInfoApp.Family
{
    internal abstract class Person
    {
        internal enum PersonAgeGroup
        {
            Child,
            Teenager,
            Adult,
            Elderly
        }
        internal enum PersonGender
        {
            Male,
            Female
        }

        internal abstract PersonAgeGroup AgeGroup { get; set; }
        internal abstract PersonGender Gender { get; set; }
        internal abstract float Max_Kcal { get; set; }
        internal abstract float Max_Fat { get; set; }
        internal abstract float Max_Saturates { get; set; }
        internal abstract float Max_Carbs { get; set; }
        internal abstract float Max_Sugars { get; set; }
        internal abstract float Max_Fibre { get; set; }
        internal abstract float Max_Protein { get; set; }
        internal abstract float Max_Salt { get; set; }
    }
}
