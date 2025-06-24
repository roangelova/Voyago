import { getFormattedDate, getFormattedTime } from "../../helpers";
import { useEffect, useState } from "react";
import { Passenger } from "../../services/passengerService";
import { BoardingPassPDF } from "../common/BoardingPassPDF";
import { usePDF } from "@react-pdf/renderer";
import { useCancelBooking } from "../../services/bookingService";
import { useNavigate, useParams } from "react-router-dom";
import { useAccountContext } from "./AccountContext";

function BookingDetails() {
  const { mutate: cancelBooking } = useCancelBooking();
  const { bookingId } = useParams();
  const { bookings } = useAccountContext();
  const navigate = useNavigate();
  const [passengers, setPassengers] = useState([]);
  const [instance, updateInstance] = usePDF({ document: BoardingPassPDF });

  const booking = bookings.find((b) => b.bookingId === bookingId);

  if (booking === undefined) {
    navigate("/404");
  }

  useEffect(() => {
    if (booking !== undefined) {
      Passenger.getNameAndAge(booking?.bookingId).then((data) =>
        setPassengers(data)
      )
    }
  }, [booking]);

  function handleCancelBooking(id) {
    const confirmed = window.confirm(
      "Do you really want to cancel this booking?"
    );
    if (confirmed) {
      cancelBooking(id);
    }
  }

  return (
    <figure className="booking">
      <div className="booking__left">
        <div className="booking__tripData">
          <p className="booking__tripData-city">
            {booking?.fromCityName} - {booking?.toCityName}
          </p>
          <p>{getFormattedDate(booking?.departureTime || null)}</p>
          <p>
            Departure time: {getFormattedTime(booking?.departureTime || null)}
          </p>
        </div>

        <div className="booking__details">
          <div>
            <span>
              {passengers?.length}{" "}
              {passengers?.length === 1 ? "Passenger:" : "Passengers:"}
            </span>

            <ul className="booking__details--list">
            {passengers.map(passenger => 
              <li key={passenger?.name}>{passenger?.name}, {passenger?.age}</li>
            )}
            </ul>
          </div>
          <div>
            <span>Baggage Details:</span>
            <div className="booking__details--list">
              <p>2 x Checked Bags &#40;max. 23 kg&#41;</p>
              <p>1 x Carry-on &#40;max. 9 kg&#41;</p>
            </div>
            <a className="booking__addBaggageBtn">Modify</a>
          </div>
        </div>

        <div className="booking__help">
          <p>
            Need help? Call us at 098/123-789-456. We are here for you 24/7!
          </p>
        </div>
      </div>

      <div className="booking__right">
        <div className="booking__info">
          <p>Booking Ref: XYZ123 </p>
          <span
            className={
              booking?.status === "Confirmed"
                ? "booking--active"
                : "booking--canceled"
            }
          >
            {booking?.status}
          </span>
        </div>

        <div className="booking__priceBreakdown">
          <span>You paid:</span>
          <div className="booking__data">
            <p>
              Price per seat: <strong>210 EUR </strong>
            </p>
            <p>
              Total:{" "}
              <strong>
                {booking?.totalPrice} {booking?.currency}
              </strong>
            </p>
            <p className="booking__data--saved">
              Saved: <strong>40 EUR</strong>{" "}
            </p>
          </div>
        </div>

        {booking?.status === "Confirmed" && (
          <div className="booking__actions">
            <a className="btn" href={instance?.url} download="BoardingPass.pdf">
              Download boarding pass
            </a>
            <button
              onClick={() => handleCancelBooking(booking?.bookingId)}
              className="btn"
            >
              Cancel booking
            </button>
          </div>
        )}
      </div>
    </figure>
  );
}

export default BookingDetails;
