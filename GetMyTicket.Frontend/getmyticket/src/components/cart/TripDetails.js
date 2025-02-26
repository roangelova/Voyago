import munich from '../../assets/images/destinations/munich.jpg'

function TripDetails({trip}){
    if (!trip) return <h2>No trip</h2>;

    return (
      <div className="trip-details">
        <img src={munich} alt="Trip" className="trip-details__image" />
        <div className="trip-details__data">
          <h3>{trip.startCityName} → {trip.endCityName}</h3>
          <p><strong>Date:</strong> {new Date(trip.startTime).toLocaleDateString()}</p>
          <p><strong>Time:</strong> {new Date(trip.startTime).toLocaleTimeString()} - {new Date(trip.endTime).toLocaleTimeString()}</p>
          <p><strong>Price:</strong> $ {trip.price}</p>
          <p><strong>Number of seats purchased:</strong> 1</p>
          <p><strong>Provider:</strong> {trip.transportationProviderName}</p>
        </div>
      </div>
    );
}

export default TripDetails;