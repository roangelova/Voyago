import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

import fallbackLogo from "../../assets/images/fallbackLogo.png";
import bistro from "../../assets/icons/bistro.png";
import wc from "../../assets/icons/wc.png";
import highspeed from "../../assets/icons/highspeed.png";
import { formatDate } from "../../helpers";

function SearchResultCard({ trip, passengers }) {
  const navigate = useNavigate();

  const handleBookTrip = () => {
    const userId = sessionStorage.getItem("userId");

    if (!userId) {
      toast.info(
        "Please log in to continue with the booking process."
      );
      return;
    }

    navigate("/cart", { state: { trip: trip, passengers: passengers } });
  };

  //TODO -> add support for other data types not just PNG
  


  let imageSrc;
  trip.transportationProviderLogo !== ""
    ? (imageSrc = `data:image/png;base64,${trip.transportationProviderLogo}`)
    : (imageSrc = fallbackLogo);

  return (
    <div className="resultCard" onClick={handleBookTrip}>
      <div>
        <div className="resultCard__provider">
          <img
            className="resultCard__provider-logo"
            src={imageSrc}
            alt="Transportation provider logo"
          />
          <span>{trip.transportationProviderName}</span>
        </div>
        <div className="resultCard__trip">
          <span className="from">{trip.startCityName}</span>
          <span className="arrow"> → </span>
          <span className="to">{trip.endCityName}</span>
        </div>
        <div className="resultCard__connection">
          <p>
            <b>Departure:</b> <span>{formatDate(trip.startTime)}</span>{" "}
          </p>
          <p>
            <b>Arrival: </b>
            <span>{formatDate(trip.endTime)}</span>
          </p>
        </div>

        <div className="resultCard__info">{getPriceNoteString(passengers)}</div>

        <div className="resultCard__amenities">
          <img src={bistro} alt="Bistro on board" />
          <img src={highspeed} alt="Highspeed connection" />
          <img src={wc} alt="WC on board" />
        </div>
      </div>
      <div className="resultCard__priceAndType">
        <span className="resultCard__priceAndType--price">
          {calculateTotalPrice(trip, passengers)} {trip.currency}
        </span>
        <span className="resultCard__priceAndType--typeTag">
          {trip.typeOfTrip}
        </span>
      </div>
    </div>
  );
}

export default SearchResultCard;

function calculateTotalPrice(trip, passengers) {
  return (
    passengers.adults * trip.adultPrice +
    passengers.children * trip.childrenPrice
  );
}

function getPriceNoteString(passengers){
    let string = [];

    string.push(passengers?.adults > 1 ? `* Price for ${passengers?.adults} adults` : '* Price for 1 adult');
    if(passengers?.children > 0) string.push(passengers?.children > 1 ? `${passengers?.children} children` : '1 child');
    if(passengers?.infants > 0) string.push(passengers?.infants > 1 ? `${passengers?.infants} infants` : '1 infant');

    return string.join(', ');
}
