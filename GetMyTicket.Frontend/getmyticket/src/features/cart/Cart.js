import { useLocation, useNavigate } from "react-router-dom";
import { calculateTotalPrice, formatDate } from "../../helpers";
import SelectPassengersDropdown from "./SelectPassengersDropdown";
import fallbackLogo from "../../assets/images/fallbackLogo.png";
import { useEffect, useState } from "react";
import { Passenger } from "../../services/passengerService";
import check from "../../assets/icons/check.png";
import { Booking } from "../../services/bookingService";

const userId = sessionStorage.getItem("userId");

//add option to create new passenger
// make select work and reconnect 'addBooking functionalityy'
//once we have a working version -> add or remove passenger

function Cart() {
  const location = useLocation();
  const { trip, passengers: passengersCountForBooking } = location.state || {};
  const [userPassengerList, setUserPassengerList] = useState();
  const [passengerIdsForBooking, setPassengerIdsForBooking] = useState([]);

  const navigate = useNavigate();
  console.log(trip, passengersCountForBooking);

  let imageSrc;
  trip.transportationProviderLogo !== ""
    ? (imageSrc = `data:image/png;base64,${trip.transportationProviderLogo}`)
    : (imageSrc = fallbackLogo);

  useEffect(() => {
    if (!userId) {
      return;
    }
    Passenger.getPassengersForUser(userId).then((data) => {
      setUserPassengerList(data);
      console.log(data);
    });
  }, []);

  const onCheckout = () => {
    // Кои условия трябва да са изпълнени, за да замършим резервацията?
    //there should always be at least 1 adult per booking

    Booking.bookTrip({
      tripId: trip.tripId,
      passengerIds: passengerIdsForBooking,
      userId,
      //TODO - ADD A MUTATE FUNC AND HANDLE ERROR AND SUCCESS
    }).then(navigate('/account/bookings'))
  };

  return (
    <>
      <title>Cart | Voyago</title>
      <div className="cart__container">
        <div className="cart">
          <div className="cart__left">
            <h2 className="heading--secondary">Cart</h2>
            <div className="cart__row cart__trip">
              <h5>
                {trip.startCityName} to {trip.endCityName}
              </h5>
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
                <div>
                  <img className="cart__checkIcon" src={check} />
                  <li>Carry-on bag included</li>
                </div>
                <div>
                  <img className="cart__checkIcon" src={check} />
                  <li>
                    Free cancelation up to <strong>24 </strong>hours prior to
                    departure
                  </li>
                </div>
                <div>
                  <img className="cart__checkIcon" src={check} />
                  <li>Carry-on bag included</li>
                </div>
                <div>
                  <img className="cart__checkIcon" src={check} />
                  <li>Seat reservation</li>
                </div>
              </ul>
            </div>
            <div className="cart__row">
              <h5>Bags</h5>
              <ul>
                <div>
                  <img className="cart__checkIcon" src={check} />
                  <li>Carry-on bag included</li>
                </div>
                <div>
                  <img className="cart__checkIcon" src={check} />
                  <li>
                    {" "}
                    <strong>1x 23 kg </strong>checked-in bag per person
                  </li>
                </div>
              </ul>
              <button className="btn cart__btn">Add bags</button>
            </div>
            <div className="cart_row">
              <h5>Who's going to travel?</h5>
              <div className="cart__passengers">
                <SelectPassengersDropdown
                  passengersCountForBooking={passengersCountForBooking}
                  userPassengerList={userPassengerList}
                  setPassengerIdsForBooking={setPassengerIdsForBooking}
                />
              </div>
            </div>
          </div>

          <div className="cart__right">
            <h5>Price summary</h5>

            <div className="cart__pricePerPersonBreakdown">
              <ul>
                {CreatePriceSummary(trip, passengersCountForBooking).map(
                  (x) => (
                    <li>{x}</li>
                  )
                )}
              </ul>
            </div>
            <div className="cart__total">
              <div>
                <p>Total</p>
                <p>
                  {calculateTotalPrice(trip, passengersCountForBooking)} EUR
                </p>
              </div>
              <span>All taxes, fees and charges included</span>
            </div>
            <div className="cart__checkout">
              <button onClick={onCheckout} className="btn">
                Check out
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Cart;

function CreatePriceSummary(trip, passengersCountForBooking) {
  let summary = [];

  if (passengersCountForBooking.adults > 0) {
    summary.push(
      `${passengersCountForBooking.adults} Adult ticket${
        passengersCountForBooking.adults > 1 ? "s" : ""
      } — ${trip.adultPrice} ${trip.currency}`
    );
  }

  if (passengersCountForBooking.children > 0) {
    summary.push(
      `${passengersCountForBooking.children} Child ticket${
        passengersCountForBooking.children > 1 ? "s" : ""
      } — ${trip.childrenPrice} ${trip.currency}`
    );
  }

  if (passengersCountForBooking.infants > 0) {
    summary.push(
      `${passengersCountForBooking.infants} Infant ticket${
        passengersCountForBooking.infants > 1 ? "s" : ""
      } — Free`
    );
  }

  return summary;
}
