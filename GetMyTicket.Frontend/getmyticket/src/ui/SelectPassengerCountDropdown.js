function SelectPassengerCountDropdown({ passengers, setPassengers }) {
  const handleChange = (e) => {
    const { name, value } = e.target;

    setPassengers((prev) => ({
      ...prev,
      [name]: Number(value),
    }));
  };

  return (
    <div className="passengerDropdown">
      <label htmlFor="passengerDropdown__adults">Adults:</label>
      <input
        name="adults"
        id="passengerDropdown__adults"
        type="number"
        value={passengers.adults}
        onChange={handleChange}
        in="1"
        max="10"
        min={1}
        placeholder="1"
        required
      />

      <label htmlFor="passengerDropdown__children">Children:</label>
      <input
        name="children"
        id="passengerDropdown__children"
        type="number"
        value={passengers.children}
        onChange={handleChange}
        max="10"
        min="0"
        placeholder="1"
      />

      <label htmlFor="passengerDropdown__infants">Infants:</label>
      <input
        id="passengerDropdown__infants"
        name="infants"
        type="number"
        value={passengers.infants}
        onChange={handleChange}
        max="10"
        min="0"
        placeholder="1"
      />
    </div>
  );
}

export default SelectPassengerCountDropdown;
