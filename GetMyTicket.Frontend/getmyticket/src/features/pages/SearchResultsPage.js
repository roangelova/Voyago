import { useLocation, useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";
import SearchResultCard from "../common/SearchResultCard.js";

import { MapContainer, TileLayer, Marker } from "react-leaflet";
import Pagination from "../../ui/Paginations.js";
import FilterBy from "../../ui/FilterBy.js";
import SortBy from "../../ui/SortBy.js";
import { CityApi } from "../../services/cityInfoAPI.js";
import { toast } from "react-toastify";

//TODO - REFACTOR! TOO MUCH LOGIC IN THIS COMPONENT!

const FILTER_PARAM = "provider";

const resultsPerPageOptions = [5, 10, 15, 20];

const sortOptions = [
  { key: "Lowest price", value: "lowestPriceFirst" },
  { key: "Highest price", value: "highestPriceFirst" },
  { key: "Travel time", value: "travelTime" },
];

//TODO -> MAKE DYNAMIC; consider how key and value should be named
const filterOptions = [
  { key: "Show all", value: "all" },
  { key: "Avianca", value: "Avianca" },
  { key: "Deutsche Bahn", value: "DeutscheBahn" },
  { key: "TransAvia", value: "TransAvia" },
];

function SearchResultsPage() {
  const location = useLocation();
  const [data, setData] = useState([]);
  const [filteredData, setFilteredData] = useState([]);

  //FILTER BY, PAGINATION, SORTBY
  const [sortValue, setSortValue] = useState(sortOptions[0]?.value);
  const [filterValue, setFilterValue] = useState(filterOptions[0]?.value);
  const [resultsPerPage, setResultsPerPage] = useState(
    resultsPerPageOptions[0]
  );
  const [currentPage, setCurrentPage] = useState(1);

  const [searchParams, setSearchParams] = useSearchParams();

  //set default FILTERBY, SORTBY AND PAGINATION values; Start with default unfiltered data
  useEffect(() => {
    if (location.state) {
      setData(location.state);
      setFilteredData(location.state); // Start with unfiltered data
    }

    let newParams = new URLSearchParams(searchParams);
    newParams.set(FILTER_PARAM, filterValue);
    newParams.set("sortBy", sortValue);
    newParams.set("page", currentPage);
    newParams.set("results", resultsPerPage);

    setSearchParams(newParams);
  }, []);

  const defaultMapCoordinates = [48.1351, 11.582];
  const [startCoordinates, setStartCoordinates] = useState([
    "48.1351",
    "11.5820",
  ]);
  const [destinationCoordinates, setDestinationCoordinates] = useState([
    "43.2141",
    "27.9147",
  ]);

  let updatedData = [...data];

  useEffect(() => {
    //////FILTER
    const filterByParam = searchParams.get(FILTER_PARAM);
    if (filterByParam !== "all") {
      updatedData = updatedData.filter(
        (x) => x.transportationProviderName === filterByParam
      );
    }

    //////SORT
    const sortByValue = searchParams.get("sortBy");

    if (sortByValue === sortOptions[2].value) {
      updatedData?.sort((a, b) => {
        const travelTimeA = new Date(a.endTime) - new Date(a.startTime);
        const travelTimeB = new Date(b.endTime) - new Date(b.startTime);
        return travelTimeA - travelTimeB;
      });
    } else if (sortByValue === sortOptions[1].value) {
      updatedData?.sort((a, b) => b.price - a.price);
    } else if (sortByValue === sortOptions[0].value) {
      updatedData?.sort((a, b) => a.price - b.price);
    }

    //////PAGINATION
    let pageSize = searchParams.get("results");
    let page = searchParams.get("page");
    updatedData = updatedData.slice((page - 1) * pageSize, page * pageSize);

    //end of sorting. pagination and filtering
    setFilteredData(updatedData);
  }, [searchParams, data]);

  useEffect(() => {
    //UPDATE PARAMS
    let newParams = new URLSearchParams(searchParams);
    newParams.set(FILTER_PARAM, filterValue);
    newParams.set("sortBy", sortValue);
    newParams.set("page", currentPage);
    newParams.set("results", resultsPerPage);

    setSearchParams(newParams);
  }, [filterValue, sortValue, currentPage, resultsPerPage]);

  //The following effect fetches the longitute and latitude for the trip start and destination. The, the coordinates are used to set markers on the map.
  //Good to know: we make a call to an external API, which allows us up to 1000 calls per month free of charge.
  useEffect(() => {
    if (data && data.length > 0) {
      //MONTHLY QUOATA EXCEEDED
      //  CityApi.getCityData(data[0].startCityName)
      //      .then(data => setStartCoordinates([data[0].latitude, data[0].longitude]))
      //      .catch(err => {
      //          toast.error(err.error)
      //      })
      //  CityApi.getCityData(data[0].endCityName)
      //      .then(data => setDestinationCoordinates([data[0].latitude, data[0].longitude]))
      //      .catch(err => {
      //          toast.error(err.error)
      //      })
    }
  });
  return (
    <>
      <title>Search results | Voyago</title>
      <section className="search__container">
        <div className="search__results">
          <div className="search__results--options">
            <SortBy
              sortOptions={sortOptions}
              sortValue={sortValue}
              setSortValue={setSortValue}
            />
            <FilterBy
              title={"Provider"}
              options={filterOptions}
              paramName={FILTER_PARAM}
              filterValue={filterValue}
              setFilterValue={setFilterValue}
            />
          </div>

          {!filteredData || filteredData.length <= 0 ? (
            <h3>
              We could not find any offers for this search. Please, try again.
            </h3>
          ) : null}

          {filteredData?.map((trip) => (
            <SearchResultCard trip={trip} key={trip.tripId + Math.random()} />
          ))}
        </div>

        <div className="search__container-map">
          <MapContainer
            center={defaultMapCoordinates}
            style={{ height: "100%", width: "100%" }}
            zoom={5}
            scrollWheelZoom={true}
          >
            <TileLayer
              attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />

            {startCoordinates && <Marker position={startCoordinates}></Marker>}
            {destinationCoordinates && (
              <Marker position={destinationCoordinates}></Marker>
            )}
          </MapContainer>
        </div>
      </section>
      <Pagination
        totalItemsCount={data?.length}
        currentPage={currentPage}
        setCurrentPage={setCurrentPage}
        resultsPerPage={resultsPerPage}
        setResultsPerPage={setResultsPerPage}
        resultsPerPageOptions={resultsPerPageOptions}
      />
    </>
  );
}

export default SearchResultsPage;
