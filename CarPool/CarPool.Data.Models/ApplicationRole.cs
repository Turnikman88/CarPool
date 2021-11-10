namespace CarPool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using CarPool.Data.Common.Models;

    public class ApplicationRole : BaseDeletableModel<Guid>
    {
        public ApplicationRole(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
