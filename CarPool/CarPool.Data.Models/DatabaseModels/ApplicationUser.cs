namespace CarPool.Data.Models.DatabaseModels
{
    using CarPool.Common;
    using CarPool.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class ApplicationUser : BaseModel<Guid>
    {
        public ApplicationUser()
        {
            this.Trips = new HashSet<Trip>();
            this.Ratings = new HashSet<Rating>();
            this.Id = Guid.NewGuid();
            this.TripsAsPassenger = new HashSet<TripPassenger>();
            this.ApplicationRoleId = 4;
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
            GlobalConstants.PassRegex,
            ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public int? ApplicationRoleId { get; set; }

        public virtual ProfilePicture ProfilePicture { get; set; }

        public virtual Ban Ban { get; set; }

        [NotMapped]
        public double AverageRating { get => Ratings.Count > 0 ? Ratings.Select(x => x.Value).ToList().Average() : 0; }  //Ratings?.Select(x => x.Value).Average() ?? 0.00;

        public virtual ApplicationRole ApplicationRole { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        // public virtual ICollection<Rating> RatingsGiven { get; set; }

        public UserVehicle Vehicle { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<TripPassenger> TripsAsPassenger { get; set; }

    }
}
