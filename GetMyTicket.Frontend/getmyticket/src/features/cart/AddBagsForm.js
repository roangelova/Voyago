import { useState } from "react";

//  const [baggage, setBaggage] = useState({size: baggageOptions[0].key, count: 1})

function AddBagsForm({ setAddBags, baggageOptions, setBaggage }) {
  const [selectValue, setSelectValue] = useState(null);

  function onAddClickHandler() {
    setAddBags(false);

    console.log("select value", selectValue);
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
              disabled={x.value === 8}
              key={x.key}
              name={x.key}
              value={x.key}
            >
              {x.key}, max {x.value}kg.
            </option>
          ))}
        </select>
        <p>Please note that a carry-on is included by default.</p>
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
