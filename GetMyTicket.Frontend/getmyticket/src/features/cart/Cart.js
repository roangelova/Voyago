import { useLocation } from "react-router-dom";
import { formatDate } from "../../helpers";
import { toast } from "react-toastify";
import fallbackLogo from "../../assets/images/fallbackLogo.png";

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
  const { trip, passengers } = location.state || {};

  console.log(trip, passengers);

  let imageSrc;
  trip.transportationProviderLogo !== ""
    ? (imageSrc = `data:image/png;base64,${trip.transportationProviderLogo}`)
    : (imageSrc = fallbackLogo);

  //TODO -> Make sure the time zones are correct
  return (
    <>
      <title>Cart | Voyago</title>
      <div className="cart__container">
        <div className="cart">
          <div className="cart__left">
            <div className="cart__row">
              <p>
                {trip.startCityName} to {trip.endCityName}
              </p>
              <p>Departure: {formatDate(trip.startTime)}</p>
              <p>Arrival:{formatDate(trip.endTime)}</p>
              <div className="cart__provider">
                <img src={imageSrc} />
                <p>{trip.transportationProviderName}</p>
              </div>
            </div>

            <div className="cart__row">
              <h5>Your fare: Economy</h5>
              <ul>
                <li>✔️ Carry-on bag included</li>
                <li>✔️ Free cancelation up to 24h prior to departure</li>
                <li>✔️ Carry-on bag included</li>
                <li>✖️ Changes not allowed</li>
              </ul>
            </div>
            <div className="cart__row">
              <h5>Bags</h5>
              <ul>
                <li>✔️ Carry-on bag included</li>
                <li>✔️ 1x 23kg checked-in bag per person</li>
              </ul>
              <button className="btn">Add bags</button>
            </div>
          </div>

          <div className="cart__right">
            <h5>Price summary</h5>

            <div className="cart__pricePerPersonBreakdown">
              <span><strong>Traveller 1</strong>:Adult</span>
              <span> 220.00 EUR</span>
              <span><strong>Traveller 2</strong>:Adult</span>
              <span> 220.00 EUR</span>
              <span><strong>Traveller 3</strong>:Infant</span>
              <span> 00.00 EUR</span>
            </div>
            <div className="cart__total">
             <div>
              <p>Total</p>
              <p>440 EUR</p>
              </div>
              <span>All taxes, fees and charges included</span>
            </div>
            <div className="cart__checkout">
              <button className="btn">Check out</button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Cart;
