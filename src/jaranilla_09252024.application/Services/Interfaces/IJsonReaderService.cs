using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Services.Interfaces
{
    public interface IJsonReaderService
    {
        Task<bool> ProcessJsonAsync(Stream jsonStream, string fileName);
    }
}
