import React from "react";

function SelectPassengersDropdown({
  passengersCountRequestedForBooking,
  userPassengerList,
  setPassengerIdsForBooking,
}) {
  return (
    <>
      {[...Array(passengersCountRequestedForBooking.adults - 1)].map((_, i) => (
        <React.Fragment key={i}>
          <label htmlFor="adults">Adult {i + 2}:</label>
          <select
            name="adults"
            id="adults"
            onChange={(e) =>
              setPassengerIdsForBooking((curr) => [...curr, e.target.value])
            }
          >
            <option key={null} value={null}>
              --Select passenger--
            </option>
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

      {[...Array(passengersCountRequestedForBooking.children)].map((_, i) => (
        <React.Fragment key={i}>
          <label htmlFor="children">Child {+1}:</label>
          <select
            name="children"
            id="chldren"
            onChange={(e) =>
              setPassengerIdsForBooking((curr) => [...curr, e.target.value])
            }
          >
            <option key={null} value={null}>
              --Select passenger--
            </option>
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
      {[...Array(passengersCountRequestedForBooking.infants)].map((_, i) => (
        <React.Fragment key={i}>
          <label htmlFor="infants">Infant {i + 1}:</label>
          <select
            id="infants"
            name="infants"
            onChange={(e) =>
              setPassengerIdsForBooking((curr) => [...curr, e.target.value])
            }
          >
            <option key={null} value={null}>
              --Select passenger--
            </option>
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

export default SelectPassengersDropdown;
