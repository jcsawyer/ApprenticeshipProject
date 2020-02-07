namespace FirstCatering.Lib.Validation
{
    /// <summary>
    /// Regex validation helper
    /// </summary>
    public static class Regexes
    {
        /// <summary>
        /// Company issued employee id regex validation string
        /// </summary>
        /// <remarks>
        /// 16 alphanumeric character string
        /// </remarks>
        public const string EmployeeId = @"^([a-zA-Z0-9]{16})$";

        /// <summary>
        /// Email regex validation string
        /// </summary>
        /// <remarks>
        /// Valid email address
        /// </remarks>
        public const string Email = @"^([a-z0-9_\.\-]{3,})@([\da-z\.\-]{3,})\.([a-z\.]{2,6})$";

        /// <summary>
        /// UK mobile phone number regex validation string
        /// </summary>
        /// <remarks>
        /// Canonical UK mobile phone number
        /// </remarks>
        public const string Mobile = @"^((\(44\))( )?|(\(\+44\))( )?|(\+44)( )?|(44)( )?)?((0)|(\(0\)))?( )?(((1[0-9]{3})|(7[1-9]{1}[0-9]{2})|(20)( )?[7-8]{1})( )?([0-9]{3}[ -]?[0-9]{3})|(2[0-9]{2}( )?[0-9]{3}[ -]?[0-9]{4}))$";

        /// <summary>
        /// Employee security pin validation string
        /// </summary>
        /// <remarks>
        /// 4 digits only
        /// </remarks>
        public const string PIN = @"^([0-9]{4})$";
    }
}