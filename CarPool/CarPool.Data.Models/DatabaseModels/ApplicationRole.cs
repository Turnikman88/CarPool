namespace CarPool.Data.Models.DatabaseModels
{
    using CarPool.Data.Common.Models;
    using System.Collections.Generic;

    public class ApplicationRole : BaseDeletableModel<int>
    {
/*        public ApplicationRole(string name)
        {
            this.Name = name;
        }*/

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
