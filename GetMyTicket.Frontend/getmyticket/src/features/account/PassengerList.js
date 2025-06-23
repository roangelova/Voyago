import { useEffect, useState } from "react";
import { useAccountContext } from "./AccountContext";
import { Passenger } from "../../services/passengerService";
import { getFormattedDob } from "../../helpers";

export default function PassengerList() {
  const data = useAccountContext();
  const [passengers, setPassengers] = useState([]);
  console.log(data);

  useEffect(() => {
    Passenger.getPassengersForUser(data.userId).then((res) =>
      setPassengers(res)
    );
  }, []);

  return (
    <div className="passengers">
      <div className="passengers__content">
        <h4 className="heading--quaternary">Passengers:</h4>
        {passengers.map((p) => (
          <div className="passengers__row">
            <div className="passengers__information">
              <p className="passengers--name">
                {p?.firstName} {p?.lastName}
              </p>
              <p>
                <strongs>{p?.passengerType}</strongs>
              </p>
              <p>{getFormattedDob(p?.dob)}</p>
            </div>
            <div className="passengers__actions">
              <button className="btn">Edit</button>
              <button className="btn">Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
