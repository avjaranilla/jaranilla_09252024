using jaranilla_09252024.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.domain.Repositories
{
    public interface IFileProcessingLogRepository
    {
        Task<FileProcessingLog> AddLogAsync(FileProcessingLog log);
        Task<IEnumerable<FileProcessingLog>> GetAllLogsAsync();
    }
}
