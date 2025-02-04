import transavia_logo from '../../assets/images/transavia_logo.PNG'

function SearchResultCard() {

    return (
        <div className="resultCard">
            <div>

                <div className="resultCard__provider">
                    <img className="resultCard__provider-logo" src={transavia_logo} alt="Transportation provider logo" />
                    <span>TransAvia</span>
                </div>
                <div className='resultCard__trip'>
                    <span class="from">Varna</span>
                    <span class="arrow">  →  </span>
                    <span class="to">Munich</span>
                </div>
                <div className='connection'>
                    <p>Departure: <span >10:30</span> </p>
                    <p> Arrival: <span>12:40</span></p>
                </div>


            </div>
            <div className='resultCard__price'>
                <span>359 BGN</span>
            </div>
        </div>
    )
}

export default SearchResultCard;