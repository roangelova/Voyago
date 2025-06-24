import { useState } from "react";
import { Passenger } from "../../services/passengerService";
import { toast } from "react-toastify";

function EditPassengerPopup({ passengertoEdit }) {
  const [passenger, setPassenger] = useState(passengertoEdit);

  function handleEditField(e) {
    let fieldToUpdate = e.target.name;
    let newValue = e.target.value;

    setPassenger((prev) => ({
      ...prev,
      [fieldToUpdate]: newValue,
    }));
  }

  function handleEditPassenger(e) {
    e.preventDefault();
    Passenger.editPassenger({
      passengerId: passenger.passengerId,
      firstName: passenger.firstName,
      lastName: passenger.lastName,
      label: passenger.label,
      nationality: passenger.nationality,
      documentType: passenger.documentType,
      documentId: passenger.documentId,
      documentExpirationDate: passenger.documentExpirationDate,
    })
      .then(() => {
        toast.information("Passenger was edited successful.");
      })
      .catch((err) => toast.error(err));
  }

  return (
    <div className="editPassenger">
      <form>
        <h4 className="heading--quaternary">Edit passenger</h4>
        <div>
          <label htmlFor="firstName">First name:</label>
          <input
            type="text"
            name="firstName"
            defaultValue={passenger?.firstName}
            onBlur={handleEditField}
          />
        </div>
        <div>
          <label htmlFor="lastName">Last name:</label>
          <input
            type="text"
            name="lastName"
            defaultValue={passenger?.lastName}
            onBlur={handleEditField}
          />
        </div>
        <div>
          <label htmlFor="label">Label:</label>
          <input
            type="text"
            name="label"
            defaultValue={passenger?.label}
            onBlur={handleEditField}
          />
        </div>
        <div>
          <label htmlFor="nationality">Nationality:</label>
          <input
            type="text"
            name="nationality"
            defaultValue={passenger?.nationality}
            onBlur={handleEditField}
          />
        </div>
        <div>
          <label htmlFor="documentType">Document type:</label>
          <select
            defaultValue={passenger?.documentType}
            id="documentType"
            name="documentTypes"
            onBlur={handleEditField}
          >
            <option key={0} value={null}>
              --Choose document--
            </option>
            <option key={1} value={"idCard"}>
              National ID
            </option>
            <option key={2} value={"passport"}>
              Passport
            </option>
          </select>
        </div>
        <div>
          <label htmlFor="documentId">Document number:</label>
          <input
            type="text"
            name="documentId"
            defaultValue={passenger?.documentId}
            onBlur={handleEditField}
          />
        </div>
        <div>
          <label htmlFor="documentExpirationDate">Document expires:</label>
          <input
            type="date"
            name="documentExpirationDate"
            defaultValue={passenger?.documentExpirationDate}
            onChange={handleEditField}
          />
        </div>
        <div>
          <button onClick={handleEditPassenger} className="btn">Edit</button>
        </div>
      </form>
    </div>
  );
}

export default EditPassengerPopup;
