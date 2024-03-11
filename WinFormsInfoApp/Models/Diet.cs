using System.Text.Json.Serialization;

namespace WinFormsInfoApp.Models
{
    [JsonSerializable(typeof(Diet))]
    public class Diet
    {
        public int DietId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }

        public Diet(int dietId, string name, string description, string priority)
        {
            DietId = dietId;
            Name = name;
            Description = description;
            Priority = priority;
        }
    }
}
