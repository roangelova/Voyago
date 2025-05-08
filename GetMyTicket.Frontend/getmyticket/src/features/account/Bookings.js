import { formatDate } from "../../helpers";
import { useState, useEffect, use } from "react";
import { Booking, useGetUserBookings } from "../../services/bookingService";
import FilterBy from "../../ui/FilterBy.js";
import { useNavigate, useSearchParams } from "react-router-dom";
import { toast } from "react-toastify";

const FILTER_PARAM = 'bookingStatus';

const options = [
    { key: 'All', value: 'All' },
    { key: 'Confirmed', value: 'Confirmed' },
    { key: 'Canceled', value: 'Canceled' },
]

function Bookings() {
    const navigate= useNavigate();

    const userId = sessionStorage.getItem('userId');
    const { isPending, error, bookings = [] } = useGetUserBookings(userId);
    const [filteredData, setFilteredData] = useState([]);


    const [searchParams, setSearchParams] = useSearchParams();

    let updatedData = [...bookings];

    useEffect(() => {
        //set default search params
        searchParams.set('bookingStatus', 'All');
        setSearchParams(searchParams)
    }, [])

    useEffect(() => {
        //////FILTER
        const filterByParam = searchParams.get(FILTER_PARAM);
        if (filterByParam !== 'All') {
            updatedData = updatedData.filter(x => x.status === filterByParam)
        }

        setFilteredData(updatedData)
    }, [searchParams, bookings])

    function handleCancelBooking(id) {

        const confirm = window.confirm('Do you really want to cancel this booking?')

        if (confirm) {
            Booking.cancelBooking(id)
                .then(() => {
                    toast.success('Booking was canceled successfully!');

                    const updatedBookings = bookings.map(booking =>
                        booking.bookingId === id
                            ? { ...booking, status: 'Canceled' }
                            : booking
                    );

                    const filterByParam = searchParams.get(FILTER_PARAM);
                    let updatedFilteredData = [...updatedBookings];
                    updatedFilteredData = updatedFilteredData.filter(b => b.status === filterByParam)

                    setFilteredData(updatedFilteredData);
                })
                .catch(err => {
                    toast.error(err)
                })
        } else {
            return;
        }
    }

    return (
        <>
            <title>Bookings | Voyago</title>
            <div className="bookings__container">
                <div className="bookings__options">
                    <FilterBy
                        title="Status"
                        options={options}
                        paramName={FILTER_PARAM}
                    />
                </div>
                <table>
                    <thead>

                        {!filteredData || filteredData.length === 0 ? <tr ><th className="bookings__noData">No bookings</th></tr> :
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

                        {filteredData?.map(b =>
                            <tr key={b.bookingId}>
                                <td>{b.bookingId}</td>
                                <td>{b.toCityName}</td>
                                <td>{b.fromCityName}</td>
                                <td>{formatDate(b.departureTime)}</td>
                                <td>{b.totalPrice} {b.currency}</td>
                                <td>{b.status}</td>
                                <td className="bookings__actions">
                                    {b.status === 'Confirmed' ? <div onClick={() => handleCancelBooking(b.bookingId)}>Cancel |</div> : null}
                                    <div onClick={() => navigate(`/account/bookings/${b.bookingId}`, {state: b})}> Manage booking</div>
                                </td>
                            </tr>
                        )}

                    </tbody>

                </table>
            </div>
        </>
    );
}

export default Bookings;