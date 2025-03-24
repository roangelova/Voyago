import { Cities } from "../../services/cityService";
import { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { Trips } from "../../services/tripService";

import { useNavigate } from "react-router-dom";

const SEARCH_BY_TYPE = 'all'; //possible values: ALL, AIRPLANE, BUS, TRAIN

const SearchBar = ({ LoginPopupVisibility }) => {
//TODO -> include countries AND STARTCITY SHOULD NOT BE SHOWN IN DESTINATION LIST
    const navigate = useNavigate();

    const [cities, setCities] = useState([]);
    const [start, setStart] = useState('');
    const [destination, setDestination] = useState('');
    const [startDate, setStartDate] = useState('2025-05-10');
    const [endDate, setEndDate] = useState('2025-05-18');

    useEffect(() => {
        Cities.getAll().then(data => {
            if (data.length > 0) {
                setCities(data);
                setStart(data[0].cityId);
                setDestination(data[1].cityId)
            }
        })
            .catch(err => toast.error("Failed to get cities."));
    }, [])

    const handleSearch = () => {
        Trips.executeFilter({
            "type": SEARCH_BY_TYPE,
            "startDate": startDate,
            "endDate": endDate,
            "startCityId": start,
            "endCityId": destination
        }).then(data => {
            navigate("/search-results", { state: data })
        }).catch(err => {
            toast.error('Oops! We could not look up any trips right now.')
        })
    }

    return (
        <div className={`searchBar__container ${LoginPopupVisibility ? 'blurred' : ''}`}>
            <div className='searchBar__container--element'>
                <select name="start" value={start} onChange={(e) => setStart(e.target.value)}>
                    {cities.map((city) => (
                        <option key={city.cityId} value={city.cityId}>
                            {city.cityName}
                        </option>
                    ))}
                </select>
            </div>

            <div className='searchBar__container--element'>
                <select name="destination" value={destination} onChange={(e) => setDestination(e.target.value)}>
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
                    value={startDate}
                    onChange={(e) => setStartDate(e.target.value)}
                >
                </input>
            </div>

            <div className='searchBar__container--element'>
                <input
                    name="endDate"
                    type="date"
                    value={endDate}
                    onChange={(e) => setEndDate(e.target.value)}
                >
                </input>
            </div>

            <div className='searchBar__container--element'>
                <button onClick={handleSearch} className='searchBar__button'>
                    Search
                </button>
            </div>

        </div>
    )

}

export default SearchBar;