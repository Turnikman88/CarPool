namespace CarPool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using CarPool.Data.Common.Models;

    public class ApplicationRole : BaseDeletableModel<int>
    {
        public ApplicationRole(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
