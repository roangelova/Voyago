import { useState } from "react";
import { getFormattedDob } from "../../helpers";
import { useDeletePassenger } from "../../services/passengerService";
import { useAccountContext } from "./AccountContext";
import PassengerForm from "./PassengerForm";
import PassengerInfo from "./PassengerInfo";

export default function PassengerList() {
  const { mutate: deletePassenger } = useDeletePassenger();
  const { passengers } = useAccountContext();
  const [showEditForm, setShowEditForm] = useState(false);
  const [passengerToEdit, setPassengerToEdit] = useState(null);

  return (
    <div className="passengers">
      <div className="passengers__content">
        <h4 className="heading--quaternary">Passengers:</h4>
        {passengers?.length === 0 ? (
          <div className="passengers__noPassengers">
            <p>No passengers registered to this account yet.</p>
          </div>
        ) : (
          passengers.map((passenger) => (
            <PassengerInfo
              passenger={passenger}
              setPassengerToEdit={setPassengerToEdit}
              passengerToEdit={passengerToEdit}
              showEditForm={showEditForm}
              setShowEditForm={setShowEditForm}
            />
          ))
        )}

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
    </div>
  );
}
