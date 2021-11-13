namespace CarPool.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "CarPool";

        public const string AdministratorRoleName = "Administrator";

        public const string PassRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";

        public const string PhoneRegex = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

        public const int PageSkip = 10;


        public const string PASSWORD_ERROR_MESSAGE = "Password must be at least 8 symbols and should contain capital letter, digit and special symbol (+, -, *, &, ^, …)";

        public const string INVALID_EMAIL = "Invalid Email Address!";

        public const string NO_FEEDBACK = "(No feedback)";

        public const string NO_COMMENT = "(No comment)";

        public const string NO_TITLE = "(No title)";

        public const string INCORRECT_DATA = "Incorrect or missing data!";

        public const string CITY_EXISTS = "City with this name already exists!";

        public const string ADDRESS_EXISTS = "This address already exists!";

        public const string ADDRESS_NOT_FOUND = "Address not found";

        public const string CITY_NOT_FOUND = "City not found";

        public const string TRIP_NOT_FOUND = "Trip not found";

        public const string PROFILE_PICTURE_NOT_FOUND = "Profile picture not found";

        public const string INVALID_ID = "Invalid Id!";

        public const string COUNTRY_NOT_FOUND = "Country not found!";

        public const string USER_NOT_FOUND = "User not found!";

        public const string COUNTRY_EXISTS = "Country with this name already exists!";

        public const string PICTURE_EXISTS = "This picture is already on your profile!";

        public const string NO_CAR_AVAILABLE = "No car available!";

        public const string USER_EXISTS = "User with this email already exists!";

        public const string USER_PERMANENT_BLOCK = "User is permanently blocked!";
    }
}
