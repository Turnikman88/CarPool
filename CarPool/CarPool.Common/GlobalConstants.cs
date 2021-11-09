namespace CarPool.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "CarPool";

        public const string AdministratorRoleName = "Administrator";

#pragma warning disable SA1310 // Field names should not contain underscore
        public const string PASSWORD_ERROR_MESSAGE = "Password must be at least 8 symbols and should contain capital letter, digit and special symbol (+, -, *, &, ^, …)";
#pragma warning restore SA1310 // Field names should not contain underscore
    }
}
