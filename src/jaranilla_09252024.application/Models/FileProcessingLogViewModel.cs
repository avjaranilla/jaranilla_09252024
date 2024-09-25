using jaranilla_09252024.domain.Domain;

namespace jaranilla_09252024.application.Models 
{ 
    public class FileProcessingLogViewModel
    {
        public int ProcessedFileCount { get; set; }
        public List<FileProcessingLog> FileProcessingLogs {get ;set;}
    }
}
