import { useLocation } from "react-router-dom";
import { useEffect, useState } from "react";

import { toast } from "react-toastify";

import Footer from "../common/Footer";
import NavBar from "../common/NavBar";
import SearchResultCard from "../common/SearchResultCard";

import { MapContainer, TileLayer, Marker } from 'react-leaflet'

import { CityApi } from "../../services/cityInfoAPI";

function SearchResultsPage() {
    const location = useLocation();
    const data = location.state;

    const defaultMapCoordinates = [48.1351, 11.5820];
    const [startCoordinates, setStartCoordinates] = useState([53.5488,9.9872]);
    const [destinationCoordinates, setDestinationCoordinates] = useState([50.1109,8.6821])

    useEffect(() => {
        if (data.length > 0) {
            CityApi.getCityData(data[0].startCityName)
                .then(data => setStartCoordinates([data[0].latitude, data[0].longitude]))
                .catch(err => {
                    toast.error(err.error)
                })

            CityApi.getCityData(data[0].endCityName)
                .then(data => setDestinationCoordinates([data[0].latitude, data[0].longitude]))
                .catch(err => {
                    toast.error(err.error)
                })
        }
    })

    return (
        <>
            <NavBar className="search__nav" handleLoginToggle={null} />
            <section className="search__container">

                <div className="search__results">

                    {data.length === 0 ?
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
            <Footer />
        </>
    )
}

export default SearchResultsPage;