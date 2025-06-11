import { formatDate } from "../../helpers";
import { useState, useEffect } from "react";
import { CancelBooking } from "../../services/bookingService";
import FilterBy from "../../ui/FilterBy.js";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useAccountContext } from "./AccountContext.js";

const FILTER_PARAM = "bookingStatus";

const options = [
  { key: "All", value: "All" },
  { key: "Confirmed", value: "Confirmed" },
  { key: "Canceled", value: "Canceled" },
];

function Bookings() {
  const navigate = useNavigate();

  const { bookings } = useAccountContext();

  const [filteredData, setFilteredData] = useState([]);
  const [filterValue, setFilterValue] = useState(options[0].value);
  const [searchParams, setSearchParams] = useSearchParams();

  let updatedData = [...bookings];

  useEffect(() => {
    //set default search params
    searchParams.set("bookingStatus", "All");
    setSearchParams(searchParams);
    setFilteredData(...bookings);
  }, []);

  useEffect(() => {
    //////FILTER
    if (filterValue !== "All") {
      updatedData = updatedData.filter((x) => x.status === filterValue);
    }
    setFilteredData(updatedData);

    searchParams.set("bookingStatus", filterValue);
    setSearchParams(searchParams);
  }, [filterValue, setFilterValue, bookings]);

  function handleCancelBooking(id) {
    let result = CancelBooking(id);

    if (result) {
      const updatedBookings = bookings.map((booking) =>
        booking.bookingId === id ? { ...booking, status: "Canceled" } : booking
      );

      const filterByParam = searchParams.get(FILTER_PARAM);
      let updatedFilteredData = [...updatedBookings];
      updatedFilteredData = updatedFilteredData.filter(
        (b) => b.status === filterByParam
      );

      setFilteredData(updatedFilteredData);
    }
  }

  //TODO -> ADD BOOKING REFERENCES AS WELL TO THE DB AND USE IT HERE

  return (
    <>
      <title>Bookings | Voyago</title>
      <div className="bookings__container">
        <div className="bookings__options">
          <FilterBy
            title="Status"
            options={options}
            paramName={FILTER_PARAM}
            filterValue={filterValue}
            setFilterValue={setFilterValue}
          />
        </div>
        <table>
          <thead>
            {!filteredData || filteredData.length === 0 ? (
              <tr>
                <th className="bookings__noData">No bookings</th>
              </tr>
            ) : (
              <tr>
                <th>Booking reference</th>
                <th>From</th>
                <th>To</th>
                <th>Departure Time</th>
                <th>Total Price</th>
                <th>Status</th>
                <th>Actions</th>
              </tr>
            )}
          </thead>

          <tbody>
            {filteredData?.map((b) => (
              <tr key={b.bookingId}>
                <td>XYZ123</td>
                <td>{b.fromCityName}</td>
                <td>{b.toCityName}</td>
                <td>{formatDate(b.departureTime)}</td>
                <td>
                  {b.totalPrice} {b.currency}
                </td>
                <td>{b.status}</td>
                <td className="bookings__actions">
                  {b.status === "Confirmed" ? (
                    <div onClick={() => handleCancelBooking(b.bookingId)}>
                      Cancel |
                    </div>
                  ) : null}
                  <div
                    onClick={() =>
                      navigate(`/account/bookings/${b.bookingId}`, { state: b })
                    }
                  >
                    Manage booking
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default Bookings;
