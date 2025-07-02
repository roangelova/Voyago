import { useState } from "react";
import { getFormattedDob } from "../../helpers";
import PassengerForm from "./PassengerForm";
import { useDeletePassenger } from "../../services/passengerService";

function PassengerInfo({
  passenger: p,
  setPassengerToEdit,
  passengerToEdit,
  showEditForm,
  setShowEditForm,
}) {
  const { mutate: deletePassenger } = useDeletePassenger();

  const [showAdditionalInformation, setShowAdditionalInformation] =
    useState(false);

  function onHandleDelete(passengerId) {
    deletePassenger(passengerId);
  }

  return (
    <>
      <div key={p?.passengerId} className="passenger__row">
        <div className="passenger__mainInfo">
          <div className="passenger__information">
            <div>
              <p className="passenger--name">
                {p?.firstName} {p?.lastName}
              </p>
              {p?.isAccountOwner ? (
                <>
                  <span className="passenger__accountOwner">
                    Account Owner
                  </span>
                </>
              ) : null}
            </div>

            <p>
              <strong>{p?.passengerType}</strong>
            </p>
            <p>{getFormattedDob(p?.dob)}</p>
            <span
              className="passenger__toggleInfo"
              onClick={() =>
                setShowAdditionalInformation(!showAdditionalInformation)
              }
            >
              {showAdditionalInformation ? "Show less  ^ " : "Show more ∨"}
            </span>
          </div>

          <div className="passenger__actions">
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
        {showAdditionalInformation && (
          <div className="passenger__additionalInfo">
            <p>
              <span>Nationality: </span> {p?.nationality || "---"}
            </p>
            <p>
              <span>Document type: </span> {p?.documentType || "---"}
            </p>
            <p>
              <span>Document Id: </span>
              {p?.documentId || "---"}
            </p>
            <p>
              <span>Document expires: </span>
              {p?.documentExpirationDate || "---"}
            </p>
          </div>
        )}
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
    </>
  );
}

export default PassengerInfo;
