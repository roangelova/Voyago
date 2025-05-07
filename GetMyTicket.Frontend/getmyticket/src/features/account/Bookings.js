import { formatDate } from "../../helpers";
import { useState, useEffect, use } from "react";
import { useGetUserBookings } from "../../services/bookingService";
import FilterBy from "../common/FilterBy";
import { useSearchParams } from "react-router-dom";

const FILTER_PARAM = 'bookingStatus';

const options = [
    { key: 'Confirmed', value: 'Confirmed' },
    { key: 'Canceled', value: 'Canceled' },
]

function Bookings() {
    const userId = sessionStorage.getItem('userId');
    const { isPending, error, bookings = [] } = useGetUserBookings(userId);
    const [filteredData, setFilteredData] = useState([]);


    const [searchParams, setSearchParams] = useSearchParams();

    let updatedData = [...bookings];

    useEffect(() => {
        //set defaulot search params
        searchParams.set('bookingStatus', 'Confirmed');
        setSearchParams(searchParams)
    }, [])

    useEffect(() => {
        //////FILTER
        const filterByParam = searchParams.get(FILTER_PARAM);
        console.log(filterByParam)
        if (filterByParam !== 'all') {
            updatedData = updatedData.filter(x => x.status === filterByParam)
        }

        setFilteredData(updatedData)
    }, [searchParams, bookings])

    return (

        <div className="bookings__container">
            <div className="bookings__options">
                <FilterBy
                    title="Status"
                    options={options}
                    param={FILTER_PARAM}
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
                            <td className="bookings__actions">{b.status === 'Confirmed' ? 'Cancel |': null } Details</td>
                        </tr>
                    )}

                </tbody>

            </table>
        </div>
    );
}

export default Bookings;