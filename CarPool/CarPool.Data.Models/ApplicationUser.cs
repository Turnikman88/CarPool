// ReSharper disable VirtualMemberCallInConstructor
namespace CarPool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarPool.Common;
    using CarPool.Data.Common.Models;

    public class ApplicationUser : BaseDeletableModel<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            ProfilePicture = new HashSet<ProfilePicture>();
        }

        [MinLength(2)]
        [MaxLength(20)]
        public string Username { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [MinLength(8)]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$",
            ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public double Rating { get; set; }

        public Guid ProfilePictureId { get; set; }

        public virtual ICollection<ProfilePicture> ProfilePicture { get; set; }

        public Guid RoleId { get; set; }

        public bool IsBlocked { get; set; }
    }
}
