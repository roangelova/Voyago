using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Passenger;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Passengers;
using GetMyTicket.Common.Enum;
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


        //TODO: Generate TRIP PASSENGER LIST 


        /// <summary>
        /// The method creates a passenger entity for the provided User. In order to travel, anyone needs to be a registered passenger. The method 
        /// creates an Adult, Child or Infant, based on the provided age, and returns the id of the created passenger. Currently only 1 passenger per booking
        /// is supported. 
        /// </summary>
        /// <param name="bookTripDTO"></param>
        /// <returns></returns>
        public async Task<Guid> CreatePassengerData(CreateOrEditPassengerDTO dto)
        {
            var user = await unitOfWork.Users.GetByIdAsync(dto);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(dto.UserId),
                    string.Format(ErrorMessages.NotFoundError, nameof(User), dto.UserId));
            }

            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

            int? age = user.DOB.HasValue
                ? today.Year - user.DOB.Value.Year - (today < user.DOB.Value.AddYears(today.Year - user.DOB.Value.Year) ? 1 : 0)
                 : null;

            Passenger passenger;

            var parseResult = Enum.TryParse<Gender>(dto.Gender, out Gender gender);

            if (!parseResult)
            {
                throw new ArgumentException("Could not parse gender", nameof(dto.Gender));
            }

            if (age > 18)
            {
                passenger = new Adult()
                {
                    PassengerId = Guid.CreateVersion7(),
                    User = user,
                    Gender = gender
                };
            }
            else
            {
                throw new ArgumentException(ErrorMessages.UserUnderage);
            }

            await unitOfWork.Passengers.AddAsync(passenger);

            return passenger.PassengerId;
        }

        public async Task<Guid> GetPassengerIdAsync(Guid userId)
        {
            var user = await unitOfWork.Users.GetByIdAsync(userId); ;
            return user.PassengerMapId;
        }
    }
}
