import { api } from "../api/api.js";

export const Trips = {
    executeFilter: (filterData) => api.post('api/trips', filterData),
}