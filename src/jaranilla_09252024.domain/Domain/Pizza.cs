using System.Reflection.Metadata.Ecma335;
using jaranilla_09252024.domain.Enum;

namespace jaranilla_09252024.domain.Domain
{
    public class Pizza
    {
        public int Id { get; set; } // Serves as Primary key.
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public string Size { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }



    }
}
