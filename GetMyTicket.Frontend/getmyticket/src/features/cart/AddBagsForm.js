import { useState } from "react";

function AddBagsForm({ setAddBags, baggageOptions, setBaggage, baggagePrices }) {
  const [selectValue, setSelectValue] = useState(null);

  function onAddClickHandler(e) {
    e.preventDefault();
    setAddBags(false);

    setBaggage((prev) => {
      const existing = prev.find((b) => b.type === selectValue);
      if (existing) {
        return prev.map((b) =>
          b.type === selectValue ? { ...b, amount: b.amount + 1 } : b
        );
      } else {
        return [...prev, { type: selectValue, amount: 1 }];
      }
    });
  }

  return (
    <div className="addBagsForm addBagsForm__container">
      <h4 className="heading--quaternary">Add bags</h4>
      <form>
        <select onBlur={(e) => setSelectValue(e.target.value)}>
          <option value={null}>---Select size---</option>
          {baggageOptions.map((x) => (
            <option
              key={x.key}
              name={x.key}
              value={x.key}
            >
              {x.key} bag, max {x.value}kg., {baggagePrices?.find(b => b?.size ===x.key)?.price} EUR
            </option>
          ))}
        </select>
        <p>Please note sizes and prices are individual for every provider.</p>
        <div>
          <button className="btn" type="submit" onClick={onAddClickHandler}>
            Add
          </button>
        </div>
      </form>
    </div>
  );
}

export default AddBagsForm;
