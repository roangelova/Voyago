import { useLocation } from "react-router-dom";
import { formatDate, getFormattedDate, getFormattedTime } from "../../helpers";
import { useEffect, useState } from "react";
import { Passenger } from "../../services/passengerService";
import Spinner from "../../ui/Spinner";
import { BoardingPassPDF } from "../common/BoardingPassPDF";
import { usePDF } from "@react-pdf/renderer";

function BookingDetails() {
  const { state: booking } = useLocation();
  const [passengers, setPassengers] = useState([]);
  const [instance, updateInstance] = usePDF({ document: BoardingPassPDF });

  useEffect(() => {
    Passenger.getNameAndAge(booking.bookingId).then((data) =>
      setPassengers(data)
    );
  }, [booking]);

  return (
    // <div className="bookingDetails__container">
    //   <h2>Your booking {booking.bookingId}</h2>
    //
    //
    //   <div className="bookingDetails__box">
    //     <h3>Price breakdown</h3>
    //     <div className="bookingDetails__data">
    //       <p>Prize per booking: 230 EUR</p>
    //       <p>
    //         Total price: {booking.totalPrice} {booking.currency}
    //       </p>
    //       <p>Caroline Smith, age 1</p>
    //       <p>No discount used</p>
    //     </div>
    //   </div>
    //   <div className="bookingDetails__actions">
    //     <a
    //       className="btn"
    //       href={instance.url}
    //       download="BoardingPass.pdf"
    //     >
    //       Download boarding pass
    //     </a>
    //     <button className="btn destructiveBtn">Cancel booking</button>
    //   </div>
    //
    //   <div className="passengerDetails__help">
    //     <p>Need help? Call us at 098/000-000-000. We are her for you 24/7!</p>
    //   </div>
    // </div>

    <figure className="booking">
      <div className="booking__left">
        <div className="booking__tripData">
          <p className="booking__tripData-city">
            {booking.fromCityName} - {booking.toCityName}
          </p>
          <p>{getFormattedDate(booking.departureTime)}</p>
          <p>Departure time: {getFormattedTime(booking.departureTime)}</p>
        </div>

        <div className="booking__box">
          <span>Total passenger: {passengers.length}</span>
          <div>
            {passengers?.map((p) => (
              <p key={p.name}>
                {p.name}, {p.age}
              </p>
            ))}
          </div>
        </div>
        <div className="booking__box">
          <span>Baggage options</span>
          <div>
            <p>2 x big</p>
            <p>1 x carry-on</p>
            <p>- | +</p>
          </div>
        </div>

        <div className="booking__help">
          <p>Need help? Call us at 098/000-000-000. We are her for you 24/7!</p>
        </div>
      </div>

      <div className="booking__right">
        <div className="booking__info">
          <p>Booking Ref: XYZ123 </p>
          <span>{booking.status}</span>
        </div>

        <div className="booking__priceBreakdown">
          <span>Price breakdown</span>
          <div className="booking__data">
            <p>Prize per booking: 230 EUR</p>
            <p>
              Total price: {booking.totalPrice} {booking.currency}
            </p>
            <p>No discount used</p>
          </div>
        </div>

        <div className="booking__actions">
          <a className="btn" href={instance.url} download="BoardingPass.pdf">
            Download boarding pass
          </a>
          <button className="btn">Cancel booking</button>
        </div>
      </div>
    </figure>
  );
}

export default BookingDetails;
