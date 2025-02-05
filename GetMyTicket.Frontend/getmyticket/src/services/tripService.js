import { api } from "../api/api.js";

export const Trips = {
    executeFilter: () => api.get('api/trips/GetAllSearchResultTrips'),
}