import Footer from "../common/Footer";
import NavBar from "../common/NavBar";
import SearchResultCard from "../common/SearchResultCard";

import munich from '../../assets/images/destinations/munich.jpg';

function SearchResultsPage() {

    return (
        <>
            <NavBar className="search__nav" handleLoginToggle={null} />
            <section className="search__container">

                <div className="search__results">
                    <SearchResultCard />
                </div>
                <figure >
                    <img className="search__image" src={munich} alt="destination image" />
                </figure>

            </section>
            <Footer />
        </>
    )
}

export default SearchResultsPage;