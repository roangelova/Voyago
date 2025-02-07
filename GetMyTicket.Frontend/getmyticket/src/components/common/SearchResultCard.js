import transavia_logo from '../../assets/images/transavia_logo.PNG'

function SearchResultCard({ trip }) {

    return (
        <div className="resultCard">
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
                <span>{trip.price} BGN</span>
            </div>
        </div>
    )
}

export default SearchResultCard;