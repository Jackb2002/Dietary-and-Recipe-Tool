using System;

namespace WinFormsInfoApp.Family
{
    internal class ElderlyMale : Person
    {
        internal override PersonAgeGroup AgeGroup { get; set; } = PersonAgeGroup.Elderly;

        internal override PersonGender Gender { get; set; } = PersonGender.Male;

        internal override float Max_Kcal { get; set; } = 2200; // Example value for maximum daily kcal for an elderly male

        internal override float Max_Fat { get; set; } = 80; // Example value for maximum daily fat intake for an elderly male

        internal override float Max_Saturates { get; set; } = 25; // Example value for maximum daily saturates intake for an elderly male

        internal override float Max_Carbs { get; set; } = 250; // Example value for maximum daily carbs intake for an elderly male

        internal override float Max_Sugars { get; set; } = 80; // Example value for maximum daily sugars intake for an elderly male

        internal override float Max_Fibre { get; set; } = 30; // Example value for maximum daily fibre intake for an elderly male

        internal override float Max_Protein { get; set; } = 50; // Example value for maximum daily protein intake for an elderly male

        internal override float Max_Salt { get; set; } = 6; // Example value for maximum daily salt intake for an elderly male
    }
}
