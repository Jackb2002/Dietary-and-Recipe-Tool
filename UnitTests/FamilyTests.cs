using WinFormsInfoApp.Family;

namespace UnitTests
{
    [TestClass]
    public class FamilyTests
    {
        [TestMethod]
        public void Empty_Family_Returns_Zero_Totals()
        {
            var family = new Family();

            Assert.AreEqual(0f, family.GetTotalKcal());
            Assert.AreEqual(0f, family.GetTotalProtein());
            Assert.AreEqual(0f, family.GetTotalFat());
            Assert.AreEqual(0f, family.GetTotalCarbs());
            Assert.AreEqual(0f, family.GetTotalSugar());
            Assert.AreEqual(0f, family.GetTotalFibre());
            Assert.AreEqual(0f, family.GetTotalSalt());
            Assert.AreEqual(0f, family.GetTotalSaturates());
        }

        [TestMethod]
        public void Empty_Family_Count_Is_Zero()
        {
            var family = new Family();
            Assert.AreEqual(0, family.Count);
        }

        [TestMethod]
        public void GetTotalKcal_Sums_All_Members()
        {
            var family = new Family();
            family.People.Add(new AdultMale());    // 2500 kcal
            family.People.Add(new AdultFemale());  // 2000 kcal

            Assert.AreEqual(4500f, family.GetTotalKcal());
        }

        [TestMethod]
        public void GetTotalProtein_Sums_All_Members()
        {
            var family = new Family();
            family.People.Add(new AdultMale());    // 55g
            family.People.Add(new AdultMale());    // 55g

            Assert.AreEqual(110f, family.GetTotalProtein());
        }

        [TestMethod]
        public void GetTotalFat_Sums_All_Members()
        {
            var family = new Family();
            family.People.Add(new AdultMale());   // 95g
            family.People.Add(new AdultFemale()); // depends on AdultFemale definition

            float expected = new AdultMale().Max_Fat + new AdultFemale().Max_Fat;
            Assert.AreEqual(expected, family.GetTotalFat());
        }

        [TestMethod]
        public void Family_Count_Reflects_Members()
        {
            var family = new Family();
            family.People.Add(new AdultMale());
            family.People.Add(new ChildFemale());
            family.People.Add(new ElderlyMale());

            Assert.AreEqual(3, family.Count);
        }

        [TestMethod]
        public void Person_ToString_Returns_Gender_And_AgeGroup()
        {
            Person p = new AdultMale();
            Assert.AreEqual("Male - Adult", p.ToString());
        }

        [TestMethod]
        public void AdultMale_Has_Expected_Nutritional_Limits()
        {
            var p = new AdultMale();
            Assert.AreEqual(2500f, p.Max_Kcal);
            Assert.AreEqual(95f, p.Max_Fat);
            Assert.AreEqual(30f, p.Max_Saturates);
            Assert.AreEqual(260f, p.Max_Carbs);
            Assert.AreEqual(55f, p.Max_Protein);
            Assert.AreEqual(6f, p.Max_Salt);
        }
    }
}
