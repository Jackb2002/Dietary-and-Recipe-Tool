using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsInfoApp.OpenFood
{
    internal class ProductMiscItem
    {
        public int additives_n { get; set; }
        public string checked_ { get; set; }
        public int complete { get; set; }
        public double completeness { get; set; }
        public string ecoscore_grade { get; set; }
        public int ecoscore_score { get; set; }
        public string food_groups { get; set; }
        public string[] food_groups_tags { get; set; }
        public class NutrientLevels
        {
            public string fat { get; set; } // enum (Allowed: low┃moderate┃high)
            public string salt { get; set; } // enum (Allowed: low┃moderate┃high)
            public string saturated_fat { get; set; } // enum (Allowed: low┃moderate┃high)
            public string sugars { get; set; } // enum (Allowed: low┃moderate┃high)
        }
        public NutrientLevels nutrient_levels { get; set; }
        public string packaging_text { get; set; }
        public class PackagingComponent
        {
            public int number_of_units { get; set; }
            public class Shape
            {
                public string id { get; set; }
                public string lc_name { get; set; }
            }
            public Shape shape { get; set; }
            public class Material
            {
                public string id { get; set; }
                public string lc_name { get; set; }
            }
            public Material material { get; set; }
            public class Recycling
            {
                public string id { get; set; }
                public string lc_name { get; set; }
            }
            public Recycling recycling { get; set; }
            public string quantity_per_unit { get; set; }
            public double quantity_per_unit_value { get; set; }
            public string quantity_per_unit_unit { get; set; }
            public double weight_specified { get; set; }
            public double weight_measured { get; set; }
            public double weight_estimated { get; set; }
            public double weight { get; set; }
            public string weight_source_id { get; set; }
        }
        public PackagingComponent[] packagings { get; set; }
        public int packagings_complete { get; set; } // Constraints: Min 0┃Max 1
        public string pnns_groups_1 { get; set; }
        public string[] pnns_groups_1_tags { get; set; }
        public string pnns_groups_2 { get; set; }
        public string[] pnns_groups_2_tags { get; set; }
        public int popularity_key { get; set; }
        public string[] popularity_tags { get; set; }
        public int scans_n { get; set; }
        public int unique_scans_n { get; set; }
        public string serving_quantity { get; set; }
        public string serving_quantity_unit { get; set; }
        public string serving_size { get; set; }
        public Dictionary<string, string> food_groups_ { get; set; }
        public Dictionary<string, string> packaging_text_ { get; set; }

    }
}
