import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";

import fallbackLogo from '../../assets/images/fallbackLogo.png'
import bistro from '../../assets/icons/bistro.png'
import wc from '../../assets/icons/wc.png'
import highspeed from '../../assets/icons/highspeed.png'

function formatDate(date) {
    return new Intl.DateTimeFormat("en-GB", {
        day: "numeric",
        month: "long",
        year: 'numeric',
        hour: "2-digit",
        minute: "2-digit",
        hourCycle: "h24"
    }).format(new Date(date));
}

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
    //TODO -> add vehicle amenities to Trip to display amenities

    let imageSrc;
    trip.transportationProviderLogo !== '' ? imageSrc = `data:image/png;base64,${trip.transportationProviderLogo}` :
        imageSrc = fallbackLogo;

    return (
        <div className="resultCard" onClick={handleBookTrip}>
            <div>

                <div className="resultCard__provider">
                    <img className="resultCard__provider-logo" src={imageSrc} alt="Transportation provider logo" />
                    <span>{trip.transportationProviderName}</span>
                </div>
                <div className='resultCard__trip'>
                    <span className="from">{trip.startCityName}</span>
                    <span className="arrow">  →  </span>
                    <span className="to">{trip.endCityName}</span>
                </div>
                <div className='resultCard__connection'>
                    <p><b>Departure:</b> <span >{formatDate(trip.startTime)}</span> </p>
                    <p><b>Arrival: </b><span>{formatDate(trip.endTime)}</span></p>
                </div>

                <div className='resultCard__amenities'>
                    <img src={bistro} alt='Bistro on board' />
                    <img src={highspeed} alt='Highspeed connection' />
                    <img src={wc} alt='WC on board' />
                </div>

            </div>
            <div className='resultCard__price'>
                <span>{trip.price} {trip.currency}</span>
            </div>


        </div>
    )
}

export default SearchResultCard;