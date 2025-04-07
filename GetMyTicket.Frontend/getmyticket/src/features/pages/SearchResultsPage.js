import { useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import SearchResultCard from "../common/SearchResultCard";

import { MapContainer, TileLayer, Marker } from 'react-leaflet'

function SearchResultsPage() {
    const location = useLocation();
    const data = location.state;

    const defaultMapCoordinates = [48.1351, 11.5820];
    const [startCoordinates, setStartCoordinates] = useState(null);
    const [destinationCoordinates, setDestinationCoordinates] = useState(null)

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

                    {!data || data.length < 0?
                        <h3>We could not find any offers for this search. Please, try again.</h3>
                        : null}

                    {data?.map((trip) => <SearchResultCard trip={trip} key={trip.tripId} />)}

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
        </>
    )
}

export default SearchResultsPage;