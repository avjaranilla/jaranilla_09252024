using jaranilla_09252024.application.Models;
using jaranilla_09252024.application.Services.Interfaces;
using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Services.Implementation
{
    public class FileProcessingLogRepositoryService : IFileProcessingLogRepositoryService
    {
        private readonly IFileProcessingLogRepository _fileProcessingLogRepository;

        public FileProcessingLogRepositoryService(IFileProcessingLogRepository fileProcessingLogRepository)
        {
            _fileProcessingLogRepository = fileProcessingLogRepository;
        }

        public async Task<FileProcessingLogViewModel> GetAllLogsAsync()
        {
            var fileProcessingLogs = await _fileProcessingLogRepository.GetAllLogsAsync();
            FileProcessingLogViewModel fileProcessingLogsViewModel = new FileProcessingLogViewModel
            {
                ProcessedFileCount = fileProcessingLogs.Count(),
                FileProcessingLogs = fileProcessingLogs.ToList()
            };


            return fileProcessingLogsViewModel;
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
