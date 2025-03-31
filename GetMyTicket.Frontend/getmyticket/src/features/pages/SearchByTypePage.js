import Footer from '../common/Footer';
import NavBar from '../common/NavBar';
import SearchBar from '../services/SearchBar';
import SearchResultsPage from './SearchResultsPage';

function SearchByTypePage() {
    return (
        <div className='search__container'>
            <SearchBar />
        </div>
    );
}

export default SearchByTypePage;