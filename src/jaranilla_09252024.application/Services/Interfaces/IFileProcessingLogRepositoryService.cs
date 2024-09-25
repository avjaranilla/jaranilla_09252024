using jaranilla_09252024.application.Models;
using jaranilla_09252024.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Services.Interfaces
{
    public interface IFileProcessingLogRepositoryService
    {
        Task<FileProcessingLogViewModel> GetAllLogsAsync();

        Task<FileProcessingLog> AddLogAsync(string Name, TimeSpan processingTime);

    }
}
