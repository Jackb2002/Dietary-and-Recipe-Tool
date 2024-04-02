namespace WinFormsInfoApp.Family
{
    internal class ChildFemale : Person
    {
        internal override PersonAgeGroup AgeGroup { get; set; } = PersonAgeGroup.Child;

        internal override PersonGender Gender { get; set; } = PersonGender.Female;

        internal override float Max_Kcal { get; set; } = 1800; // Example value for maximum daily kcal for a child female

        internal override float Max_Fat { get; set; } = 65; // Example value for maximum daily fat intake for a child female

        internal override float Max_Saturates { get; set; } = 20; // Example value for maximum daily saturates intake for a child female

        internal override float Max_Carbs { get; set; } = 130; // Example value for maximum daily carbs intake for a child female

        internal override float Max_Sugars { get; set; } = 30; // Example value for maximum daily sugars intake for a child female

        internal override float Max_Fibre { get; set; } = 25; // Example value for maximum daily fibre intake for a child female

        internal override float Max_Protein { get; set; } = 34; // Example value for maximum daily protein intake for a child female

        internal override float Max_Salt { get; set; } = 3; // Example value for maximum daily salt intake for a child female
    }
}
