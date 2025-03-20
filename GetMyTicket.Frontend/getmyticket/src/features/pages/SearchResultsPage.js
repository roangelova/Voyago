import { useLocation } from "react-router-dom";
import { useEffect, useState } from "react";

import Footer from "../common/Footer";
import NavBar from "../common/NavBar";
import SearchResultCard from "../common/SearchResultCard";

import { MapContainer, TileLayer, Marker } from 'react-leaflet'

import { CityApi } from "../../services/cityInfoAPI";

function SearchResultsPage() {
    const location = useLocation();
    const data = location.state;

    const [startCordinates, setStartCordinates] = useState([48.1351, 11.5820]);
    const [destinationCordinates, setDestinationCordinates] = useState([43.2142, 27.9147])

    //TODO -> FETCH REAL CITY LNG AND LAT 

    //MONTLY QUOTA EXCEEDED
    //useEffect(() => {
    //    CityApi.getCityData(data[0].startCityName)
    //        .then(data => setStartCordinates([data[0].latitude, data[0].longitude]))
    //
    //
    //    CityApi.getCityData(data[0].endCityName)
    //        .then(data => setDestinationCordinates([data[0].latitude, data[0].longitude]))
    //})

    return (
        <>
            <NavBar className="search__nav" handleLoginToggle={null} />
            <section className="search__container">

                <div className="search__results">

                    {data?.map((trip) => <SearchResultCard trip={trip} key={trip.tripId} />)}
                    {data?.map((trip) => <SearchResultCard trip={trip} key={1} />)}


                </div>

                <div className='search__container-map'>
                    <MapContainer center={startCordinates} style={{ height: "100%", width: "100%" }} zoom={5} scrollWheelZoom={true}>
                        <TileLayer
                            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                        <Marker position={startCordinates}></Marker>

                        <Marker position={destinationCordinates}></Marker>
                    </MapContainer>

                </div>

            </section>
            <Footer />
        </>
    )
}

export default SearchResultsPage;