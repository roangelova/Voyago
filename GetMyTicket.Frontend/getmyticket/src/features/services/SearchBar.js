import { useGetCities } from "../../services/cityService";
import { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { Trips } from "../../services/tripService";

import { useNavigate, useParams } from "react-router-dom";
import Spinner from "../../ui/Spinner";

const SearchBar = ({ LoginPopupVisibility }) => {
    const navigate = useNavigate();

    const [start, setStart] = useState('');
    const [destination, setDestination] = useState('');
    const [startDate, setStartDate] = useState('2025-05-10');
    const [endDate, setEndDate] = useState('2025-05-18');

    let params = useParams();
    let searchType = params.searchType;

    searchType === 'flights' ? searchType = 'Airplane'
        : searchType === 'trains' ? searchType = 'Train'
            : searchType == 'buses' ? searchType = 'Bus'
                : searchType = 'all'

    const { cities, error, isPending } = useGetCities();

    useEffect(() => {
        if (cities?.length > 0) {
            setStart(cities[0].cityId);
            setDestination(cities[1].cityId);
        }
    }, [cities]);

    if (error) toast.error("Failed to get cities.");

    const handleSearch = () => {
        Trips.executeFilter({
            "type": searchType,
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

    const availableCitiesForStart = cities?.filter(city => city.cityId !== destination);
    const availableCitiesForDestination = cities?.filter(city => city.cityId !== start);

    if (isPending) return <Spinner />

    return (
        <div className={`searchBar__container ${LoginPopupVisibility ? 'blurred' : ''}`}>
            <div className='searchBar__container--element'>
                <select name="start" value={start} onChange={(e) => setStart(e.target.value)}>
                    {availableCitiesForStart?.map((city) => (
                        <option key={city.cityId} value={city.cityId}>
                            {city.cityName}
                        </option>
                    ))}
                </select>
            </div>

            <div className='searchBar__container--element'>
                <select name="destination" value={destination} onChange={(e) => setDestination(e.target.value)}>
                    {availableCitiesForDestination?.map((city) => (
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