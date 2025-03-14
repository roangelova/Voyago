namespace GetMyTicket.Common.Constants
{
    public static class ErrorMessages
    {
        //API RESPONSE

        public const string InvalidCredentials = "Invalid credentials. Please try again!";
        public const string SomethingWentWrong = "Oops...Something went wrong.";

        public const string NotFoundError = "{0} with Id {1} was not found.";

        public const string UserUnderage = "No solo undearage travellers supported at this time";

        public const string SoldOut = "Uh oh! The itinerary is no longer available. Please, choose a different connection!";

        public const string Invalid = "Invalid {0}";
        public const string NotSupported = "{0} not supported.";
        public const string InvalidType = "{0} is not a valid type of {1}";

        public const string InvalidDateFormat = "The provided date format was incorrect";

        public const string CantBeNull = "{0} can't be null.";

        public const string AllFieldsRequired = "All fields are required";

        public const string CantBeTheSame = "{0} and {1} can't be the same";

        public const string OwnershipMissmatch = "Ownership missmatch. Vehicle does not belong to this transportation provider.";
    }
}
