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

        public const string InvalidDocumentType = "Invalid document type";
        public const string InvalidGender = "Gender not reccognized";

        public const string InvalidDateFormat = "The provided date format was incorrect";
    }
}
