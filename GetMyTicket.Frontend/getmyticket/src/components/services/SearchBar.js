import { Cities } from "../../services/cityService";
import { useState, useEffect } from "react";
import { toast, ToastContainer } from "react-toastify";
import { Trips } from "../../services/tripService";

const SearchBar = () => {

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



    const handleLogin = () => {
        //TODO IMPLEMENT
        Trips.executeFilter({
            "startDate": startDate,
            "endDate": endDate,
            "startCityId": start,
            "endCityId": destination
        }).then(data => {
            console.log(data)
        }).catch(err => {
            toast.error('You fucked this up!')
        })
    }

    return (
        <div className="searchBar__container">
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
                <button onClick={handleLogin} className='searchBar__button'>
                    Search
                </button>
            </div>

        </div>
    )

}

export default SearchBar;