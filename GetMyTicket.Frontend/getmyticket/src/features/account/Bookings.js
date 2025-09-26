import { formatDate, getFormattedBookingDate } from "../../helpers";
import { useState, useEffect } from "react";
import FilterBy from "../../ui/FilterBy.js";
import SortBy from "../../ui/SortBy.js";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useAccountContext } from "./AccountContext.js";
import { useCancelBooking } from "../../services/bookingService.js";

const FILTER_PARAM = "bookingStatus";

const options = [
  { key: "All", value: "All" },
  { key: "Confirmed", value: "Confirmed" },
  { key: "Canceled", value: "Canceled" },
];

const sortOptions = [
  { key: "Booking date DESC", value: "bookingDateDesc" },
  { key: "Booking date ASC", value: "bookingDateAsc" },
];

function Bookings() {
  const navigate = useNavigate();
  const { mutate: cancelBooking } = useCancelBooking();
  const { bookings } = useAccountContext();

  const [filterValue, setFilterValue] = useState(options[0].value);
  const [sortValue, setSortValue] = useState(sortOptions[0].value);

  const [sortedAndFilteredData, setSortedAndFilteredData] = useState([]);

  const [searchParams, setSearchParams] = useSearchParams();

  let updatedData = [...bookings];

  useEffect(() => {
    //set default search params
    searchParams.set("bookingStatus", "All");
    searchParams.set("sortBy", sortOptions[0].value);
    setSearchParams(searchParams);
    setSortedAndFilteredData(...bookings);
  }, []);

  useEffect(() => {
    //////FILTER
    if (sortValue === sortOptions[1].value) {
      //asc
      updatedData = updatedData.sort(
        (a, b) => new Date(a.bookingDate) - new Date(b.bookingDate)
      );
    } else {
      //desc
      updatedData = updatedData.sort(
        (a, b) => new Date(b.bookingDate) - new Date(a.bookingDate)
      );
    }

    if (filterValue !== "All") {
      updatedData = updatedData.filter((x) => x.status === filterValue);
    }
    setSortedAndFilteredData(updatedData);

    searchParams.set("bookingStatus", filterValue);
    searchParams.set("sortBy", sortValue);
    setSearchParams(searchParams);
  }, [
    filterValue,
    setFilterValue,
    sortValue,
    setSortValue,
    searchParams,
    bookings,
  ]);

  function handleCancelBooking(id) {
    const confirmed = window.confirm(
      "Do you really want to cancel this booking?"
    );
    if (confirmed) {
      cancelBooking(id);
    }
    //const updatedBookings = bookings.map((booking) =>
    //  booking.bookingId === id ? { ...booking, status: "Canceled" } : booking
    //);

    //const filterByParam = searchParams.get(FILTER_PARAM);
    //let updatedFilteredData = [...updatedBookings];
    //updatedFilteredData = updatedFilteredData.filter(
    //  (b) => b.status === filterByParam
    //);

    //setFilteredData(updatedFilteredData);
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
          <SortBy
            sortOptions={sortOptions}
            sortValue={sortValue}
            setSortValue={setSortValue}
          />
        </div>

        <div className="bookings__tableWrapper">
          <table>
            {!sortedAndFilteredData || sortedAndFilteredData.length === 0 ? (
              <thead>
                <tr>
                  <th className="account__noData">No bookings</th>
                </tr>
              </thead>
            ) : (
              <tbody>
                <tr>
                  <th>Booking reference</th>
                  <th>From</th>
                  <th>To</th>
                  <th>Departure Time</th>
                  <th>Total Price</th>
                  <th>Status</th>
                  <th>Booking Date</th>
                  <th>Actions</th>
                </tr>
              </tbody>
            )}

            <tbody>
              {sortedAndFilteredData?.map((b) => (
                <tr key={b?.bookingId}>
                  <td>{b?.bookingId}</td>
                  <td>{b?.fromCityName}</td>
                  <td>{b?.toCityName}</td>
                  <td>{formatDate(b.departureTime)}</td>
                  <td>
                    {b?.totalPrice} {b?.currency}
                  </td>
                  <td>{b?.status}</td>
                  <td>{getFormattedBookingDate(b?.bookingDate)}</td>
                  <td className="bookings__actions">
                    {b?.status === "Confirmed" ? (
                      <>
                        <button
                          className="btn btn--destructive"
                          onClick={() => handleCancelBooking(b?.bookingId)}
                        >
                          Cancel
                        </button>
                        <button
                          className="btn"
                          onClick={() =>
                            navigate(`/account/bookings/${b?.bookingId}`)
                          }
                        >
                          Details
                        </button>
                      </>
                    ) : null}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}

export default Bookings;
