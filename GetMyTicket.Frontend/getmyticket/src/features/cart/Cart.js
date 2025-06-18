import { useLocation } from "react-router-dom";

import { toast } from "react-toastify";


const userId = sessionStorage.getItem("userId");

const initialState = {
  tripId: null,
  userId: userId,
  passengers: [],
  trip: {},
  activeStep: 1,
};

function Cart() {
  const location = useLocation();
  const { trip } = location.state || {};

console.log(trip)

  return (
    <>
      <title>Cart | Voyago</title>
      <div className="cart">
      Welcome to the new cart component
      </div>
    </>
  );
}

export default Cart;
