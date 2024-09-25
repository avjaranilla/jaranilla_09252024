using jaranilla_09252024.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Models
{
    public class AddPizzasReturnModel
    {
        public FileProcessingLog FileProcessingLog { get; set; }
        public List<Pizza> Pizzas { get; set; } 
    }
}
