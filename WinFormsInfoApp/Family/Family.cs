namespace WinFormsInfoApp.Family
{
    internal class Family
    {
        public int? Count => People.Count;
        public List<Person> People { get; set; }  = new List<Person>();

        internal float GetTotalKcal()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Kcal;
            }
            return total;
        }

        internal float GetTotalProtein()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Protein;
            }
            return total;
        }

        internal float GetTotalFat()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Fat;
            }
            return total;
        }

        internal float GetTotalCarbs()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Carbs;
            }
            return total;
        }

        internal float GetTotalSugar()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Sugars;
            }
            return total;
        }

        internal float GetTotalFiber()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Fibre;
            }
            return total;
        }

        internal float GetTotalSalt()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Salt;
            }
            return total;
        }

        internal float GetTotalSaturates()
        {
            float total = 0;
            foreach (var person in People)
            {
                total += person.Max_Saturates;
            }
            return total;
        }
    }
}