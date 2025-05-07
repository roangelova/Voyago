import { formatDate } from "../../helpers";
import { useGetUserBookings } from "../../services/bookingService";

function Bookings() {
    const userId = sessionStorage.getItem('userId');
    const { isPending, error, bookings } = useGetUserBookings(userId);

    return (

        //TTODO: ADD FILTER BY BOOKINGSTATUS

        <div className="bookings__container">
            <table>
                <thead>

                    {!bookings || bookings.length === 0 ? <tr ><th className="bookings__noData">No bookings</th></tr> :
                        <tr>
                            <th>Booking reference</th>
                            <th>From</th>
                            <th>To</th>
                            <th>Departure Time</th>
                            <th>Total Price</th>
                            <th>Status</th>
                            <th>Actions</th>
                        </tr>}
                </thead>

                <tbody>

                    {bookings?.map(b =>
                        <tr key={b.bookingId}>
                            <td>{b.bookingId}</td>
                            <td>{b.toCityName}</td>
                            <td>{b.fromCityName}</td>
                            <td>{formatDate(b.departureTime)}</td>
                            <td>{b.totalPrice} {b.currency}</td>
                            <td>{b.status}</td>
                            <td><strong>Cancel | Details</strong></td>
                        </tr>
                    )}

                </tbody>

            </table>
        </div>
    );
}

export default Bookings;