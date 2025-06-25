import { useState } from "react";
import { Passenger } from "../../services/passengerService";
import { toast } from "react-toastify";

function PassengerForm({ passengertoEdit,setShowEditForm }) {
  const [passenger, setPassenger] = useState(passengertoEdit);
  const [isEdit, setIsEdit] = useState(!!passengertoEdit);

  //TODO --> ADD VALIDATION
  function handleEditField(e) {
    let fieldToUpdate = e.target.name;
    let newValue = e.target.value;

    setPassenger((prev) => ({
      ...prev,
      [fieldToUpdate]: newValue,
    }));
  }

  const titlePage = passenger?.passengerId ? "Edit passenger" : "Add passenger";
  const titleBtn = passenger?.passengerId ? "Edit" : "Add";

  function handleOnClick(e) {
    e.preventDefault();

    let data = {
      passengerId: passenger.passengerId,
      firstName: passenger.firstName,
      lastName: passenger.lastName,
      label: passenger.label,
      nationality: passenger.nationality,
      documentType: passenger.documentType,
      documentId: passenger.documentId,
      documentExpirationDate: passenger.documentExpirationDate,
    };

    if (passenger.passengerId) {
      Passenger.editPassenger(data)
        .then(() => {
          setShowEditForm(false)
          toast.info("Passenger was edited successfully.");
        })
        .catch((err) => toast.error(err));
      //passenger exists. This is an edit operation
    } else {
      //create passenger
      Passenger.createPassenger({
        ...data,
        userId: sessionStorage.getItem("userId"),
        gender: passenger.gender,
        dob: passenger.dob,
        isAccountOwner: passenger.isAccountOwner,
      })
        .then(() => {
          toast.info("Passenger was created successfully.");
          setShowEditForm(false)
        })
        .catch((err) => toast.error(err));
    }
  }

  return (
    <div className={`passengerForm ${!isEdit ?'passengerForm__create' : null}`}>
      <form>
        <h4 className="heading--quaternary">{titlePage}</h4>
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

        {!isEdit ? 
          <>
            <div>
              <label htmlFor="gender">Gender:</label>
              <select id="gender" name="gender" onBlur={handleEditField}>
                <option key={0} value={"Male"}>
                  Male
                </option>
                <option key={1} value={"Female"}>
                  Female
                </option>
                <option key={2} value={"Other"}>
                  Other
                </option>
              </select>
            </div>
            <div>
              <label htmlFor="isAccountOwner">
                Is this passenger the account owner?
              </label>
              <input type="checkbox" name="isAccountOwner" defaultChecked />
            </div>
            <div>
              <label htmlFor="dob">Date of birth:</label>
              <input type="date" name="dob" defaultChecked />
            </div>
          </>
        : null}

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
            name="documentType"
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
          <button onClick={handleOnClick} className="btn">
            {titleBtn}
          </button>
        </div>
      </form>
    </div>
  );
}

export default PassengerForm;
