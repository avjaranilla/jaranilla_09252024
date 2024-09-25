namespace jaranilla_09252024.Models
{
    public class PizzaRequestModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
