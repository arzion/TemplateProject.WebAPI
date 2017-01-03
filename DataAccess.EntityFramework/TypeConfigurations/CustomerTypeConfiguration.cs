using System.Data.Entity.ModelConfiguration;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.EntityFramework.TypeConfigurations
{
    /// <summary>
    /// The fluent configuration of <see cref="Customer"/> type.
    /// </summary>
    internal class UserTypeConfiguration : EntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserTypeConfiguration"/> class.
        /// </summary>
        public UserTypeConfiguration()
        {
            ToTable("Customers");
            HasKey(user => user.Id);
            Property(user => user.LastName).HasMaxLength(400);
            Property(user => user.FirstName).HasMaxLength(400);
            Ignore(user => user.FullName);
        }
    }
}