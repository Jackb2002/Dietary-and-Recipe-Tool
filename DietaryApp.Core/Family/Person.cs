namespace WinFormsInfoApp.Family
{
    public abstract class Person
    {
        public enum PersonAgeGroup
        {
            Child,
            Teenager,
            Adult,
            Elderly
        }
        public enum PersonGender
        {
            Male,
            Female
        }

        public abstract PersonAgeGroup AgeGroup { get; set; }
        public abstract PersonGender Gender { get; set; }
        public abstract float Max_Kcal { get; set; }
        public abstract float Max_Fat { get; set; }
        public abstract float Max_Saturates { get; set; }
        public abstract float Max_Carbs { get; set; }
        public abstract float Max_Sugars { get; set; }
        public abstract float Max_Fibre { get; set; }
        public abstract float Max_Protein { get; set; }
        public abstract float Max_Salt { get; set; }

        public override string? ToString()
        {
            return $"{Gender} - {AgeGroup}";
        }
    }
}
