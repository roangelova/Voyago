using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Passenger;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Passengers;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IUnitOfWork unitOfWork;

        public PassengerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The method creates a passenger entity for the provided User. In order to travel, anyone needs to be a registered passenger. The method 
        /// creates an Adult, Child or Infant, based on the provided age, and returns the id of the created passenger.
        /// </summary>
        /// <param name="CreateOrEditPassengerDTO"></param>
        /// <returns></returns>
        public async Task<Guid> CreatePassenger(CreateOrEditPassengerDTO dto)
        {
            var user = await unitOfWork.Users.GetByIdAsync(dto.UserId);

            if (user == null)
            {
                throw new ApplicationError(
                    string.Format(ResponseConstants.NotFoundError, nameof(User), dto.UserId));
            }

            var (gender, documentType, DateOfBirth, DocumentExpirationDate) = ParseAndValidatePassengerData(dto);

            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

            int? age = user.DOB.HasValue
                ? today.Year - user.DOB.Value.Year - (today < user.DOB.Value.AddYears(today.Year - user.DOB.Value.Year) ? 1 : 0)
                 : null;

            Passenger passenger;

            if (age >= 18)
            {
                passenger = new Adult()
                {
                    PassengerId = Guid.CreateVersion7(),
                    FirstName = dto.FirstName.Trim(),
                    LastName = dto.LastName.Trim(),
                    User = user,
                    UserId = dto.UserId.Value,
                    Gender = gender,
                    DocumentType = documentType,
                    DocumentId = dto.DocumentId.Trim(),
                    ExpirationDate = DocumentExpirationDate,
                    DOB = DateOfBirth,
                    Nationality = dto.Nationality.Trim()
                };

                user.PassengerMapId = passenger.PassengerId;
            }
            else if(age >= 2 && age <18 )
            {
                passenger = new Child()
                {
                    FirstName = dto.FirstName.Trim(),
                    LastName = dto.LastName.Trim(),
                    Gender = gender,
                    DOB = DateOfBirth,
                    Nationality = dto.Nationality.Trim()
                };
            }
            else
            {
                passenger = new Child()
                {
                    FirstName = dto.FirstName.Trim(),
                    LastName = dto.LastName.Trim(),
                    Gender = gender,
                    DOB = DateOfBirth,
                    Nationality = dto.Nationality.Trim()
                };
            }

            await unitOfWork.Passengers.AddAsync(passenger);
            await unitOfWork.SaveChangesAsync();

            return passenger.PassengerId;
        }

        public async Task<GetPassengerDTO> EditPassenger(Guid passengerId, CreateOrEditPassengerDTO dto)
        {
            //At this stage, every passenger is an adult; This will change as the application grows
            Adult passenger = await unitOfWork.Passengers.GetByIdAsync(passengerId) as Adult;
            if (passenger == null)
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Passenger), passengerId));

            var user = await unitOfWork.Users.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(User), dto.UserId));

            var (gender, documentType, DateOfBirth, DocumentExpirationDate) = ParseAndValidatePassengerData(dto);

            passenger.FirstName = dto.FirstName.Trim();
            passenger.LastName = dto.LastName.Trim();
            passenger.Gender = gender;
            passenger.DocumentType = documentType;
            passenger.DocumentId = dto.DocumentId.Trim();
            passenger.ExpirationDate = DocumentExpirationDate;
            passenger.DOB = DateOfBirth;
            passenger.Nationality = dto.Nationality.Trim();

            unitOfWork.Passengers.Update(passenger);
            await unitOfWork.SaveChangesAsync();

            //TODO -> add FN LN to passenger obj;
            return new GetPassengerDTO
            {
                UserId = user.Id,
                PassengerId = passenger.PassengerId,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Gender = passenger.Gender.ToString(),
                DocumentType = passenger.DocumentType.ToString(),
                DocumentId = passenger.DocumentId,
                Dob = passenger.DOB,
                Nationality = passenger.Nationality
            };
        }


        private static (Gender gender, DocumentType documentType, DateOnly DateOfBirth, DateOnly DocumentExpirationDate)
            ParseAndValidatePassengerData(CreateOrEditPassengerDTO dto)
        {
            if (!Enum.TryParse<Gender>(dto.Gender, out Gender gender))
                throw new ArgumentException(string.Format(ResponseConstants.NotSupported, nameof(dto.Gender)));

            if (!Enum.TryParse<DocumentType>(dto.DocumentType, out DocumentType documentType))
                throw new ArgumentException(string.Format(ResponseConstants.NotSupported, nameof(dto.DocumentType)));

            bool dobParseResult = DateOnly.TryParse(dto.Dob, out DateOnly DateOfBirth);
            bool documentExpirationParseResult = DateOnly.TryParse(dto.DocumentExpirationDate, out DateOnly DocumentExpirationDate);

            if (!dobParseResult || !documentExpirationParseResult)
                throw new ArgumentException(ResponseConstants.InvalidDateFormat);

            return (gender, documentType, DateOfBirth, DocumentExpirationDate);
        }

        public async Task<GetPassengerDTO> GetPassengerAsync(Guid userId)
        {
            var user = await unitOfWork.Users.GetByIdAsync(userId);

            if (user is null)
            {
                throw new ApplicationError(
                     string.Format(ResponseConstants.NotFoundError, nameof(User), userId));
            }

            var passenger = await unitOfWork.Passengers.GetByIdAsync(user.PassengerMapId) as Adult;

            if (passenger == null)
            {
                return null;
            }

            return new GetPassengerDTO()
            {
                UserId = userId,
                PassengerId = passenger.PassengerId,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Gender = passenger.Gender.ToString(),
                DocumentType = passenger.DocumentType.ToString(),
                DocumentId = passenger.DocumentId,
                Dob = passenger.DOB,
                Nationality = passenger.Nationality
            };
        }

        public async Task<List<GetNameAndAgePassengerDataDTO>> GetPassengersForBooking(Guid bookingId, CancellationToken cancellationToken = default)
        {
            var passengers = await unitOfWork.PassengerBookingMap.GetAllAsync(x => x.BookingId == bookingId, null, true, cancellationToken, x => x.Passenger);

            var result = new List<GetNameAndAgePassengerDataDTO>();

            //TODO -> add a func to calculate age properly and consider leap years etc
            foreach (var passenger in passengers)
            {
                result.Add(new GetNameAndAgePassengerDataDTO
                {
                    Name = passenger.Passenger.FirstName + ' ' + passenger.Passenger.LastName,
                    Age = CalculateAge(passenger.Passenger.DOB)
                });
            }

            return result;
        }

        public static int CalculateAge(DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dob.Year;

            if (today < dob.AddYears(age))
                age--;

            return age;
        }
    }
}
