using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GetMyTicket.Common.Constants
{
    public static class ResponseConstants
    {
        //ERROR

        public const string SomethingWentWrong = "Oops...Something went wrong.";

        public const string InvalidCredentials = "Invalid credentials. Please try again!";
        public const string Invalid = "Invalid {0}";
        public const string InvalidType = "{0} is not a valid type of {1}";
        public const string InvalidDateFormat = "The provided date format was incorrect";

        public const string NotFoundError = "{0} with Id {1} was not found.";
        public const string UserNotFound = "User was not found.";
        public const string NoBookingsForThisUser = "No bookings were found for this user";

        public const string DuplicateIsAccountOwnerWhenCreatingPassenger = "There already is a registered account owner. Could not create passenger. To create an account for this passenger, please proceed to register.";

        public const string SoldOut = "Uh oh! The itinerary is no longer available. Please, choose a different connection!";

        public const string NotSupported = "{0} not supported.";

        public const string InvalidTripTime = "Invalid start or end time for trip.";

        public const string CantBeNull = "{0} can't be null.";

        public const string AllFieldsRequired = "All fields are required";

        public const string CantBeTheSame = "{0} and {1} can't be the same";

        public const string OwnershipMissmatch = "Ownership missmatch. Vehicle does not belong to this transportation provider.";
        public const string OwnershipChangeNotAllowed = "Cannot update trip vehicle to another provider. To do so, please cancel the current trip and add a new one.";

        public const string CantDeleteAccountOwnersPassengerEntity = "Passenger can't be deleted as he is registered as an account owner. If you wish to proceed with a deletion, close your account instead.";
        public const string CantDeleteXwithActiveY = "Cannot delete a {0} with active {1}";

        public const string InvalidOperation = "Invalid operation.";

        public const string SameTripAlredyBooked = "Can't book this trip as it'a already booked";

        //discount
        public const string DiscountHasBeenUsed = "Discount code has already been used on a previous booking";
        public const string DiscountExpired = "Discount code has expired!";
        public const string InvalidDiscount = "Invalid discount code!";
        public const string CantApplyDiscount = "Can't apply discount to this booking!";
        //SUCCESS

        public const string LogoutSuccessful = "Successfully logged out.";

        //INACTIVE OR DELETED STATE
        public const string AccountDeactivated = "Account has been deactivated.";

        //VALIDATION

        public const string UserWithThisEmailExist = "An account with this email already exists.";
    }
}
