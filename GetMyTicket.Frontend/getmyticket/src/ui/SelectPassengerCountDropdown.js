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
      <label htmlFor="adults">Adults:</label>
      <input
        name="adults"
        type="number"
        value={passengers.adults}
        onChange={handleChange}
        in="1"
        max="10"
        placeholder="1"
        required
      />

      <label htmlFor="children">Children:</label>
      <input
        name="children"
        type="number"
        value={passengers.children}
        onChange={handleChange}
        max="10"
        placeholder="1"
      />

      <label htmlFor="infants">Infants:</label>
      <input
        name="infants"
        type="number"
        value={passengers.infants}
        onChange={handleChange}
        max="10"
        placeholder="1"
      />
    </div>
  );
}

export default SelectPassengerCountDropdown;
