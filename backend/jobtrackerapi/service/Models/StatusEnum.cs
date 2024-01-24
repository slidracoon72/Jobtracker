using System;

namespace service.Models
{
    // This enum represents the possible status values for a job in the service layer.
    public enum StatusEnum
    {
        // The job application has been submitted.
        Applied,

        // The job application has been reviewed by the employer.
        Reviewed,

        // The job application is incomplete or missing information.
        Incomplete,

        // The job application has been rejected by the employer.
        Rejected,

        // The job application is pending an online assessment.
        PendingOA,

        // The job application has been accepted.
        Accepted
    }
}
