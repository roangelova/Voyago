import Footer from "../common/Footer";
import NavBar from "../common/NavBar";
import SearchResultCard from "../common/SearchResultCard";

import munich from '../../assets/images/destinations/munich.jpg';
import { useLocation } from "react-router-dom";

function SearchResultsPage() {
    const location = useLocation();
    const data = location.state;

    return (
        <>
            <NavBar className="search__nav" handleLoginToggle={null} />
            <section className="search__container">

                <div className="search__results">

                    {data?.map((trip) => <SearchResultCard trip={trip} key={trip.tripId} />)}

                </div>

                <div className='search__container-destinationCard'>
                    <div className="search__image">
                        <img src={munich} alt="destination image" />
                    </div>
                    <p> Munich, Bavaria’s capital, is home to centuries-old buildings and numerous museums. The city is known for its annual Oktoberfest celebration and its beer halls, including the famed Hofbräuhaus, founded in 1589. In the Altstadt (Old Town), central Marienplatz square contains landmarks such as Neo-Gothic Neues Rathaus (town hall), with a popular glockenspiel show that chimes and reenacts stories from the 16th century.</p>
                </div>

            </section>
            <Footer />
        </>
    )
}

export default SearchResultsPage;