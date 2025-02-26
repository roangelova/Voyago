import { toast } from 'react-toastify';
import transavia_logo from '../../assets/images/transavia_logo.PNG'
import { useNavigate } from "react-router-dom";

function SearchResultCard({ trip }) {
const navigate = useNavigate();

    const handleBookTrip = () => {
        const userId = sessionStorage.getItem('userId');

        //TODO: DISABLE FOR DEVELOPMENT PURPOSES
       // if (!userId) {
       //     toast.error('Ugh oh.. Seems like you need to log in again, in order to book this trip.')
       //     return;
       // }

        navigate("/cart", { state: { trip: trip } });
    }

    return (
        <div className="resultCard" onClick={handleBookTrip}>
            <div>

                <div className="resultCard__provider">
                    <img className="resultCard__provider-logo" src={transavia_logo} alt="Transportation provider logo" />
                    <span>{trip.transportationProviderName}</span>
                </div>
                <div className='resultCard__trip'>
                    <span className="from">{trip.startCityName}</span>
                    <span className="arrow">  →  </span>
                    <span className="to">{trip.endCityName}</span>
                </div>
                <div className='connection'>
                    <p>Departure: <span >{new Date(trip.startTime).toLocaleString()}</span> </p>
                    <p> Arrival: <span>{new Date(trip.endTime).toLocaleString()}</span></p>
                </div>


            </div>
            <div className='resultCard__price'>
                <span>{trip.price} {trip.currency}</span>
            </div>
        </div>
    )
}

export default SearchResultCard;