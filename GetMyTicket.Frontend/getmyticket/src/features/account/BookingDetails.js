import { useLocation } from "react-router-dom";
import { formatDate, getFormattedDate, getFormattedTime } from "../../helpers";

function BookingDetails() {
    const { state: booking } = useLocation();

console.log(booking)

    return (
        <div className="bookingDetails__container">
            <h2>Your booking {booking.bookingId}</h2>
            <div className="bookingDetails__box">
                <h3>Trip details</h3>
                <div className="bookingDetails__data">
                    <p>{booking.fromCityName} to {booking.toCityName}</p>
                    <p>{getFormattedDate(booking.departureTime)}</p>
                    <p>Departure time: {getFormattedTime(booking.departureTime)}</p>
                    <p>Status: {booking.status}</p>
                </div>
            </div>
            <div className="bookingDetails__box">
                <h3>Total passenger: 3</h3>
                <div className="bookingDetails__data">
                    <p>Sara Smith, age 34</p>
                    <p>Sam Smith, age 36</p>
                    <p>Caroline Smith, age 2</p>
                </div>
            </div>
            <div className="bookingDetails__box">
                <h3>Baggage options</h3>
                <div className="bookingDetails__data">
                    <p>2 x big</p>
                    <p>1 x carry-on</p>
                    <p>- | +</p>
                </div>
            </div>
            <div className="bookingDetails__box">
                <h3>Price breakdown</h3>
                <div className="bookingDetails__data">
                    <p>Prize per booking: 230 EUR</p>
                    <p>Total price: {booking.totalPrice} {booking.currency}</p>
                    <p>Caroline Smith, age 1</p>
                    <p>No discount used</p>
                </div>
            </div>
            <div className="bookingDetails__actions">
                <button className="btn">Download boarding pass</button>
                <button className="btn destructiveBtn">Cancel booking</button>
            </div>


            <div className="passengerDetails__help">
                <p>Need help? Call us at 098/000-000-000. We are her for you 24/7!</p>
            </div>
        </div>
    );
}

export default BookingDetails;