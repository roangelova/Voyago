import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";

function SearchResultCard({ trip }) {
    const navigate = useNavigate();

    const handleBookTrip = () => {
        const userId = sessionStorage.getItem('userId');

        if (!userId) {
            toast.error('Ugh oh.. Seems like you need to log in again, in order to book this trip.')
            return;
        }

        navigate("/cart", { state: { trip: trip } });
    }

    //TODO -> add support for other data types not just PNG

    return (
        <div className="resultCard" onClick={handleBookTrip}>
            <div>

                <div className="resultCard__provider">
                    <img className="resultCard__provider-logo" src={`data:image/png;base64,${trip.transportationProviderLogo}`} alt="Transportation provider logo" />
                    <span>{trip.transportationProviderName}</span>
                </div>
                <div className='resultCard__trip'>
                    <span className="from">{trip.startCityName}</span>
                    <span className="arrow">  →  </span>
                    <span className="to">{trip.endCityName}</span>
                </div>
                <div className='connection'>
                    <p><b>Departure:</b> <span >{new Date(trip.startTime).toLocaleString()}</span> </p>
                    <p><b>Arrival: </b><span>{new Date(trip.endTime).toLocaleString()}</span></p>
                </div>


            </div>
            <div className='resultCard__price'>
                <span>{trip.price} {trip.currency}</span>
            </div>
        </div>
    )
}

export default SearchResultCard;