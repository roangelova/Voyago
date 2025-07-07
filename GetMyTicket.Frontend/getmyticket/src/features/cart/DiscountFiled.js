import { useState } from "react";
import { toast } from "react-toastify";

function DiscountField({
  discountCode,
  setDiscountCode,
  discountApplied,
  setDiscountApplied,
}) {
  function applyDiscountCode() {
    let isValid = IsAValidDiscountCode();

    if (!isValid) {
      toast.error("Invalid discount code!");
      setDiscountCode("");
    } else {
      //apply the discount to the totalPrice
      toast.success("Discount code applied successfully!");
      setDiscountApplied(true);
    }
  }

  return (
    <div className="discountField">
      {!discountApplied && (
        <>
          <label htmlFor="discount">Have a discount code?</label>
          <div>
            <input
              value={discountCode}
              onChange={(e) => setDiscountCode(e.target.value)}
              type="text"
              placeholder="MY DISCOUNT"
            ></input>
            <button
              disabled={discountApplied}
              onClick={() => applyDiscountCode()}
            >
              Apply
            </button>
          </div>
        </>
      )}

      {discountApplied && (
        <p className="discountField__codeUsed">
          Code used: <span>WELCOME10</span> for <span>10%</span> off!
        </p>
      )}
    </div>
  );
}

export default DiscountField;

function IsAValidDiscountCode() {
  //TODO: should check the following:
  // - if the user has already used the discount code
  // - if code is still active
  //for now, the behavior is static
  //USER SHOULD NOT BBE ABLE TO USE MORE THAN ONE DISCOUNT CODE PER BOOKING!

  return true;
}
