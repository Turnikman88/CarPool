// ReSharper disable VirtualMemberCallInConstructor
namespace CarPool.Data.Models
{
    using System;

    using CarPool.Data.Common.Models;

    public class ApplicationRole : IAuditInfo, IDeletableEntity
    {
        public ApplicationRole(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
