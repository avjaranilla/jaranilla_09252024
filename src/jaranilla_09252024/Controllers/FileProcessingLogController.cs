using jaranilla_09252024.application.Implementation.Services.Interfaces;
using jaranilla_09252024.domain.Domain;
using Microsoft.AspNetCore.Mvc;

namespace jaranilla_09252024.Controllers
{
    [Route("api/fileprocessinglogs")]
    [ApiController]
    public class FileProcessingLogController : Controller
    {
        private readonly IFileProcessingLogRepositoryService _fileProcessingLogRepositoryService;

        public FileProcessingLogController(IFileProcessingLogRepositoryService fileProcessingLogRepositoryService)
        {
            _fileProcessingLogRepositoryService = fileProcessingLogRepositoryService;
        }

        [HttpGet]
        [Route("get")]
        [ApiKey]
        public async Task<ActionResult<List<FileProcessingLog>>> GetProccessedFileLogs()
        {
            var processedFileLogs = await _fileProcessingLogRepositoryService.GetAllLogsAsync();
            if (processedFileLogs.Count() <= 0)
            {
                return NoContent();
            }
            return Ok(processedFileLogs);
        }
    }
}
