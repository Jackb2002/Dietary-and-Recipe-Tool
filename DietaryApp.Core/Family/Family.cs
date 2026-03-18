namespace WinFormsInfoApp.Family
{
    public class Family
    {
        public int? Count => People.Count;
        public List<Person> People { get; set; } = [];

        public float GetTotalKcal()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Kcal;
            }
            return total;
        }

        public float GetTotalProtein()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Protein;
            }
            return total;
        }

        public float GetTotalFat()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Fat;
            }
            return total;
        }

        public float GetTotalCarbs()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Carbs;
            }
            return total;
        }

        public float GetTotalSugar()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Sugars;
            }
            return total;
        }

        public float GetTotalFibre()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Fibre;
            }
            return total;
        }

        public float GetTotalSalt()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Salt;
            }
            return total;
        }

        public float GetTotalSaturates()
        {
            float total = 0;
            foreach (Person person in People)
            {
                total += person.Max_Saturates;
            }
            return total;
        }
    }
}