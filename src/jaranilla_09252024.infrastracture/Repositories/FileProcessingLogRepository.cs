using jaranilla_09252024.domain.Domain;
using jaranilla_09252024.domain.Repositories;
using jaranilla_09252024.infrastracture.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.infrastracture.Repositories
{
    public class FileProcessingLogRepository : IFileProcessingLogRepository
    {
        private readonly AppDbContext _context;

        public FileProcessingLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FileProcessingLog> AddLogAsync(FileProcessingLog log)
        {
            _context.FileProcessingLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<IEnumerable<FileProcessingLog>> GetAllLogsAsync()
        {
            return await _context.FileProcessingLogs.ToListAsync();
        }
    }
}
