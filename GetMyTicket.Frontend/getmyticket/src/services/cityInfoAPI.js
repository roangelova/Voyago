import { api } from "../api/api.js";

const BASE_URL = 'https://api.api-ninjas.com/v1';

export const CityApi = {
    getCityData: (cityName) => api.get(`${BASE_URL}/city?name=${cityName}`)}