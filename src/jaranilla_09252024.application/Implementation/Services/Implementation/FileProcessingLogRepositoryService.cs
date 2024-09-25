using jaranilla_09252024.application.Implementation.Services.Interfaces;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Implementation.Services.Implementation
{
    public class FileProcessingLogRepositoryService : IFileProcessingLogRepositoryService
    {
        private readonly IFileProcessingLogRepository _fileProcessingLogRepository;

        public FileProcessingLogRepositoryService(IFileProcessingLogRepository fileProcessingLogRepository)
        {
            _fileProcessingLogRepository = fileProcessingLogRepository;
        }

        public async Task<IEnumerable<FileProcessingLog>> GetAllLogsAsync()
        {
            return await _fileProcessingLogRepository.GetAllLogsAsync();
        }

        public async Task<FileProcessingLog> AddLogAsync(string Name, TimeSpan processingTime)
        {
            var log = new FileProcessingLog
            {
                Filename = Name,
                ProcessingTime = processingTime,
                ProcessedAt = DateTime.UtcNow
            };

            return await _fileProcessingLogRepository.AddLogAsync(log);
        }
    }
}
