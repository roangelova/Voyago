using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Passenger;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Common.Mapping_Tables;
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
        public async Task<Guid> CreatePassenger(CreatePassengerDTO dto)
        {
            var user = await unitOfWork.Users.GetByIdAsync(dto.UserId);

            if (user == null)
            {
                throw new ApplicationError(
                    string.Format(ResponseConstants.NotFoundError, nameof(User), dto.UserId));
            }

            if (dto.IsAccountOwner)
            {
                //If there already is an account owner registered, we cannot register a second one
                var registeredAccountOwnersForUserId = await unitOfWork.UserPassengerMap.GetAllAsync(x => x.UserId == dto.UserId && x.IsAccountOwner == true);

                if (registeredAccountOwnersForUserId.Any())
                {
                    throw new ApplicationException(ResponseConstants.DuplicateIsAccountOwnerWhenCreatingPassenger);
                }
            }

            var gender = ParseEnum<Gender>(dto.Gender);

            Passenger passenger = new Passenger()
            {
                PassengerId = Guid.CreateVersion7(),
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Gender = gender,
                DocumentId = string.IsNullOrWhiteSpace(dto?.DocumentId) ? null : dto.DocumentId.Trim(),
                ExpirationDate = dto.DocumentExpirationDate,
                DOB = dto.Dob,
                Nationality = string.IsNullOrWhiteSpace(dto?.Nationality) ? null : dto.Nationality.Trim()
            };

            if (!string.IsNullOrWhiteSpace(dto.DocumentType))
            {
                passenger.DocumentType = ParseEnum<DocumentType>(dto.DocumentType);
            }

            int age = CalculateAge(dto.Dob);

            if (age < 0)
            {
                throw new ApplicationError(string.Format(ResponseConstants.Invalid, nameof(age)));
            }

            if (age < 2)
            {
                passenger.PassengerType = PassengerType.Infant;
            }
            else if (age >= 2 && age < 18)
            {
                passenger.PassengerType = PassengerType.Child;
            }
            else if (age > 18 && age < 100)
            {
                passenger.PassengerType = PassengerType.Adult;
            }

            passenger.UserPassengerMaps.Add(new UserPassengerMap()
            {
                UserId = user.Id,
                PassengerId = passenger.PassengerId,
                Label = string.IsNullOrWhiteSpace(dto.Label) ? null : dto.Label,
                IsAccountOwner = dto.IsAccountOwner
            });

            await unitOfWork.Passengers.AddAsync(passenger);
            await unitOfWork.SaveChangesAsync();

            return passenger.PassengerId;
        }

        ///NOTE: Gender, DOB and PassengerType cannot be edited. A passenger cannot be transfered from one profile to another (change of UserId). 
        ///To achieve this, the passenger must be deactivated from the old account and recreated on the new one. 
        public async Task<GetPassengerDTO> EditPassenger(EditPassengerDTO dto, CancellationToken cancellationToken)
        {
            var passenger = await unitOfWork.Passengers.GetByIdAsync(dto.PassengerId);
            if (passenger == null)
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Passenger), dto.PassengerId));

            var userPassengerMapResult = await unitOfWork.UserPassengerMap.GetAllAsync(x => x.PassengerId == passenger.PassengerId);
            var userPassengerMap = userPassengerMapResult.FirstOrDefault();

            if (userPassengerMap == null)
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(UserPassengerMap), dto.PassengerId));

            passenger.FirstName = string.IsNullOrWhiteSpace(dto.FirstName) ? passenger.FirstName : dto.FirstName.Trim();
            passenger.LastName = string.IsNullOrWhiteSpace(dto.LastName) ? passenger.LastName : dto.LastName.Trim();
            passenger.DocumentId = string.IsNullOrWhiteSpace(dto?.DocumentId) ? passenger.DocumentId : dto.DocumentId.Trim();
            passenger.ExpirationDate = dto.DocumentExpirationDate ?? dto.DocumentExpirationDate;
            passenger.Nationality = string.IsNullOrWhiteSpace(dto?.Nationality) ? passenger.Nationality : dto.Nationality.Trim();
            userPassengerMap.Label = string.IsNullOrWhiteSpace(dto?.Label) ? userPassengerMap.Label : dto.Label.Trim();


            if (!string.IsNullOrWhiteSpace(dto.DocumentType))
            {
                passenger.DocumentType = ParseEnum<DocumentType>(dto.DocumentType);
            }

            unitOfWork.Passengers.Update(passenger);
            await unitOfWork.SaveChangesAsync();

            return new GetPassengerDTO
            {
                PassengerId = passenger.PassengerId,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Gender = passenger.Gender.ToString(),
                Dob = passenger.DOB,
                DocumentType = passenger.DocumentType.ToString(),
                DocumentId = passenger.DocumentId,
                Nationality = passenger.Nationality
            };
        }


        public async Task<List<GetPassengerDTO>> GetPassengerAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);

            if (user is null)
            {
                throw new ApplicationError(
                     string.Format(ResponseConstants.NotFoundError, nameof(User), userId));
            }

            var passengers = await unitOfWork.UserPassengerMap.GetAllAsync(x => x.UserId == userId, null, false, cancellationToken, x => x.Passenger);

            return passengers.Select(passenger => new GetPassengerDTO()
            {
                PassengerId = passenger.PassengerId,
                FirstName = passenger.Passenger.FirstName,
                LastName = passenger.Passenger.LastName,
                Gender = passenger.Passenger.Gender.ToString(),
                Label = passenger.Label,
                DocumentType = passenger.Passenger.DocumentType.ToString(),
                DocumentId = passenger.Passenger.DocumentId,
                Dob = passenger.Passenger.DOB,
                Nationality = passenger.Passenger.Nationality,
                PassengerType = Enum.GetName<PassengerType>(passenger.Passenger.PassengerType),
                IsAccountOwner = passenger.IsAccountOwner,
                DocumentExpirationDate = passenger.Passenger.ExpirationDate
            }).ToList();
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

        public async Task DeletePassenger(Guid passengerId, CancellationToken cancellationToken)
        {
            var passenger = await unitOfWork.Passengers.GetAsync(
                x => x.PassengerId == passengerId,
                false, cancellationToken,
                x => x.UserPassengerMaps,
                x => x.PassengerBookingMaps);

            if (passenger.UserPassengerMaps.Any(x => x.IsAccountOwner is true))
            {
                throw new ApplicationException(ResponseConstants.CantDeleteAccountOwnersPassengerEntity);
            }

            bool hasActiveBookings = passenger.PassengerBookingMaps.Any(x => !x.IsDeleted);
            if (hasActiveBookings)
            {
                throw new ApplicationException(string.Format(ResponseConstants.CantDeleteXwithActiveY, nameof(Passenger), nameof(Booking)));
            }

            passenger.IsDeleted = true;
            passenger.DeletedAt = DateTime.UtcNow;

            foreach (var x in passenger.UserPassengerMaps)
            {
                x.IsDeleted = true;
                x.DeletedAt = DateTime.UtcNow;
            }

            unitOfWork.Passengers.Update(passenger);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return;
        }

        private static int CalculateAge(DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dob.Year;

            if (today < dob.AddYears(age))
                age--;

            return age;
        }

        private static T ParseEnum<T>(string value) where T : struct, Enum
        {
            if (Enum.TryParse<T>(value, out var result))
            {
                return result;
            }

            throw new ApplicationException(string.Format(ResponseConstants.NotSupported, value));
        }
    }
}
