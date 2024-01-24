using System.ComponentModel.DataAnnotations;

namespace database.Models
{
    // This class represents the 'Job' entity in the database.
    public class Job
    {
        // Unique identifier for the job.
        [Key]
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

        // The status of the job, represented as an enum (StatusEnum).
        public StatusEnum status { get; set; }

        // The URL or link to the job posting.
        public string? url { get; set; }

        // The date when the job entry was created.
        public DateTime? CreatedDate { get; set; }

        // The date when the job entry was last updated.
        public DateTime? UpdatedDate { get; set; }

        // The URL to logout
        public string? logoUrl { get; set; }

        // The ID for a job
        public string? jobId { get; set; }
    }
}
