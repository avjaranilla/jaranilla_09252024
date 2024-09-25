using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.domain.Domain
{
    public class FileProcessingLog
    {
        public int Id { get; set; } // Primary key
        public string Filename { get; set; } // File Name
        public TimeSpan ProcessingTime { get; set; } // Duration for processing
        public DateTime ProcessedAt { get; set; } // Timestamp of processing
    }
}
