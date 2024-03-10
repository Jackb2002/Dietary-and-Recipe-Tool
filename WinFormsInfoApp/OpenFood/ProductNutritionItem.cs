using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsInfoApp.OpenFood
{
    internal class ProductNutritionItem
    {
        public string no_nutrition_data { get; set; }
        public string nutrition_data_per { get; set; } // enum (Allowed: serving┃100g)
        public string nutrition_data_prepared_per { get; set; } // enum (Allowed: serving┃100g)
        public class Nutriments
        {
            public double alcohol { get; set; }
            public double carbohydrates { get; set; }
            public double energy { get; set; }
            public double energy_value { get; set; }
            public string energy_unit { get; set; } // enum (Allowed: kcal┃kj)
            public double energy_kcal { get; set; }
            public double energy_kj { get; set; }
            public double fat { get; set; }
            public double fruits_vegetables_legumes_estimate_from_ingredients { get; set; }
            public double fruits_vegetables_nuts_estimate_from_ingredients { get; set; }
            public int nova_group { get; set; }
            public object nutrition_score_fr { get; set; } // {missing-type-info}
            public double proteins { get; set; }
            public double salt { get; set; }
            public double saturated_fat { get; set; }
            public double sodium { get; set; }
            public double sugars { get; set; }
            public int carbon_footprint_from_known_ingredients_product { get; set; }
            public double carbon_footprint_from_known_ingredients_serving { get; set; }
            public double erythritol { get; set; }
            public Dictionary<string, string> nutrient_unit { get; set; } // enum (Allowed: 公斤┃公升┃kg┃кг┃l┃л┃毫克┃mg┃мг┃mcg┃µg┃oz┃fl oz┃dl┃дл┃cl┃кл┃斤┃g┃∅┃●┃kj┃克┃公克┃г┃мл┃ml┃mmol/l┃毫升┃% vol┃ph┃%┃% dv┃% vol (alcohol)┃iu┃mol/l┃mval/l┃ppm┃�rh┃�fh┃�e┃�dh┃gpg
            public Dictionary<string, double> nutrient_100g { get; set; } // 🆁
            public Dictionary<string, double> nutrient_serving { get; set; } // 🆁
            public Dictionary<string, double> nutrient_value { get; set; } // 🆁
            public Dictionary<string, double> nutrient_prepared { get; set; }
            public Dictionary<string, string> nutrient_prepared_unit { get; set; }
            public Dictionary<string, double> nutrient_prepared_100g { get; set; } // 🆁
            public Dictionary<string, double> nutrient_prepared_serving { get; set; } // 🆁
            public Dictionary<string, double> nutrient_prepared_value { get; set; } // 🆁
        }
        public Nutriments nutriments { get; set; }
        public class NutriscoreData
        {
            public int energy { get; set; }
            public int energy_points { get; set; }
            public int energy_value { get; set; }
            public int fiber { get; set; }
            public int fiber_points { get; set; }
            public int fiber_value { get; set; }
            public int fruits_vegetables_nuts_colza_walnut_olive_oils { get; set; }
            public int fruits_vegetables_nuts_colza_walnut_olive_oils_points { get; set; }
            public int fruits_vegetables_nuts_colza_walnut_olive_oils_value { get; set; }
            public string grade { get; set; }
            public int is_beverage { get; set; }
            public int is_cheese { get; set; }
            public int is_fat { get; set; }
            public int is_water { get; set; }
            public int negative_points { get; set; }
            public int positive_points { get; set; }
            public double proteins { get; set; }
            public int proteins_points { get; set; }
            public double proteins_value { get; set; }
            public double saturated_fat { get; set; }
            public int saturated_fat_points { get; set; }
            public double saturated_fat_ratio { get; set; }
            public int saturated_fat_ratio_points { get; set; }
            public double saturated_fat_ratio_value { get; set; }
            public double saturated_fat_value { get; set; }
            public int score { get; set; }
            public double sodium { get; set; }
            public int sodium_points { get; set; }
            public double sodium_value { get; set; }
            public double sugars { get; set; }
            public int sugars_points { get; set; }
            public double sugars_value { get; set; }
        }
        public NutriscoreData nutriscore_data { get; set; }
        public string nutriscore_grade { get; set; } // enum (Allowed: a┃b┃c┃d┃e)
        public int nutriscore_score { get; set; }
        public int nutriscore_score_opposite { get; set; }
        public string nutrition_grade_fr { get; set; }
        public string nutrition_grades { get; set; }
        public string[] nutrition_grades_tags { get; set; }
        public int nutrition_score_beverage { get; set; }
        public int nutrition_score_warning_fruits_vegetables_nuts_estimate_from_ingredients { get; set; }
        public int nutrition_score_warning_fruits_vegetables_nuts_estimate_from_ingredients_value { get; set; }
        public int nutrition_score_warning_no_fiber { get; set; }
        public object[] other_nutritional_substances_tags { get; set; }
        public object[] unknown_nutrients_tags { get; set; }
        public object[] vitamins_tags { get; set; }

    }
}
