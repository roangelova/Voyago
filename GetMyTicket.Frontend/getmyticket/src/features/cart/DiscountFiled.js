import { toast } from "react-toastify";
import { Discount } from "../../services/discountService";
import { useAccountContext } from "../account/AccountContext";

function DiscountField({
  discountCode,
  setDiscountCode,
  discountApplied,
  setDiscount,
  discount
}) {
  var { passengers } = useAccountContext();
  var passengerId = passengers?.find((x) => x?.isAccountOwner)?.passengerId;

  function applyDiscountCode() {
    Discount.canApplyDiscountToBooking(passengerId, discountCode)
      .then((isValid) => {
        if (!isValid) {
          toast.error("Invalid discount code!");
          setDiscountCode("");
        } else {
          Discount.getByName(discountCode).then((discount) =>
            setDiscount(discount)
          //this line will make sure that the code is applied to the total price
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
              disabled={discount}
              onClick={() => applyDiscountCode()}
            >
              Apply
            </button>
          </div>
        </>
      )}

      {discount && (
        <p className="discountField__codeUsed">
          Code used: <span>{discount.name}</span> for <span>{discount.value} {discount.discountType === "Percent" ? '%' : 'EUR'}</span> off!
        </p>
      )}
    </div>
  );
}

export default DiscountField;
