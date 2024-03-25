using System;

namespace WinFormsInfoApp.Family
{
    internal class TeenMale : Person
    {
        internal override PersonAgeGroup AgeGroup { get; set; } = PersonAgeGroup.Teenager;

        internal override PersonGender Gender { get; set; } = PersonGender.Male;

        internal override float Max_Kcal { get; set; } = 2500; // Example value for maximum daily kcal for a teenage male

        internal override float Max_Fat { get; set; } = 85; // Example value for maximum daily fat intake for a teenage male

        internal override float Max_Saturates { get; set; } = 27; // Example value for maximum daily saturates intake for a teenage male

        internal override float Max_Carbs { get; set; } = 260; // Example value for maximum daily carbs intake for a teenage male

        internal override float Max_Sugars { get; set; } = 50; // Example value for maximum daily sugars intake for a teenage male

        internal override float Max_Fibre { get; set; } = 30; // Example value for maximum daily fibre intake for a teenage male

        internal override float Max_Protein { get; set; } = 55; // Example value for maximum daily protein intake for a teenage male

        internal override float Max_Salt { get; set; } = 6; // Example value for maximum daily salt intake for a teenage male
    }
}
