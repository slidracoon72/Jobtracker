using database.Models;
using Microsoft.EntityFrameworkCore;

namespace database.context
{
    /// <summary>
    /// Represents a custom database context that inherits from DbContext in Entity Framework.
    /// </summary>
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /// <summary>
        /// Constructor for the DbContext class, which accepts DbContextOptions as a parameter.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public DbContext(DbContextOptions options) : base(options)
        {
            // Initialize the DbContext with the provided options.
        }

        /// <summary>
        /// Represents a database DbSet for the 'User' entity.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Represents a database DbSet for the 'Job' entity.
        /// </summary>
        public DbSet<Job> Job { get; set; }

        /// <summary>
        /// Represents a database DbSet for the 'SingleSignOn' entity.
        /// </summary>
        public DbSet<SingleSignOn> SingleSignOn { get; set; }

        /// <summary>
        /// Represents a database DbSet for the 'ProviderData' entity.
        /// </summary>
        public DbSet<ProviderData> ProviderData { get; set; }
    }
}
