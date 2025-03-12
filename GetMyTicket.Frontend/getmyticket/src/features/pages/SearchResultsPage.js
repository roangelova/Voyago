import { useLocation } from "react-router-dom";

import Footer from "../common/Footer";
import NavBar from "../common/NavBar";
import SearchResultCard from "../common/SearchResultCard";

import { MapContainer, TileLayer, Marker } from 'react-leaflet'

function SearchResultsPage() {
    const location = useLocation();
    const data = location.state;

    const Munich = [48.1351, 11.5820];
    const Varna = [43.2142, 27.9147]

    //TODO -> FETCH REAL CITY LNG AND LAT 

    return (
        <>
            <NavBar className="search__nav" handleLoginToggle={null} />
            <section className="search__container">

                <div className="search__results">

                    {data?.map((trip) => <SearchResultCard trip={trip} key={trip.tripId} />)}
                    {data?.map((trip) => <SearchResultCard trip={trip} key={1} />)}
                    {data?.map((trip) => <SearchResultCard trip={trip} key={2} />)}
                    {data?.map((trip) => <SearchResultCard trip={trip} key={3} />)}


                </div>

                <div className='search__container-map'>
                    <MapContainer center={Munich} style={{ height: "100%", width: "100%" }} zoom={5} scrollWheelZoom={true}>
                        <TileLayer
                            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                        <Marker position={Varna}>
                        </Marker>

                        <Marker position={Munich}></Marker>
                    </MapContainer>

                </div>

            </section>
            <Footer />
        </>
    )
}

export default SearchResultsPage;