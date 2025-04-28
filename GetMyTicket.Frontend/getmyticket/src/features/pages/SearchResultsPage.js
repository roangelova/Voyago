import { useLocation, useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";
import SearchResultCard from "../common/SearchResultCard";

import { MapContainer, TileLayer, Marker } from 'react-leaflet'
import Pagination from "../common/Paginations";
import FilterBy from "../common/FilterBy";
import SortBy from "../common/SortBy";

const sortOptions = [
    { key: 'Lowest price', value: 'lowestPriceFirst' },
    { key: 'Highest price', value: 'highestPriceFirst' },
    { key: 'Travel time', value: 'travelTime' }];

function SearchResultsPage() {
    const location = useLocation();
    const [data, setData] = useState([]);
    const [filteredData, setFilteredData] = useState([]);

    useEffect(() => {
        if (location.state) {
            setData(location.state);
            setFilteredData(location.state); // Start with unfiltered data
        }
    }, [location.state]);


    const defaultMapCoordinates = [48.1351, 11.5820];
    const [startCoordinates, setStartCoordinates] = useState(null);
    const [destinationCoordinates, setDestinationCoordinates] = useState(null)

    const [searchParams, setSearchParams] = useSearchParams();

    let updatedData = [...data];

    useEffect(() => {
        //////FILTER
        const filterByParam = searchParams.get('provider');
        if (filterByParam !== 'all') {
            updatedData = updatedData.filter(x => x.transportationProviderName === filterByParam)
        }
        
        //////SORT
        const sortByValue = searchParams.get('sortBy');
        
        if (sortByValue === 'travelTime') {
            updatedData?.sort((a, b) => {
                const travelTimeA = new Date(a.endTime) - new Date(a.startTime);
                const travelTimeB = new Date(b.endTime) - new Date(b.startTime);
                return travelTimeA - travelTimeB;
            })
        } else if (sortByValue === 'highestPriceFirst') {
            updatedData?.sort((a, b) => b.price - a.price)
        } else if (sortByValue === 'lowestPriceFirst') {
            updatedData?.sort((a, b) => a.price - b.price)
        }

        //////PAGINATION
        let pageSize = searchParams.get('results');
        let page = searchParams.get('page');
        updatedData = updatedData.slice((page - 1) * pageSize, page * pageSize);
        
        //end of sorting. pagination and filtering
        setFilteredData(updatedData)
    }, [searchParams, data])

    //The following effect fetches the longitute and latitude for the trip start and destination. The, the coordinates are used to set markers on the map.
    //Good to know: we make a call to an external API, which allows us up to 1000 calls per month free of charge. 
    useEffect(() => {
        if (data && data.length > 0) {
            //MONTHLY QUOATA EXCEEDED

            //   CityApi.getCityData(data[0].startCityName)
            //       .then(data => setStartCoordinates([data[0].latitude, data[0].longitude]))
            //       .catch(err => {
            //           toast.error(err.error)
            //       })

            //   CityApi.getCityData(data[0].endCityName)
            //       .then(data => setDestinationCoordinates([data[0].latitude, data[0].longitude]))
            //       .catch(err => {
            //           toast.error(err.error)
            //       })
        }
    })
    return (
        <>

            <section className="search__container">
                <div className="search__results">
                    <div className="search__results--options">
                        <FilterBy />
                        <SortBy sortOptions={sortOptions} />
                    </div>

                    {!filteredData || filteredData.length <= 0 ?
                        <h3>We could not find any offers for this search. Please, try again.</h3>
                        : null}

                    {filteredData?.map((trip) => <SearchResultCard trip={trip} key={trip.tripId + Math.random()} />)}


                </div>

                <div className='search__container-map'>
                    <MapContainer center={defaultMapCoordinates} style={{ height: "100%", width: "100%" }} zoom={5} scrollWheelZoom={true}>
                        <TileLayer
                            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />

                        {startCoordinates && <Marker position={startCoordinates}></Marker>}
                        {destinationCoordinates && <Marker position={destinationCoordinates}></Marker>}

                    </MapContainer>

                </div>

            </section>
            <Pagination totalItemsCount={data?.length} />
        </>
    )
}

export default SearchResultsPage;