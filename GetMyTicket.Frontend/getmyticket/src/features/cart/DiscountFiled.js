import { toast } from "react-toastify";
import { Discount } from "../../services/discountService";
import { useAccountContext } from "../account/AccountContext";
import { useEffect, useState } from "react";

function DiscountField({
  discountCode,
  setDiscountCode,
  setDiscount,
  discount,
  bookingCurrentTotal,
}) {
  var { passengers } = useAccountContext();
  const [passengerId, setPassengerId] = useState();

  useEffect(() => {
    setPassengerId(passengers?.find((x) => x?.isAccountOwner)?.passengerId);
  }, [passengers]);

  function applyDiscountCode() {
    if (discountCode.trim() === "") {
      toast.error("Invalid discount code!");
      return;
    }

    if (!passengerId) {
      toast.error("Please enter your passenger details first.");
      return;
    }

    //also checks for the minimum amount
    Discount.canApplyDiscountToBooking(
      passengerId,
      discountCode,
      bookingCurrentTotal
    )
      .then((isValid) => {
        if (!isValid) {
          toast.error("Invalid discount code!");
          setDiscountCode("");
        } else {
          Discount.getByName(discountCode).then((discount) =>
            //this line will make sure that the code is applied to the total price
            setDiscount(discount)
          );
          toast.success("Discount code applied successfully!");
        }
      })
      .catch((err) => toast.error(err.message));
  }

  return (
    <div className="discountField">
      {!discount && (
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
              disabled={discountCode === ""}
              onClick={() => applyDiscountCode()}
            >
              Apply
            </button>
          </div>
        </>
      )}

      {discount && (
        <p className="discountField__codeUsed">
          Code used: <span>{discount.name}</span> for{" "}
          <span>
            {discount.value} {discount.discountType === "Percent" ? "%" : "EUR"}
          </span>{" "}
          off!
        </p>
      )}
    </div>
  );
}

export default DiscountField;
