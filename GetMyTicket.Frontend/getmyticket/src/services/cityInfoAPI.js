const BASE_URL = 'https://api.api-ninjas.com/v1';
const API_KEY = process.env.REACT_APP_API_NINJAS_APP_KEY;

const headers = new Headers();
headers.append("Content-Type", "application/json");
headers.append('X-Api-Key', API_KEY);

export const CityApi = {
    getCityData: (cityName) => fetch(`${BASE_URL}/city?name=${cityName}`, { headers: headers })
        .then(response => response.json())
        .catch(error => console.error("Error fetching city data:", error))
}