using System;

namespace service.Models
{
    // This class represents a model for a job in the service layer.
    public class Job
    {
        public string? logoUrl { get; set; }

        public string? jobId { get; set; }
        // Unique identifier for the job.
        public Guid Id { get; set; }

        // The title of the job.
        public string? title { get; set; }

        // The name of the company offering the job.
        public string? companyName { get; set; }

        // The location of the job.
        public string? location { get; set; }

        // The salary or compensation associated with the job.
        public string? salary { get; set; }

        // Keywords related to the job.
        public string? jobKeywords { get; set; }

        // Identifier of the user associated with this job.
        public Guid userId { get; set; }

        // The status of the job, represented as a StatusEnum.
        public StatusEnum status { get; set; }

        // The URL or link to the job posting.
        public string? url { get; set; }

        // The date when the job entry was created.
        public DateTime? CreatedDate { get; set; }

        // The date when the job entry was last updated.
        public DateTime? UpdatedDate { get; set; }
    }
}
