﻿namespace WinFormsInfoApp.Family
{
    internal class ElderlyFemale : Person
    {
        internal override PersonAgeGroup AgeGroup { get; set; } = PersonAgeGroup.Elderly;

        internal override PersonGender Gender { get; set; } = PersonGender.Female;

        internal override float Max_Kcal { get; set; } = 2000; // Example value for maximum daily kcal for an elderly female

        internal override float Max_Fat { get; set; } = 70; // Example value for maximum daily fat intake for an elderly female

        internal override float Max_Saturates { get; set; } = 20; // Example value for maximum daily saturates intake for an elderly female

        internal override float Max_Carbs { get; set; } = 230; // Example value for maximum daily carbs intake for an elderly female

        internal override float Max_Sugars { get; set; } = 90; // Example value for maximum daily sugars intake for an elderly female

        internal override float Max_Fibre { get; set; } = 25; // Example value for maximum daily fibre intake for an elderly female

        internal override float Max_Protein { get; set; } = 45; // Example value for maximum daily protein intake for an elderly female

        internal override float Max_Salt { get; set; } = 6; // Example value for maximum daily salt intake for an elderly female
    }
}
