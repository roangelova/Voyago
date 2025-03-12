import { api } from "../api/api.js";

export const Trips = {
    executeFilter: (data) => api.get(`api/trips/search?StartDate=${data.startDate}&EndDate=${data.endDate}&StartCityId=${data.startCityId}&EndCityId=${data.endCityId}`),
}