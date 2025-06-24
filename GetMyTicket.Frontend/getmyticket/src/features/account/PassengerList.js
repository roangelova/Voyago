import { useEffect, useState } from "react";
import { useAccountContext } from "./AccountContext";
import { Passenger } from "../../services/passengerService";
import { getFormattedDob } from "../../helpers";
import EditPassengerPopup from "./EditPassengerPopup";

export default function PassengerList() {
  const data = useAccountContext();
  const [passengers, setPassengers] = useState([]);
  const [showEditForm, setShowEditForm] = useState(false);
  const [passengerToEdit, setPassengerToEdit] = useState(null);

  useEffect(() => {
    Passenger.getPassengersForUser(data.userId).then((res) =>
      setPassengers(res)
    );
  }, []);

  return (
    <div className="passengers">
      <div className="passengers__content">
        <h4 className="heading--quaternary">Passengers:</h4>

        { passengers?.length === 0 ? 
          <p className="account__noData">No passengers registered to this account yet</p>
         : null}

        {passengers?.map((p) => (
          <div key={p?.passengerId} className="passengers__row">
            <div className="passengers__information">
              <div>
                <p className="passengers--name">
                  {p?.firstName} {p?.lastName}
                </p>
                {p?.isAccountOwner ? (
                  <span className="passengers__accountOwner">
                    Account Owner
                  </span>
                ) : null}
              </div>

              <p>
                <strong>{p?.passengerType}</strong>
              </p>
              <p>{getFormattedDob(p?.dob)}</p>
            </div>
            <div className="passengers__actions">
              <button
                className="btn"
                onClick={() => {
                  setPassengerToEdit(p);
                  setShowEditForm(true);
                }}
              >
                Edit
              </button>
              <button className="btn">Delete</button>
            </div>
          </div>
        ))}
      </div>

      {showEditForm ? (
        <div className="blur-overlay" onClick={() => setShowEditForm(false)}>
          <div onClick={(e) => e.stopPropagation()}>
            <EditPassengerPopup passengertoEdit={passengerToEdit} />
          </div>
        </div>
      ) : null}
    </div>
  );
}
