import { Cities } from "../../services/cityService";
import { useState, useEffect } from "react";

const SearchBar = () => {

    const [cities, setCities] = useState([]);
    //CURRENT BASIC STRUCTURE: 
    //START
    //end
    //START DAY 
    //END DAY 
    useEffect(() => {
        Cities.getAll().then(data => {
            setCities(data);
        }
        );
    }, [])

    let [start, setStart] = useState();
    let [destination, setDestination] = useState('');






    return (
        <div className="searchBar__container">
            <div className='searchBar__container--element'>
                <select name="start">
                    {cities.map((city) => (
                        <option key={city.cityId} value={city.cityId}>
                            {city.cityName}
                        </option>
                    ))}
                </select>
            </div>

            <div className='searchBar__container--element'>
                <select name="destination">
                    {cities.map((city) => (
                        <option key={city.cityId} value={city.cityId}>
                            {city.cityName}
                        </option>
                    ))}
                </select>
            </div>

            <div className='searchBar__container--element'>
                <input
                    name="startDate"
                    type="date"
                    value='2025-05-10'
                >
                </input>
            </div>

            <div className='searchBar__container--element'>
                <input
                    name="endDate"
                    type="date"
                    value='2025-05-18'
                >
                </input>
            </div>

            <div className='searchBar__container--element'>
                <button className='searchBar__button'>
                    Search
                </button>
            </div>

        </div>
    )

}

export default SearchBar;