import { useLocation } from "react-router-dom";
import { getFormattedDate, getFormattedTime } from "../../helpers";
import { useEffect, useState } from "react";
import { Passenger } from "../../services/passengerService";
import { BoardingPassPDF } from "../common/BoardingPassPDF";
import { usePDF } from "@react-pdf/renderer";
import { CancelBooking } from "../../services/bookingService";

function BookingDetails() {
  const { state: booking } = useLocation();
  const [passengers, setPassengers] = useState([]);
  const [instance, updateInstance] = usePDF({ document: BoardingPassPDF });

  useEffect(() => {
    Passenger.getNameAndAge(booking.bookingId).then((data) =>
      setPassengers(data)
    );
  }, [booking]);

  console.log(booking);

  return (
    <figure className="booking">
      <div className="booking__left">
        <div className="booking__tripData">
          <p className="booking__tripData-city">
            {booking.fromCityName} - {booking.toCityName}
          </p>
          <p>{getFormattedDate(booking.departureTime)}</p>
          <p>Departure time: {getFormattedTime(booking.departureTime)}</p>
        </div>

        <div className="booking__details">
          <div>
            <span>Total passenger: {passengers.length}</span>
            <div>
              {passengers?.map((p) => (
                <p key={p.name}>
                  {p.name}, {p.age}
                </p>
              ))}
            </div>
          </div>
          <div>
            <span>Baggage options</span>
            <div>
              <p>2 x big</p>
              <p>1 x carry-on</p>
              <p> - | + </p>
            </div>
          </div>
        </div>

        <div className="booking__help">
          <p>Need help? Call us at 098/000-000-000. We are her for you 24/7!</p>
        </div>
      </div>

      <div className="booking__right">
        <div className="booking__info">
          <p>Booking Ref: XYZ123 </p>
          <span
            className={
              booking.status === "Confirmed"
                ? "booking--active"
                : "booking--canceled"
            }
          >
            {booking.status}
          </span>
        </div>

        <div className="booking__priceBreakdown">
          <span>You paid:</span>
          <div className="booking__data">
            <p>
              Price per seat: <strong>230 EUR </strong>
            </p>
            <p>
              Total:{" "}
              <strong>
                {booking.totalPrice} {booking.currency}
              </strong>
            </p>
            <p>
              Saved: <strong>0 EUR</strong>{" "}
            </p>
          </div>
        </div>

        {booking.status === "Confirmed" && (
          <div className="booking__actions">
            <a className="btn" href={instance.url} download="BoardingPass.pdf">
              Download boarding pass
            </a>
            <button
              onClick={() => CancelBooking(booking.bookingId)}
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
