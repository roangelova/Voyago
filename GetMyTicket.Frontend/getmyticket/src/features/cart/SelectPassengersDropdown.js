import React from "react";

function SelectPassengesDropdown({
  passengersCountForBooking,
  userPassengerList,
  setPassengerIdsForBooking,
}) {
  return (
    <>
      {[...Array(passengersCountForBooking.adults -1)].map((_, i ) => (
     <React.Fragment key={i}>
          <label htmlFor="adults">Adult {i + 2}:</label>
          <select
            name="adults"
            id="adults"
            onChange={(e) =>
              setPassengerIdsForBooking((curr) => [...curr, e.target.value])
            }
          >
            {userPassengerList
              ?.filter((x) => x.passengerType === "Adult")
              .map((passenger) => (
                <option
                  key={passenger.passengerId}
                  value={passenger.passengerId}
                >
                  {passenger.firstName} {passenger.lastName}
                </option>
              ))}
          </select>
          </React.Fragment>
      ))}

      {[...Array(passengersCountForBooking.children)].map((_, i) => (
        <React.Fragment key={i}>
          <label htmlFor="children">Child { + 1}:</label>
          <select
            name="children"
            id="chldren"
            onChange={(e) =>
              setPassengerIdsForBooking((curr) => [...curr, e.target.value])
            }
          >
            {userPassengerList
              ?.filter((x) => x.passengerType === "Child")
              .map((passenger) => (
                <option
                  key={passenger.passengerId}
                  value={passenger.passengerId}
                >
                  {passenger.firstName} {passenger.lastName}
                </option>
              ))}
          </select>
        </React.Fragment>
      ))}
      {[...Array(passengersCountForBooking.infants)].map((_, i) => (
        <React.Fragment key={i}>
          <label htmlFor="infants">Infant {i + 1}:</label>
          <select
            id="infants"
            name="infants"
            onChange={(e) =>
              setPassengerIdsForBooking((curr) => [...curr, e.target.value])
            }
          >
            {userPassengerList
              ?.filter((x) => x.passengerType === "Infant")
              .map((passenger) => (
                <option
                  key={passenger.passengerId}
                  value={passenger.passengerId}
                >
                  {passenger.firstName} {passenger.lastName}
                </option>
              ))}
          </select>
        </React.Fragment>
      ))}
    </>
  );
}

export default SelectPassengesDropdown;
