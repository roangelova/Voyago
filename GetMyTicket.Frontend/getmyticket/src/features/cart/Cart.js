import { useLocation, useNavigate } from "react-router-dom";
import { calculateTotalPrice, formatDate } from "../../helpers";
import SelectPassengersDropdown from "./SelectPassengersDropdown";
import fallbackLogo from "../../assets/images/fallbackLogo.png";
import { useEffect, useState } from "react";
import { Passenger } from "../../services/passengerService";
import check from "../../assets/icons/check.png";
import { Booking } from "../../services/bookingService";
import PassengerForm from "../account/PassengerForm";
import AddBagsForm from "./AddBagsForm";

const userId = sessionStorage.getItem("userId");

const baggageOptions = [
  { key: "Carry-on", value: 8 },
  { key: "Small", value: 23 },
  { key: "Large", value: 32 },
];

//TODO -? features to implement in the feature: baggage options should be fetched from the backend; Furthermore, each bag size should have a price and it should be added to the total Price
//TODO -> INCLUDE BAGGAGE OPTIONS IN BOOKING

//add option to create new passenger
// make select work and reconnect 'addBooking functionalityy'
//once we have a working version -> add or remove passenger

function Cart() {
  const location = useLocation();
  const { trip, passengers: passengersCountForBooking } = location.state || {};
  const [userPassengerList, setUserPassengerList] = useState();
  const [passengerIdsForBooking, setPassengerIdsForBooking] = useState([]);
  const [accountOwner, setAccountOwner] = useState(null);
  const [baggage, setBaggage] = useState([
    { key: baggageOptions[0].key, count: 1 },
  ]);

  console.log(baggage);
  //show form popups controls:
  const [showPassengerForm, setShowPassengerForm] = useState(false);
  const [showAddBags, setAddBags] = useState(false);

  const navigate = useNavigate();

  let imageSrc;
  trip?.transportationProviderLogo !== ""
    ? (imageSrc = `data:image/png;base64,${trip?.transportationProviderLogo}`)
    : (imageSrc = fallbackLogo);

  useEffect(() => {
    if (!userId) {
      return;
    }
    Passenger.getPassengersForUser(userId).then((data) => {
      let filteredOwner = data.filter((x) => x?.isAccountOwner === true)[0];
      setAccountOwner(filteredOwner);
      setUserPassengerList(
        data.filter((x) => x.passengerId !== filteredOwner.passengerId)
      );
      setPassengerIdsForBooking([
        ...passengerIdsForBooking,
        filteredOwner?.passengerId,
      ]);
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
    }).then(navigate("/account/bookings"));
  };

  return (
    <>
      <title>Cart | Voyago</title>
      <div className="cart__container">
        <div className="cart">
          <div className="cart__left">
            <h4 className="heading--quaternary">
              Your {trip.typeOfTrip.toLowerCase()} booking
            </h4>
            <div className="cart__row cart__trip">
              <h5>
                {trip.startCityName} to {trip.endCityName}
              </h5>
              <p>Departure: {formatDate(trip.startTime)}</p>
              <p>Arrival: {formatDate(trip.endTime)}</p>
              <div className="cart__provider">
                <img alt="icon" src={imageSrc} />
                <p>{trip.transportationProviderName}</p>
              </div>
            </div>

            <div className="cart__row">
              <h5>Your fare: Economy</h5>
              <ul>
                <div>
                  <img alt="icon" className="cart__checkIcon" src={check} />
                  <li>Carry-on bag included</li>
                </div>
                <div>
                  <img alt="icon" className="cart__checkIcon" src={check} />
                  <li>
                    Free cancelation up to <strong>24 </strong>hours prior to
                    departure
                  </li>
                </div>
                <div>
                  <img alt="icon" className="cart__checkIcon" src={check} />
                  <li>Carry-on bag included</li>
                </div>
                <div>
                  <img alt="icon" className="cart__checkIcon" src={check} />
                  <li>Seat reservation</li>
                </div>
              </ul>
            </div>
            <div className="cart__row">
              <h5>Bags</h5>
              <ul>
                {baggage.map((b) => (
                  <div>
                    <img alt="icon" className="cart__checkIcon" src={check} />
                    <li>{b.count}x {b.key} {b.count === 1 ?'bag' : 'bags' }</li>
                  </div>
                ))}
              </ul>
              <button
                className="btn cart__btn"
                onClick={() => setAddBags(true)}
              >
                Add bags
              </button>

              {showAddBags ? (
                <div className="blur-overlay" onClick={() => setAddBags(false)}>
                  <div onClick={(e) => e.stopPropagation()}>
                    <AddBagsForm
                      setBaggage={setBaggage}
                      setAddBags={setAddBags}
                      baggageOptions={baggageOptions}
                    />
                  </div>
                </div>
              ) : null}
            </div>
            <div className="cart_row">
              <h5>Who's going to travel?</h5>
              <div className="cart__passengers">
                <>
                  <label>Adult 1: </label>
                  <select disabled>
                    <option>
                      {accountOwner?.firstName} {accountOwner?.lastName}
                    </option>
                  </select>
                </>

                <SelectPassengersDropdown
                  passengersCountForBooking={passengersCountForBooking}
                  userPassengerList={userPassengerList}
                  setPassengerIdsForBooking={setPassengerIdsForBooking}
                />
              </div>
              <div className="cart__addPassenger">
                Want to{" "}
                <span onClick={() => setShowPassengerForm(true)}>add</span> a
                new passenger instead?
              </div>
            </div>

            {showPassengerForm ? (
              <div
                className="blur-overlay"
                onClick={() => setShowPassengerForm(false)}
              >
                <div onClick={(e) => e.stopPropagation()}>
                  <PassengerForm
                    passengertoEdit={null}
                    setShowEditForm={setShowPassengerForm}
                  />
                </div>
              </div>
            ) : null}

            <div className="cart__important">
              <h4>Important notice!</h4>
              <p>
                Please not that if your passport is set to expire in less than{" "}
                <b>six months</b> after your trip, you could be denied boarding
                or entry! Always check the current entry regulations for your
                destination in advance.
              </p>
              <p>
                If additional documents are required &#40;such as birth
                sertificates for passengers under 18 years old, visas etc.&#41;,
                you will be asked to provide those at the airport. Please note
                that failure to do may result in denied bording.
              </p>
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
                  <strong>
                    {calculateTotalPrice(trip, passengersCountForBooking)}
                  </strong>{" "}
                  {trip.currency}
                </p>
              </div>
              <span>* All taxes, fees and charges included</span>
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
      } — ${trip.adultPrice} ${trip.currency} p.p.`
    );
  }

  if (passengersCountForBooking.children > 0) {
    summary.push(
      `${passengersCountForBooking.children} Child ticket${
        passengersCountForBooking.children > 1 ? "s" : ""
      } — ${trip.childrenPrice} ${trip.currency} p.p.`
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
