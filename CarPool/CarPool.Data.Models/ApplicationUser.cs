// ReSharper disable VirtualMemberCallInConstructor
namespace CarPool.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarPool.Common;
    using CarPool.Data.Common.Models;

    public class ApplicationUser : IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

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

        public virtual Rating Ratings { get; set; }

        public Guid ProfilePictureId { get; set; }

        public virtual ProfilePicture ProfilePicture { get; set; }

        public int MyProperty { get; set; }

        public bool IsBlocked { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
