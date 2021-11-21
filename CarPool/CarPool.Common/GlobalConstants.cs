﻿namespace CarPool.Common
{
    public static class GlobalConstants
    {
        public const string Domain = "https://localhost:5001";

        public const string Secret = "CarPoolSecretTelerikAcademy"; //minimum 16 letters

        public const string SystemName = "CarPool";

        public const string AdministratorRoleName = "Admin";

        public const string UserRoleName = "User";

        public const string BannedRoleName = "Banned";

        public const string NotConfirmedRoleName = "NotConfirmed";

        public const string PassRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";

        public const string PhoneRegex = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

        public const int PageSkip = 10;

        public const string VALUE_LENGTH_ERROR = "Value for {0} must be between {2} and {1}.";

        public const string PASSWORD_ERROR_MESSAGE = "Password must be at least 8 symbols and should contain capital letter, digit and special symbol (+, -, *, &, ^, …)";

        public const string PASSWORDS_MUST_MATCH = "Password and Confirmation Password must match.";

        public const string INVALID_EMAIL = "Invalid Email Address!";

        public const string NO_FEEDBACK = "(No feedback)";

        public const string NO_COMMENT = "(No comment)";

        public const string NO_TITLE = "(No title)";

        public const string INCORRECT_DATA = "Incorrect or missing data!";

        public const string CITY_EXISTS = "City with this name already exists!";

        public const string ADDRESS_EXISTS = "This address already exists!";

        public const string ADDRESS_NOT_FOUND = "Address not found!";

        public const string ADDRESS_TOO_SHORT = "Address name cannot be that short";

        public const string CITY_NOT_FOUND = "City not found!";

        public const string TRIP_NOT_FOUND = "Trip not found!";

        public const string PROFILE_PICTURE_NOT_FOUND = "Profile picture not found";

        public const string INVALID_ID = "Invalid Id!";

        public const string COUNTRY_NOT_FOUND = "Country not found!";

        public const string USER_NOT_FOUND = "User not found!";

        public const string COUNTRY_EXISTS = "Country with this name already exists!";

        public const string PICTURE_EXISTS = "This picture is already on your profile!";

        public const string NO_CAR_AVAILABLE = "No car available!";

        public const string USER_EXISTS = "User with this email already exists!";

        public const string USER_PHONE_EXISTS = "User with this phone number already exists!";

        public const string WRONG_PHONE = "Incorrect phone number!";

        public const string WRONG_GUID = "Wrong guid!";

        public const string USER_PERMANENT_BLOCK = "User is permanently blocked!";

        public const string USER_UNBLOCKED = "User with email {0} was successfuly unbanned";

        public const string TRIP_USER_BLOCKED_JOIN = "User is currently banned and cannot join or create trips!";

        public const string TRIP_FULL = "This trip is full. No empty seats left.";

        public const string WRONG_CREDENTIALS = "Wrong credentials!";

        public const string LOGGED = "You logged successfully!";

        public const string NOT_AUTHORIZED = "You are not authorized!";

        public const string NOT_PROVIDED = "(Not provided)";
    }
}
