import { useState } from "react";
import { getFormattedDob } from "../../helpers";
import { useDeletePassenger } from "../../services/passengerService";
import { useAccountContext } from "./AccountContext";
import PassengerForm from "./PassengerForm";

export default function PassengerList() {
  const [showEditForm, setShowEditForm] = useState(false);
  const [passengerToEdit, setPassengerToEdit] = useState(null);
 const { mutate: deletePassenger } = useDeletePassenger();
  const { passengers } = useAccountContext();

  function onHandleDelete(passengerId) {
    deletePassenger(passengerId);
  }

  return (
    <div className="passengers">
      <div className="passengers__content">
        <h4 className="heading--quaternary">Passengers:</h4>
        {passengers?.length === 0 ? (
          <div className="passengers__noPassengers">
            <p>No passengers registered to this account yet.</p>
          </div>
        ) : null}

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
              <button
                className="btn"
                onClick={() => onHandleDelete(p?.passengerId)}
              >
                Delete
              </button>
            </div>
          </div>
        ))}
        <button
          className="btn"
          onClick={() => {
            setShowEditForm(true);
            setPassengerToEdit(null);
          }}
        >
          Add passenger
        </button>
      </div>

      {showEditForm ? (
        <div className="blur-overlay" onClick={() => setShowEditForm(false)}>
          <div onClick={(e) => e.stopPropagation()}>
            <PassengerForm
              passengertoEdit={passengerToEdit}
              setPassengerToEdit={setPassengerToEdit}
              setShowEditForm={setShowEditForm}
            />
          </div>
        </div>
      ) : null}
    </div>
  );
}
