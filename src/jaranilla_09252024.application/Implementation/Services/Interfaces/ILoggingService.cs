using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Implementation.Services.Interfaces
{
    public interface ILoggingService
    {
        Task LogInformationAsync(string message);
        Task LogErrorAsync(string message, Exception exception);

    }
}
