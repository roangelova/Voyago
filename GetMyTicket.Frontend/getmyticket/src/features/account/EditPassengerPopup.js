function EditPassengerPopup({ passenger }) {


  return (
    <div className="editPassenger">
      <form>
        <h4 className="heading--quaternary">Edit passenger</h4>
        <div>
          <label>First name:</label>
          <input type="text" value={passenger?.firstName} />
        </div>
        <div>
          <label>Last name:</label>
          <input type="text" value={passenger?.lastName} />
        </div>
        <div>
          <label>Label:</label>
          <input type="text" value={passenger?.label} />
        </div>
        <div>
          <label>Nationality:</label>
          <input type="text" value={passenger?.nationality} />
        </div>
        <div>
          <label htmlFor="documentType">Document type:</label>
          <select defaultValue={passenger?.documentType} id="documentType" name="documentTypes">
            <option value={null}>--Choose document--</option>
            <option value={"idCard"}>National ID</option>
            <option value={"passport"}>Passport</option>
          </select>
        </div>
        <div>
          <label>DocumentId:</label>
          <input type="text" value={passenger?.documentId} />
        </div>
        <div>
          <label>Document expires:</label>
          <input type="date" value={passenger?.documentExpirationDate} />
        </div>
        <div>
          <button className="btn">Edit</button>
        </div>
      </form>
    </div>
  );
}

export default EditPassengerPopup;
