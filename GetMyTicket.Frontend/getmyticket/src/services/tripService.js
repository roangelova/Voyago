import { api } from "../api/api.js";

export const Trips = {
    executeFilter: () => api.post('api/trips/GetAllSearchResultTrips', filterData),
}