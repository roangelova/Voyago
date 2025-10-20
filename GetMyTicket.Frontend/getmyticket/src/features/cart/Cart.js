import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import check from "../../assets/icons/check.png";
import fallbackLogo from "../../assets/images/fallbackLogo.png";
import partnerImage from "../../assets/images/partnerImage.png";
import { formatDate } from "../../helpers";
import { useCreateBooking } from "../../services/bookingService";
import { Passenger } from "../../services/passengerService";
import PassengerForm from "../account/PassengerForm";
import AddBagsForm from "./AddBagsForm";
import SelectPassengersDropdown from "./SelectPassengersDropdown";
import { BaggagePrices } from "../../services/baggagePriceService";
import { calculateTotalPriceForCart, CreatePriceSummary } from "./cartHelpers";
import DiscountField from "./DiscountFiled";
import { useAccountContext } from "../account/AccountContext";
const userId = localStorage.getItem("userId");

const baggageOptions = [
  { key: "CarryOn", value: 8 },
  { key: "Small", value: 23 },
  { key: "Large", value: 32 },
];

//TODO - split up component; this file is huge!!!

function Cart() {
  const location = useLocation();
  const createBooking = useCreateBooking();
  const { trip, passengers: passengersCountForBooking } = location.state || {};
  const { passengers: passengersFromContext } = useAccountContext();
  const [userPassengerList, setUserPassengerList] = useState(
    passengersFromContext
  );
  const [passengerIdsForBooking, setPassengerIdsForBooking] = useState([]);
  const [accountOwner, setAccountOwner] = useState(null);
  const [baggage, setBaggage] = useState([]);
  const [baggagePrices, setBaggagePrices] = useState(null);
  const [discountCode, setDiscountCode] = useState("");
  const [discount, setdDiscount] = useState(null);
  const total = Object.values(passengersCountForBooking).reduce(
    (a, b) => a + b,
    0
  );
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
      if (filteredOwner) {
        //account holder has registeres his passenger data
        setAccountOwner(filteredOwner);
        setUserPassengerList(
          data.filter((x) => x.passengerId !== filteredOwner.passengerId)
        );
        setPassengerIdsForBooking([
          ...passengerIdsForBooking,
          filteredOwner?.passengerId,
        ]);
      }
    });

    if (trip) {
      BaggagePrices.getPricesForProvider(trip?.transportationProviderId).then(
        (data) => {
          setBaggagePrices(data);
        }
      );
    }
  }, []);

      useEffect(() => {
      let filteredOwner = passengersFromContext.filter((x) => x?.isAccountOwner === true)[0];
      if (filteredOwner) {
        //account holder has registeres his passenger data
        setAccountOwner(filteredOwner);
        setUserPassengerList(
          passengersFromContext.filter((x) => x.passengerId !== filteredOwner.passengerId)
        );
        setPassengerIdsForBooking([
          ...passengerIdsForBooking,
          filteredOwner?.passengerId,
        ]);
      }
    }, [passengersFromContext]);

  const onCheckout = async () => {
    if (!trip.tripId) {
      toast.error("Something went wrong.");
      navigate("/");
    }

    if (total > passengerIdsForBooking.length) {
      console.log(passengerIdsForBooking);
      let confirm = window.confirm(
        "You have not selected all passengers for this booking Do you wish to proceed and book only for the selected passengers? "
      );
      if (!confirm) {
        return;
        //user has terminated the checkout process due to not all passengers added to booking
      }
    }

    try {
      await createBooking.mutateAsync({
        tripId: trip.tripId,
        passengerIds: passengerIdsForBooking,
        baggage: baggage,
        userId,
        discountId: discount?.id,
      });
      navigate("/account/bookings");
    } catch (error) {
      toast.error(error?.message || "Could not create booking.");
    }
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
              <p>
                Departure: <strong>{formatDate(trip.startTime)}</strong>
              </p>
              <p>
                Arrival: <strong>{formatDate(trip.endTime)}</strong>
              </p>
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
                {baggage.length === 0 && (
                  <p>No bags added to this booking yet!</p>
                )}

                {baggage.map((b) => (
                  <div key={b.type}>
                    <img alt="icon" className="cart__checkIcon" src={check} />
                    <li>
                      {b.amount}x {b.type} {b.amount === 1 ? "bag" : "bags"}
                    </li>
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
                      baggagePrices={baggagePrices}
                    />
                  </div>
                </div>
              ) : null}
            </div>
            <div className="cart_row">
              <h5>Who's going to travel?</h5>
              <div className="cart__passengers">
                <>
                  {accountOwner ? (
                    <>
                      {" "}
                      <label>Adult 1: </label>
                      <select disabled>
                        <option>
                          {accountOwner?.firstName} {accountOwner?.lastName}
                        </option>
                      </select>
                    </>
                  ) : (
                    <button
                      className="btn"
                      onClick={() => setShowPassengerForm(true)}
                    >
                      Add your data{" "}
                    </button>
                  )}
                </>

                <SelectPassengersDropdown
                  passengersCountForBooking={passengersCountForBooking}
                  userPassengerList={userPassengerList}
                  setPassengerIdsForBooking={setPassengerIdsForBooking}
                />
              </div>

              {total > 1 && (
                <div className="cart__addPassenger">
                  Want to{" "}
                  <span onClick={() => setShowPassengerForm(true)}>create</span>{" "}
                  a new passenger instead?
                </div>
              )}
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

            <div className="cart_row">
              <img
                className="cart__partnerImg"
                href="#"
                src={partnerImage}
                alt="An image that links to a partner website for rent a cars."
              />
            </div>
          </div>

          <div className="cart__right">
            <h5>Price summary</h5>

            <div className="cart__pricePerPersonBreakdown">
              <ul>
                {CreatePriceSummary(
                  trip,
                  passengersCountForBooking,
                  baggage,
                  baggagePrices,
                  discountCode
                ).map((x) => (
                  <li key={x}>{x}</li>
                ))}
              </ul>
            </div>
            <DiscountField
              discountCode={discountCode}
              setDiscountCode={setDiscountCode}
              discount={discount}
              setDiscount={setdDiscount}
              bookingCurrentTotal ={(calculateTotalPriceForCart(
                      trip,
                      passengersCountForBooking,
                      baggage,
                      baggagePrices,
                      discount
                    ))}
            />

            <div className="cart__total">
              <div>
                <p>Total:</p>
                <p>
                  <strong>
                    {calculateTotalPriceForCart(
                      trip,
                      passengersCountForBooking,
                      baggage,
                      baggagePrices,
                      discount
                    )}
                  </strong>{" "}
                  {trip.currency}
                </p>
              </div>
              <span>* Prices include all applicable taxes and fees. Final charges may vary and will be confirmed before payment.</span>
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
